using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using tinkoff.ru.partners.insurance.investing.types;
using TinkoffClient;
using TinkoffClient.Models;
using TinkoffService.Entities;
using TinkoffService.Helpers;
using VirtuClient.Models;
using VirtuClient.Models.Core;
namespace TinkoffService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Insurance : InvestingInsuranceInterface
    {
        private VirtuClient.VirtuClient createVirtuClient(AuthenticationInput authenticationInput = null)
        {
            ConfigurationManagerHelper configurationManagerHelper = ConfigurationManagerHelper.Default;
            VirtuClient.VirtuClient result = new VirtuClient.VirtuClient(new Uri(configurationManagerHelper.VirtuBaseUrl));
            result.Logger = (log) =>
            {
                using (var db = new Model())
                {
                    db.Logs.Add(new Log()
                    {
                        Start = log.Start,
                        End = log.End,
                        Input = log.Input,
                        Output = log.Output,
                        Exception = log.Exception,
                        Name = log.Name ?? "",
                    });
                    db.SaveChanges();
                }
            };
            result.Authenticate(authenticationInput ?? new AuthenticationInput()
            {
                createPersistentCookie = true,
                userName = configurationManagerHelper.VirtuUserName,
                password = configurationManagerHelper.VirtuPassword,
            });
            return result;
        }
        private decimal getQuote(DateTime date, string strategyId)
        {
            var result = this.getQuotes(new getQuotesRequest1()
            {
                GetQuotesRequest = new GetQuotesRequest()
                {
                    strategyId = strategyId,
                    dateFrom = date.AddDays(-365),
                    dateTo = date,
                }
            });
            return result.GetQuotesResponse.quotes.OrderByDescending(A => A.date).First().price;
        }
        private decimal getRate(DateTime date, currency currency)
        {
            return currency == currency.RUR ? 1 : new CbrHelper().getRate(date, currency);
        }
        private TinkoffClient.TinkoffClient createTinkoffClient(VirtuClient.VirtuClient virtuClient = null)
        {
            return new TinkoffClient.TinkoffClient(virtuClient ?? this.createVirtuClient())
            {
                getQuote = this.getQuote,
                getRate = this.getRate,
                Log = (log) => this.addLog(
                        methodName: log.Name ?? "TinkoffClient",
                        start: log.Start ?? DateTime.Now,
                        input: log.Input,
                        output: log.Output),
            };
        }
        private string trySerialize(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return "<serialization error>";
            }
        }
        private string trySerializeException(Exception exception)
        {
            try
            {
                return JsonConvert.SerializeObject(exception);
            }
            catch
            {
                return exception.ToString();
            }
        }
        private void addLog(DateTime start, object input = null, object output = null, Exception exception = null, [CallerMemberName] string methodName = null)
        {
            using (var db = new Model())
            {
                db.Logs.Add(new Log()
                {
                    Start = start,
                    End = DateTime.Now,
                    Input = this.trySerialize(input),
                    Output = this.trySerialize(output),
                    Exception = this.trySerializeException(exception),
                    Name = methodName ?? "",
                });
                db.SaveChanges();
            }
        }
        private FaultException<CommonFault> handleException(Exception exception)
        {
            if (exception is VirtuException)
            {
                return new FaultException<CommonFault>(new CommonFault()
                {
                    errorCode = "502",
                    errorMessage = "Произошла ошибка взаимодействия с вышестоящим сервером",
                }, "Произошла ошибка взаимодействия с вышестоящим сервером");
            }
            else if (exception is ServiceException)
            {
                return new FaultException<CommonFault>(new CommonFault()
                {
                    errorCode = "500",
                    errorMessage = exception.Message,
                }, exception.Message);
            }
            else
            {
                return new FaultException<CommonFault>(new CommonFault()
                {
                    errorCode = "500",
                    errorMessage = "Произошла внутреняя ошибка сервиса",
                }, "Произошла внутреняя ошибка сервиса");
            }
        }
        private TResponce tryAction<TResponce, TInput, TOutput>(Func<TInput> inputFunc, Func<TInput, TOutput> actionFunc, Func<TOutput, TResponce> outputFunc, [CallerMemberName] string methodName = null)
            where TInput : class
            where TOutput : class
        {
            TInput input = null;
            TOutput output = null;
            Exception exception = null;
            DateTime start = DateTime.Now;
            try
            {
                input = inputFunc();
                output = actionFunc(input);
                return outputFunc(output);
            }
            catch (Exception error)
            {
                exception = error;
            }
            finally
            {
                this.addLog(
                    start: start,
                    input: input,
                    output: output,
                    exception: exception,
                    methodName: methodName);
            }
            throw this.handleException(exception);
        }
        public acceptPolicyResponse1 acceptPolicy(acceptPolicyRequest1 request)
        {
            return this.tryAction(
                inputFunc: () => request.AcceptPolicyRequest,
                actionFunc: input => this.createTinkoffClient().Accept(input),
                outputFunc: output => new acceptPolicyResponse1(output));
        }
        public createPolicyResponse1 createPolicy(createPolicyRequest1 request)
        {
            return this.tryAction(
                inputFunc: () => request.CreatePolicyRequest,
                actionFunc: input => this.createTinkoffClient().CreatePolicy(input),
                outputFunc: output => new createPolicyResponse1(output));
        }
        public getPolicyResponse1 getPolicy(getPolicyRequest1 request)
        {
            return this.tryAction(
               inputFunc: () => request.GetPolicyRequest,
               actionFunc: input => this.createTinkoffClient().GetPolicy(input),
               outputFunc: output => new getPolicyResponse1(output));
        }
        public getPolicyDocumentResponse1 getPolicyDocument(getPolicyDocumentRequest1 request)
        {
            return this.tryAction(
               inputFunc: () => request.GetPolicyDocumentRequest,
               actionFunc: input => this.createTinkoffClient().GetPolicyDocument(input),
               outputFunc: output => new getPolicyDocumentResponse1(output));
        }
        public getProductsResponse getProducts(getProductsRequest1 request)
        {
            return this.tryAction(
                inputFunc: () => request.GetProductsRequest,
                actionFunc: input => this.createTinkoffClient().GetProducts(input),
                outputFunc: output => output);
        }
        public getQuotesResponse1 getQuotes(getQuotesRequest1 request)
        {
            return this.tryAction(
                inputFunc: () => request.GetQuotesRequest,
                actionFunc: input => new QuotesHelper(this.createVirtuClient())
                {
                    Log = (log) => this.addLog(
                        methodName: log.Name ?? "QuotesHelper",
                        start: log.Start ?? DateTime.Now,
                        input: log.Input,
                        output: log.Output),
                }.GetQuotes(input),
                outputFunc: output => new getQuotesResponse1(output));
        }
    }
}