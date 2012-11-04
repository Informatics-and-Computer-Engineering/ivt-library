using System.Web.Mvc;

namespace IvtLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Добро пожаловать в библиотеку IVTLibrary";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
