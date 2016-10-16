using Salvis.App.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace Salvis.App.Web.Controllers
{
    public enum LayoutStyleEnum
    {
        NormalLayout = 0,
        LiteLayout = 1,
        AccountLayout = 2,
        MailLayout = 3
    }

    public abstract class SalvisBaseController : Controller
    {
        /// <summary>
        /// Indicates the LayoutStyle to use.
        /// </summary>
        /// <param name="layoutStyle"></param>
        protected SalvisBaseController(LayoutStyleEnum layoutStyle = LayoutStyleEnum.NormalLayout)
        {
            ViewBag.LayoutStyle = layoutStyle;
        }

        #region Json Responses

        /// <summary>
        /// Create a JsonResultData.
        /// </summary>
        /// <param name="data">Relevate information.</param>
        /// <param name="code">Type of JsonResulData.</param>
        /// <returns>A JsonResulData without message.</returns>
        protected JsonResult Json(dynamic data, HttpStatusCode code)
        {
            return base.Json(new JsonResultData
            {
                Data = data,
                Code = code
            });
        }

        protected JsonResult Json(JsonResultData jsonData)
        {
            return base.Json(jsonData);
        }

        /// <summary>
        /// Create a JsonResultData.
        /// </summary>
        /// <param name="data">Relevate information.</param>
        /// <param name="code">Type of JsonResulData.</param>
        /// <param name="message"></param>
        /// <param name="persist">Within persist so is stored on Session.</param>
        /// <returns></returns>
        protected JsonResult Json(dynamic data, HttpStatusCode code, MessageBox message, bool persist = false)
        {
            if (persist)
            {
                if (Session["Messagebox"] == null)
                {
                    Session.Add("Messagebox", Newtonsoft.Json.JsonConvert.SerializeObject(message));
                }
            }
            return base.Json(new JsonResultData
            {
                Data = data,
                Code = code,
                Message = message
            });
        }

        #endregion

        protected JsonResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Json(returnUrl, HttpStatusCode.Redirect);
            }
            return Json(Url.Action("Index", "Home"), HttpStatusCode.Redirect);
        }

    }
}