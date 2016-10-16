using System.Linq;
using System.Web.Mvc;

namespace Salvis.App.Web.Controllers
{

    [AllowAnonymous]
    public class ResourceController : Controller
    {

        public JsonResult GetResources()
        {
            return Json(
                 typeof(Resources.Texts).GetProperties()
                 .Where(p => !(p.Name.Equals("ResourceManager") || p.Name.Equals("Culture")))
                 .ToDictionary(p => p.Name, p => p.GetValue(null) as string)
                 , JsonRequestBehavior.AllowGet);
        }

    }
}
