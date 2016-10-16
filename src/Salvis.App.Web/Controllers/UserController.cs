using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Salvis.Framework.Helpers;
using Salvis.Framework.Services;
using Salvis.App.Web.Models;
using WebGrease.Css.Extensions;


namespace Salvis.App.Web.Controllers
{
    [Authorize]
    public class UserController : SalvisBaseController
    {

        public UserController()
        {
        }


        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Configurations()
        {
            return View();
        }
    }
}
