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
            foreach(T1 t1 in enumerable1)
            {
                foreach (T2 t2 in enumerable2)
                {
                    yield return func(t1, t2); 
                }
            }
        }
        public GetProductsResponse GetProducts()
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
            return new GetProductsResponse()
            {
                products = new List<Product>()
                {
                    new Product()
                    {
                        id = product.Id,
                        name = product.Name,
                        shortDescription = product.Description,
                        fullDescription = product.Description,
                        currency = Currency.RUR,
                        minPremiums = insuranceSums.Min(A => int.Parse(A.Name)).ToString(),
                        maxPremiums = insuranceSums.Max(A => int.Parse(A.Name)).ToString(),
                        insuranceRisks = this.CrossJoin(risks, insuranceSums, (risk, insuranceSum) => new Risk()
                        {
                            id = risk.Id,
                            text = risk.Name,
                            sum = insuranceSum.Name,
                        }).ToList(),
                        policyTermOptions = insurancePeriods.Select(A => new PolicyTerm()
                        {
                            policyTerm = int.Parse(A.Name),
                        }).ToList(),
                        redemptionAmounts = getBuyoutTariffs.Select(A => new RedemptionAmount()
                        {
                            sum = decimal.Parse(A.InsSum, CultureInfo.InvariantCulture),
                            year = int.Parse(A.Year),
                            policyTerm = int.Parse(insurancePeriods.Single(B => B.Id == A.InsPeriod).Name),
                            currency = Currency.RUR,
                            contributionsPeriodicity = new Periodicity()
                            {
                                periodicitiy = 0,
                            }
                        }).ToList(),
                        contributionsPeriodicityOptions = new List<Periodicity>()
                        {
                            new Periodicity()
                            {
                                periodicitiy = 0,
                            }
                        },
                        investParams = strategies.Select(strategy => new InvestParam()
                        {
                            strategyId = strategy.ID,
                            coefficient = decimal.Parse(strategy.Coefficient),
                            contributionsPeriodicity = new Periodicity() { periodicitiy = 0 },
                            currencyInvest = Currency.RUR,
                            policyTerm = (int)((strategy.ExpirationDate - DateTime.Today).TotalDays / 365),
                            strategy = strategy.InvestmentStrategyRaw,
                            paymentsPeriodicities = new Periodicity() { periodicitiy = 0 },
                            coverCapital = 100,
                        }).ToList()
                    }
                }
            };
        }
    }
}