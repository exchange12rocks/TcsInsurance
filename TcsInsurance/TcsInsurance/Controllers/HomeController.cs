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
            var test = new
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
                StrategiesSearch = virtuClient.StrategiesSearch(new StrategiesSearchDataInput()
                {
                    IsActive = true,
                    ReadRedefined = true,
                })
            };
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
            return Json(test, JsonRequestBehavior.AllowGet);
        }
    }
}