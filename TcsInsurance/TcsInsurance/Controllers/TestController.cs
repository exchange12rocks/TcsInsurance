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
                productId = "",
                strategyId = "",
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