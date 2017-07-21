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
            using (var db = new Model())
            {
                return View(db.Logs.OrderByDescending(A => A.Start).Where(A => A.Exception != "null").Take(100).ToArray());
            }
        }
    }
}