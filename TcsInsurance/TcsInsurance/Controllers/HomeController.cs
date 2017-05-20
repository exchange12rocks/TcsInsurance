using System;
using System.Web.Mvc;
using VirtuClient.Models;
namespace TcsInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var virtuClient = new VirtuClient.VirtuClient(new Uri("https://uralsiblife.virtusystems.ru"));
            virtuClient.Authenticate(new AuthenticationInputParams()
            {
                userName = "site_integr",
                password = "es4zMJ5JZs",
                createPersistentCookie = true,
            });
            var products = virtuClient.GetProducts();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}