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
        public GetProductsResponse GetProducts()
        {
            var products = this.virtuClient.GetProducts();
            var product = products.Single(A => A.Name == "Верное решение");

            var GetRisks = this.virtuClient.GetRisks(product.Id);
            var GetStrategies = this.virtuClient.GetStrategies(product.Id);
            var GetInsuranceSums = this.virtuClient.GetInsuranceSums(product.Id);
            var GetInsurancePeriods = this.virtuClient.GetInsurancePeriods(product.Id);
            var GetDocumentTypes = this.virtuClient.GetDocumentTypes(product.Id);
            var GetCurrencies = this.virtuClient.GetCurrencies(product.Id);
            var InsuredDocumentTypes = this.virtuClient.InsuredDocumentTypes(product.Id);
            var GetBuyoutTariffs = this.virtuClient.GetBuyoutTariffs(product.Id);
            var PrintForms = this.virtuClient.GetPrintforms(product.Id);

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
                        //minPremiums = GetInsuranceSums.Min(A => int.Parse(A.Name)).ToString(), //хз
                        //maxPremiums = GetInsuranceSums.Max(A => int.Parse(A.Name)).ToString(), //хз
                        insuranceRisks = GetRisks.Select(risk => new Risk()
                        {
                            id = risk.Id,
                            text = risk.Name,
                            sum = null, //хз
                        }).ToList(),
                        policyTermOptions = GetInsurancePeriods.Select(A => new PolicyTerm()
                        {
                            policyTerm = int.Parse(A.Name),
                        }).ToList(),
                        redemptionAmounts = GetBuyoutTariffs.Select(A => new RedemptionAmount()
                        {
                            sum = decimal.Parse(A.InsSum, CultureInfo.InvariantCulture),
                            year = int.Parse(A.Year),
                            policyTerm = int.Parse(GetInsurancePeriods.Single(B => B.Id == A.InsPeriod).Name),
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
                        investParams = GetStrategies.Select(strategy => new InvestParam() //хз
                        {
                        }).ToList(),
                    }
                }
            };
        }
    }
}