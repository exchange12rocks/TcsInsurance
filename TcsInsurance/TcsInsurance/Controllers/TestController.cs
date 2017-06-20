using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TcsInsurance.TinkoffInsuranceServiceReference;
namespace TcsInsurance.Controllers
{
    public class TestController : Controller
    {
        private VirtuClient.VirtuClient virtuClient;
        private InvestingInsuranceInterfaceClient tinkoffClient;
        public TestController()
        {
            this.virtuClient = new VirtuClient.VirtuClient(new Uri("https://uralsiblife.virtusystems.ru"));
            this.virtuClient.Authenticate(new VirtuClient.Models.AuthenticationInput()
            {
                userName = "site_integr",
                password = "es4zMJ5JZs",
                createPersistentCookie = true,
            });
            this.tinkoffClient = new InvestingInsuranceInterfaceClient();
        }
        // GET: Test
        public ActionResult Index()
        {
            var product = this.tinkoffClient.getProducts(new GetProductsRequest())[0];
            var quotes = this.tinkoffClient.getQuotes(new GetQuotesRequest()
            {
                dateFrom = DateTime.Today.AddYears(-1),
                dateTo = DateTime.Today,
                productId = product.id,
                strategyId = product.investParams[0].strategyId,
            });
			var policy = this.tinkoffClient.createPolicy(new CreatePolicyRequest()
			{
				amount = 200000,
				currency = currency.RUR,
				strategyId = product.investParams[0].strategyId,
				policyTerm = product.investParams[0].policyTerm,
				productId = product.id,
				periodicityOfContributions = product.investParams[0].contributionsPeriodicity,
				periodicitiesOfPayments = product.investParams[0].paymentsPeriodicities,
				insurantIsInsured = true,
				coverCapital = product.investParams[0].coverCapital,
				insurant = new person()
				{
					addresses = new address[]
					{
						new address()
						{
							type = addressType.registration,
							country = "country",
							city = "city",
							region = "region",
							street = "street",
							house = "house",
							flat = "flat",
						}
					}
				}

			});
            /*var policy = this.tinkoffClient.createPolicy(new CreatePolicyRequest()
            {
                amount = 60000,
                currency = currency.RUR,
                insurant = new person()
                {
                    sex = sex.male,
                    documentSerie = "1234",
                    documentNumber = "123456",
                    documentIssueDate = DateTime.Today.AddYears(-18),
                    dateOfBirth = DateTime.Today.AddYears(-28),
                    firstName = "",
                    lastName = "",
                    patronymicName = "",
                    inn = "",
                    placeOfBirth = "",
                    addresses = new address[]
                    {
                        new address()
                        {
                            city = "г.Тестовый",
                            country = "Россия",
                        }
                    }
                },
            });*/
            return View();
        }
    }
}