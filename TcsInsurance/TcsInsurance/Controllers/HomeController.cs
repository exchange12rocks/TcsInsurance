using System;
using System.Web.Mvc;
using VirtuClient;
using VirtuClient.Models;
using System.Linq;
using TcsInsurance.Helpers;
using TcsInsurance.Entities;

namespace TcsInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var virtuClient = new VirtuClient.VirtuClient(new Uri("https://uralsiblife.virtusystems.ru"), VirtuMapper.Instance);
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
                GetRisks = virtuClient.GetRisks(product.Id),
                GetStrategies = virtuClient.GetStrategies(product.Id),
                GetInsuranceSums = virtuClient.GetInsuranceSums(product.Id),
                GetInsurancePeriods = virtuClient.GetInsurancePeriods(product.Id),
                GetDocumentTypes = virtuClient.GetDocumentTypes(product.Id),
                GetCurrencies = virtuClient.GetCurrencies(product.Id),
                InsuredDocumentTypes = virtuClient.InsuredDocumentTypes(product.Id),
                GetBuyoutTariffs = virtuClient.GetTariffs(product.Id),
                PrintForms = virtuClient.GetPrintforms(product.Id),
                StrategiesSearch = virtuClient.StrategiesSearch(new StrategiesSearchInput()
                {
                    IsActive = true,
                    ReadRedefined = true,
                }),
                Statuses = virtuClient.GetStatuses(product.Id),
                Calculate = virtuClient.Calculate(new CalculateInput()
                {
                    ProductID = product.Id,
                    Premium = "30000",
                }),
            };
            var quotesHelper = new QuotesHelper(virtuClient);
            var quotes = quotesHelper.GetQuotes(new GetQuotesRequest()
            {
                dateFrom = new DateTime(2017, 1, 1),
                dateTo = new DateTime(2017, 1, 7),
                strategyId = dictionaries.StrategiesSearch.First().ID,
                productId = product.Id,
            });
            var draft = virtuClient.Read("37EEAC87-62EE-4A09-B9D7-0FB247124EDD");
            var t = virtuClient.Read("C81DF550-0E96-41EC-B849-9230FC1BCBCF");
            var policy = virtuClient.Save(new Policy()
            {
                Premium = "30000",
                ProductID = product.Id,
                ProductName = "Верное решение",
                
                StatusID = dictionaries.Statuses.First(A => A.DisplayName == "Проект").Id,
                DocumentStatusID = dictionaries.Statuses.First(A => A.DisplayName == "Проект").Id,
                StatusName = dictionaries.Statuses.First(A => A.DisplayName == "Проект").Name,
                CreatorUser = "testaa",
                CreatorName = "Леонтьев Тест Тестович",
                DocumentDate = DateTime.Today,
                EffectiveDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddYears(5),
                InvestmentStrategy = dictionaries.GetStrategies.First().Id,

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
            virtuClient.Accept(policy);
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