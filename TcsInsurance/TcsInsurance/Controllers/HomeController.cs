using System.Web;
using System.Web.Mvc;
using TcsInsurance.Entities;
using TcsInsurance.Helpers;
using System.Linq;
using System;
using TcsInsurance.ViewModels;
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
            DateTime? actualDate = null;
            using (var db = new Model())
            {
                if(db.TickerHistoryValues.Any())
                {
                    actualDate = db.TickerHistoryValues.Max(A => A.Date);
                }
            }
            return View(new UploadViewModel()
            {
                ActualDate = actualDate,
            });
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