using System.Linq;
using System.Web.Mvc;
using TcsInsurance.Entities;
namespace TcsInsurance.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        [HttpGet]
        public ActionResult Index()
        {
            string[] methodNames = new string[]
            {
                "acceptPolicy",
                "createPolicy",
                "getPolicy",
                "getPolicyDocument",
                "getProducts",
                "getQuotes",
            };
            using (var db = new Model())
            {
                return View(db.Logs
                    .Where(A => methodNames.Contains(A.Name))
                    .Where(A => A.Exception != "null")
                    .OrderByDescending(A => A.Start)
                    .Take(100)
                    .ToArray());
            }
        }
    }
}