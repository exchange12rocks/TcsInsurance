using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TcsInsurance.Entities;
using TcsInsurance.Helpers;
using tinkoff.ru.partners.insurance.investing.types;
using VirtuClient.Models;
namespace TcsInsurance.Controllers
{
    public static class Extensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            int skip = new Random().Next(enumerable.Count());
            return enumerable.Skip(skip).First();
        }
    }
    public class HomeController : Controller
    {
        private VirtuClient.VirtuClient virtuClient;
        private TinkoffClient.TinkoffClient tinkoffClient;
        public HomeController()
        {
            this.virtuClient = new VirtuClient.VirtuClient(new Uri("https://uralsiblife.virtusystems.ru"));
            this.virtuClient.Authenticate(new AuthenticationInput()
            {
                userName = "site_integr",
                password = "es4zMJ5JZs",
                createPersistentCookie = true,
            });
            this.tinkoffClient = new TinkoffClient.TinkoffClient(this.virtuClient);
        }
        public ActionResult Index()
        {
            var products = virtuClient.GetProducts();
            var product = products.Single(A => A.Name == "Верное решение");
            var dictionaries = new
            {
                Products = products,
                Product = product,
                GetRisks = virtuClient.GetRisks(product.ID),
                GetStrategies = virtuClient.GetStrategies(product.ID),
                GetInsuranceSums = virtuClient.GetInsuranceSums(product.ID),
                GetInsurancePeriods = virtuClient.GetInsurancePeriods(product.ID),
                GetDocumentTypes = virtuClient.GetDocumentTypes(product.ID),
                GetCurrencies = virtuClient.GetCurrencies(product.ID),
                InsuredDocumentTypes = virtuClient.InsuredDocumentTypes(product.ID),
                GetBuyoutTariffs = virtuClient.GetTariffs(product.ID),
                PrintForms = virtuClient.GetPrintforms(product.ID),
                StrategiesSearch = virtuClient.StrategiesSearch(new StrategiesSearchInput()
                {
                    IsActive = true,
                    ReadRedefined = true,
                }),
                Statuses = virtuClient.GetStatuses(product.ID),
                Calculate = virtuClient.Calculate(new CalculateInput()
                {
                    ProductID = product.ID,
                    Premium = "30000",
                }),
            };
            var quotes = new QuotesHelper(virtuClient).GetQuotes(new GetQuotesRequest()
            {
                dateFrom = new DateTime(2017, 1, 1),
                dateTo = new DateTime(2017, 1, 7),
                strategyId = dictionaries.StrategiesSearch.First().ID,
                productId = product.ID,
            });


            var strategy = dictionaries.StrategiesSearch.Random();
            var policy = tinkoffClient.CreatePolicy(new CreatePolicyRequest()
            {
                amount = 200000,
                currency = currency.RUR,
                insurantIsInsured = true,
                insurant = new person()
                {
                    firstName = "Имя",
                    lastName = "Фамилия",
                    patronymicName = "Отчество",
                    addresses = new address[]
                    {
                        new address()
                        {
                            type = addressType.registration,
                            city = "г.Ульяновск",
                            index = "432000",
                            country = "Россия",
                            street = "пер. Меньковского",
                            house = "22",
                            flat = "11"
                        }
                    },
                    inn = "123123123",
                    dateOfBirth = DateTime.Today.AddYears(-29),
                    sex = sex.male,
                    placeOfBirth = "г.Ульяновск",
                    documentSerie = "1212",
                    documentNumber = "123123",
                    documentIssueDate = DateTime.Today.AddYears(-2),
                    documentIssueDateSpecified = true,
                    documentOrganisation = "отделом ОФМС",
                    documentOrganisationCode = "123-123"
                },
                policyTerm = dictionaries.GetInsurancePeriods.Single(A => A.ID == strategy.OptionPeriod).Name,
                strategyId = strategy.ID,
                productId = product.ID,
                periodicityOfContributions = periodicity.Item0,
            });
            var accept = tinkoffClient.Accept(new AcceptPolicyRequest()
            {
                policyId = policy.policyId,
            });
            var document = tinkoffClient.GetPolicyDocumentsList(new GetPolicyDocumentsListRequest()
            {
                policyId = policy.policyId,
            }).GetPolicyDocumentsListResponse1.Single(A => string.Equals(A.name, "Полис", StringComparison.OrdinalIgnoreCase));
            var pdf = tinkoffClient.GetPolicyDocument(new GetPolicyDocumentRequest()
            {
                policyId = policy.policyId,
                documentId = document.id,
            });
            System.IO.File.WriteAllBytes(@"C:\Users\iamai\Desktop\1.pdf", pdf.documentData);
            try
            {
                using (var db = new Model())
                {
                    var helper = new TickerHistoryHelper(db);
                    using (var stream = System.IO.File.OpenRead(@"C:\Users\iamai\OneDrive\Temporary\work\Документы по взаимодействию с Тинькофф Банком\Графики_сайт.xlsx"))
                    {
                        helper.AddOrUpdate(helper.GetFromExcel(stream));
                    }
                }
            }
            catch(Exception exception)
            {
                return Json(exception.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json(dictionaries, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult LoadTickets()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                using (var db = new Model())
                {
                    TickerHistoryHelper helper = new TickerHistoryHelper(db);
                    helper.GetFromExcel(upload.InputStream);
                }
            }
            return RedirectToAction("LoadTickets");
        }
    }
}