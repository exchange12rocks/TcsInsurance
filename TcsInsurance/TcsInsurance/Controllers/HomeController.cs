using System;
using System.Web.Mvc;
using VirtuClient;
using VirtuClient.Models;
using System.Linq;
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
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}