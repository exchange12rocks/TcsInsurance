using System.Globalization;
using System.Linq;
using System;
using TinkoffClient.Models;
using System.Collections.Generic;
namespace TinkoffClient
{
    public class TinkoffClient
    {
        private VirtuClient.VirtuClient virtuClient;
        public TinkoffClient(VirtuClient.VirtuClient virtuClient)
        {
            this.virtuClient = virtuClient;
        }
        private IEnumerable<TResult> CrossJoin<T1, T2, TResult>(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, Func<T1, T2, TResult> func)
        {
            foreach (T1 t1 in enumerable1)
            {
                foreach (T2 t2 in enumerable2)
                {
                    yield return func(t1, t2);
                }
            }
        }
        public getProductsResponse GetProducts(GetProductsRequest parameter)
        {
            var products = this.virtuClient.GetProducts();
            var product = products.Single(A => A.Name == "Верное решение");

            var risks = this.virtuClient.GetRisks(product.Id);
            var insuranceSums = this.virtuClient.GetInsuranceSums(product.Id);
            var insurancePeriods = this.virtuClient.GetInsurancePeriods(product.Id);
            var documentTypes = this.virtuClient.GetDocumentTypes(product.Id);
            var currencies = this.virtuClient.GetCurrencies(product.Id);
            var insuredDocumentTypes = this.virtuClient.InsuredDocumentTypes(product.Id);
            var getBuyoutTariffs = this.virtuClient.GetTariffs(product.Id);
            var printForms = this.virtuClient.GetPrintforms(product.Id);
            var strategies = this.virtuClient.StrategiesSearch(new VirtuClient.Models.StrategiesSearchInput()
            {
                IsActive = true,
                ReadRedefined = true,
            });
            return new getProductsResponse()
            {
                GetProductsResponse1 = new Product[]
                {
                    new Product()
                    {
                        id = product.Id,
                        name = product.Name,
                        shortDescription = product.Description,
                        fullDescription = product.Description,
                        currency = currency.RUR,
                        minPremiums = insuranceSums.Min(A => int.Parse(A.Name)).ToString(),
                        maxPremiums = insuranceSums.Max(A => int.Parse(A.Name)).ToString(),
                        insuranceRisks = this.CrossJoin(risks, insuranceSums, (risk, insuranceSum) => new risk()
                        {
                            id = risk.Id,
                            text = risk.Name,
                            sum = insuranceSum.Name,
                        }).ToArray(),
                        policyTermOptions = insurancePeriods.Select(A => A.Name).ToArray(),
                        redemptionAmounts = getBuyoutTariffs.Select(A => new redemptionAmount()
                        {
                            sum = decimal.Parse(A.InsSum, CultureInfo.InvariantCulture),
                            year = A.Year,
                            policyTerm = insurancePeriods.Single(B => B.Id == A.InsPeriod).Name,
                            currency = currency.RUR,
                            contributionsPeriodicity = periodicity.Item0,
                        }).ToArray(),
                        contributionsPeriodicityOptions = new periodicity[]
                        {
                            periodicity.Item0,
                        },
                        investParams = strategies.Select(strategy => new investParam()
                        {
                            strategyId = strategy.ID,
                            coefficient = decimal.Parse(strategy.Coefficient),
                            contributionsPeriodicity = periodicity.Item0,
                            currencyInvest = currency.RUR,
                            policyTerm = ((int)((strategy.ExpirationDate - DateTime.Today).TotalDays / 365)).ToString(),
                            strategy = strategy.InvestmentStrategyRaw,
                            paymentsPeriodicities = periodicity.Item0,
                            coverCapital = 100,
                        }).ToArray()
                    }
                }
            };
        }
        public CreatePolicyResponse CreatePolicy(CreatePolicyRequest parameter)
        {
            return null;
        }
        public GetPolicyResponse GetPolicy(GetPolicyRequest parameter)
        {
            return null;
        }
        public getPolicyDocumentsListResponse GetPolicyDocumentsList(GetPolicyDocumentsListRequest parameter)
        {
            var policy = this.virtuClient.Read(parameter.policyId);
            string[] allowedPrintformNames = new string[]
            {
                "Полис",
                "Заявление на возврат",
                "Правила и согласие на обработку персональных данных",
                "Онлайн оплата полиса",
                "Форма самосертификации (FATCA)",
                "Заявление о внесении изменений в Договор страхования",
            };
            return new getPolicyDocumentsListResponse()
            {
                GetPolicyDocumentsListResponse1 = this.virtuClient.GetPrintforms(policy.ProductID)
                    .Where(A => allowedPrintformNames.Contains(A.Value.Caption, StringComparer.OrdinalIgnoreCase))
                    .Select(A => new document()
                    {
                        id = A.Key,
                        name = A.Value.Caption,
                    })
                    .ToArray(),
            };
        }
        public GetPolicyDocumentResponse GetPolicyDocument(GetPolicyDocumentRequest parameter)
        {
            return new GetPolicyDocumentResponse()
            {
                documentData = this.virtuClient.Print(new VirtuClient.Models.PrintInput()
                {
                    policyID = parameter.policyId,
                    viewID = parameter.documentId,
                }),
                documentName = null,
                documentType = null,
            };
        }
        public AcceptPolicyResponse Accept(AcceptPolicyRequest parameter)
        {
            var policy = this.virtuClient.Read(parameter.policyId);
            this.virtuClient.Accept(policy);
            return new AcceptPolicyResponse()
            {
                result = "OK",
            };
        }
    }
}