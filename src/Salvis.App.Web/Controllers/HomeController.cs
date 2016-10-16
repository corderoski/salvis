using System.Web.Mvc;

namespace Salvis.App.Web.Controllers
{
    [HandleError(View = "~/Views/Home/Error")]
    [AllowAnonymous]
    public class HomeController : SalvisBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error(string error)
        {
            ViewBag.LayoutStyle = LayoutStyleEnum.LiteLayout;

            string msg = !string.IsNullOrEmpty(error) ? error : Salvis.Resources.Texts.ErrorInternal;
            ViewBag.Error = msg;
            return View();
        }

        public ActionResult Default()
        {
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["disable"]))
                Redirect("Home/Index");

            ViewBag.LayoutStyle = LayoutStyleEnum.LiteLayout;
            return View();
        }

    }
}
