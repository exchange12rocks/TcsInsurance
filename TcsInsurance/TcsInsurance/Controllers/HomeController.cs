using System;
using System.Linq;
using System.Web.Mvc;
using TcsInsurance.Entities;
using TcsInsurance.Helpers;
using TinkoffClient.Models;
using VirtuClient.Models;
namespace TcsInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var virtuClient = new VirtuClient.VirtuClient(new Uri("https://uralsiblife.virtusystems.ru"));
            virtuClient.Authenticate(new AuthenticationInput()
            {
                userName = "site_integr",
                password = "es4zMJ5JZs",
                createPersistentCookie = true,
            });
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
                Policy = virtuClient.Read("90EE1179-F96A-4ED7-B1B5-AC205DE9BA97")
            };
            var quotes = new QuotesHelper(virtuClient).GetQuotes(new GetQuotesRequest()
            {
                dateFrom = new DateTime(2017, 1, 1),
                dateTo = new DateTime(2017, 1, 7),
                strategyId = dictionaries.StrategiesSearch.First().ID,
                productId = product.ID,
            });
            var draft = dictionaries.Policy;
            var policy = virtuClient.Save(new Policy()
            {
                Premium = "30000",
                ProductID = product.ID,
                ProductName = "Верное решение",
                
                StatusID = dictionaries.Statuses.First(A => A.DisplayName == "Проект").ID,
                DocumentStatusID = dictionaries.Statuses.First(A => A.DisplayName == "Проект").ID,
                StatusName = dictionaries.Statuses.First(A => A.DisplayName == "Проект").Name,
                CreatorUser = "testaa",
                CreatorName = "Леонтьев Тест Тестович",
                DocumentDate = DateTime.Today,
                EffectiveDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddYears(5),
                InvestmentStrategy = dictionaries.GetStrategies.First().ID,

                InsurerRepresentId = "e1ea1a8b-1113-4006-bfa0-0ecd5f01a7d6",
                InsurerRepresentName = "Леонтьев Тест Тестович",
                SallerDivisionID = "79489087-3082-4c78-bb32-81f5039ffb25",
                SallerDivision = "УРАЛСИБ Жизнь",
                SERIAL = "ИСЖ",
                AcceptationDate = DateTime.Today,
                ReceiptSum = "30000",
                PaymentDocumentDate = DateTime.Now,
                ReceiptDate = DateTime.Now,
                KvPartner1Percent = dictionaries.Calculate.KvPartner1Percent,
                KvPartner2Percent = dictionaries.Calculate.KvPartner2Percent,
                KvPartner1Rub = dictionaries.Calculate.KvPartner1Rub,
                KvPartner2Rub = dictionaries.Calculate.KvPartner2Rub,

            });
            //virtuClient.Accept(policy);
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
    }
}