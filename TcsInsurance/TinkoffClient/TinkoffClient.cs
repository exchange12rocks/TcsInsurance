using System.Globalization;
using System.Linq;
using System;
using TinkoffClient.Models;
using System.Collections.Generic;
using VirtuClient.Models;
using System.IO;
namespace TinkoffClient
{
    public class TinkoffClient
    {
        private static IEnumerable<TResult> CrossJoin<T1, T2, TResult>(IEnumerable<T1> enumerable1, IEnumerable<T2> enumerable2, Func<T1, T2, TResult> func)
        {
            foreach (T1 t1 in enumerable1)
            {
                foreach (T2 t2 in enumerable2)
                {
                    yield return func(t1, t2);
                }
            }
        }
        private static string GetDate(DateTime? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return value.Value.ToString("yyyy-MM-dd");
            }
        }
        private static DateTime? GetDate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(value, "yyyy-MM-dd", null);
            }
        }
        private static GetClassifierOutput GetCurrency(currency? value, GetClassifierOutput[] currencies)
        {
            if (value == null)
            {
                return null;
            }
            else if(value.Value == currency.RUR)
            {
                return currencies.Single(A => string.Equals(A.Name, "РУБЛЬ", StringComparison.OrdinalIgnoreCase));
            }
            else if (value.Value == currency.USD)
            {
                return currencies.Single(A => string.Equals(A.Name, "ДОЛЛАР США", StringComparison.OrdinalIgnoreCase));
            }
            else if (value.Value == currency.EUR)
            {
                return currencies.Single(A => string.Equals(A.Name, "ЕВРО", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                throw new ArgumentException("unknown currency value");
            }
        }
        private static currency? GetCurrency(GetClassifierOutput value)
        {
            if (value == null)
            {
                return null;
            }
            else if (string.Equals(value.Name, "РУБЛЬ", StringComparison.OrdinalIgnoreCase))
            {
                return currency.RUR;
            }
            else if (string.Equals(value.Name, "ДОЛЛАР США", StringComparison.OrdinalIgnoreCase))
            {
                return currency.USD;
            }
            else if (string.Equals(value.Name, "ЕВРО", StringComparison.OrdinalIgnoreCase))
            {
                return currency.EUR;
            }
            else
            {
                throw new ArgumentException("unknown currency value");
            }
        }
        private static string getExtension(string transformFile)
        {
            string extension = Path.GetExtension(transformFile);
            if (string.Equals(extension, ".mrt", StringComparison.OrdinalIgnoreCase))
            {
                return ".pdf";
            }
            else
            {
                return extension;
            }
        }
        private static string getContentType(string extension)
        {
            if (string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return "application/pdf";
            }
            else if (string.Equals(extension, ".doc", StringComparison.OrdinalIgnoreCase))
            {
                return "application/msword";
            }
            else if (string.Equals(extension, ".docx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else
            {
                return null;
            }
        }

        private VirtuClient.VirtuClient virtuClient;
        public TinkoffClient(VirtuClient.VirtuClient virtuClient)
        {
            this.virtuClient = virtuClient;
        }
        
        public getProductsResponse GetProducts(GetProductsRequest parameter)
        {
            var product = this.virtuClient.GetProducts()
                .Single(A => string.Equals(A.Name, "Верное решение", StringComparison.OrdinalIgnoreCase));

            var risks = this.virtuClient.GetRisks(product.ID);
            var insuranceSums = this.virtuClient.GetInsuranceSums(product.ID);
            var insurancePeriods = this.virtuClient.GetInsurancePeriods(product.ID);
            var documentTypes = this.virtuClient.GetDocumentTypes(product.ID);
            var currencies = this.virtuClient.GetCurrencies(product.ID);
            var insuredDocumentTypes = this.virtuClient.InsuredDocumentTypes(product.ID);
            var getBuyoutTariffs = this.virtuClient.GetTariffs(product.ID);
            var strategies2 = this.virtuClient.GetStrategies(product.ID);
            var strategies = this.virtuClient.StrategiesSearch(new StrategiesSearchInput()
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
                        id = product.ID,
                        name = product.Name,
                        shortDescription = product.Description,
                        fullDescription = product.Description,
                        currency = currency.RUR,
                        minPremiums = insuranceSums.Min(A => int.Parse(A.Name)).ToString(),
                        maxPremiums = insuranceSums.Max(A => int.Parse(A.Name)).ToString(),
                        insuranceRisks = CrossJoin(risks, insuranceSums, (risk, insuranceSum) => new risk()
                        {
                            id = risk.ID,
                            text = risk.Name,
                            sum = insuranceSum.Name,
                        }).ToArray(),
                        policyTermOptions = insurancePeriods.Select(A => A.Name).ToArray(),
                        redemptionAmounts = getBuyoutTariffs.Select(A => new redemptionAmount()
                        {
                            sum = decimal.Parse(A.InsSum.value),
                            year = A.Year.value,
                            policyTerm = insurancePeriods.Single(B => B.ID == A.InsPeriod.value).Name,
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
                            currencyInvest = GetCurrency(currencies.Single(A => string.Equals(A.ID, strategy.OptionCurrency, StringComparison.OrdinalIgnoreCase))).Value,
                            policyTerm = null,
                            strategy = strategy.InvestmentStrategyRaw,
                            paymentsPeriodicities = periodicity.Item0,
                            coverCapital = 100,
                        }).ToArray(),
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
                //"Заявление на возврат",
                "Правила и согласие на обработку персональных данных",
                //"Онлайн оплата полиса",
                //"Форма самосертификации (FATCA)",
                //"Заявление о внесении изменений в Договор страхования",
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
            var productId = this.virtuClient.Read(parameter.policyId).ProductID;
            var printform = this.virtuClient.GetPrintforms(productId).Single(A => string.Equals(A.Key, parameter.documentId, StringComparison.OrdinalIgnoreCase));
            return new GetPolicyDocumentResponse()
            {
                documentData = this.virtuClient.Print(new PrintInput()
                {
                    policyID = parameter.policyId,
                    viewID = parameter.documentId,
                }),
                documentName = printform.Value.Caption,
                documentType = getContentType(getExtension(printform.Value.Transform)),
            };
        }
        public AcceptPolicyResponse Accept(AcceptPolicyRequest parameter)
        {
            Policy policy = this.virtuClient.Read(parameter.policyId);
            var status = this.virtuClient.GetStatuses(policy.ProductID).Single(A => string.Equals(A.Name, "Действующий", StringComparison.OrdinalIgnoreCase));
            policy.AcceptationDate = GetDate(DateTime.Now);
            policy.ReceiptDate = GetDate(DateTime.Now);
            policy.PaymentType = "4";
            policy.ReceiptSum = policy.Premium;
            policy.StatusID = status.ID;
            policy.DocumentStatusID = status.ID;
            policy.StatusName = status.Name;
            this.virtuClient.Accept(policy);
            return new AcceptPolicyResponse()
            {
                result = "OK",
            };
        }
    }
}