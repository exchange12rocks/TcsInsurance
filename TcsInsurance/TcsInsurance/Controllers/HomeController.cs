using System.Web;
using System.Web.Mvc;
using TcsInsurance.Entities;
using TcsInsurance.Helpers;
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
                    helper.AddOrUpdate(helper.GetFromExcel(upload.InputStream));
                }
            }
            return RedirectToAction("Index");
        }
    }
}