using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Salvis.App.Web.Filters
{

    /// <summary>
    /// An attribute used to prevent forgery of a request, mainly the Ajax ones.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SalvisAntiForgeryToken : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {
                if (request.IsAjaxRequest())
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
                    var cookieValue = antiForgeryCookie != null ? antiForgeryCookie.Value : null;
                    var header = request.Headers["__RequestVerificationToken"];
                    var form = request.Form != null ? request.Form["__RequestVerificationToken"] : null;
                    var token = string.IsNullOrEmpty(form) ? header : form;
                    AntiForgery.Validate(cookieValue, token);
                }
                else
                {
                    try
                    {
                        new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
                    }
                    catch (Exception)
                    {
                        filterContext.Result = new JsonResult { Data = "Authorization cannnot be handled.", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                }
            }
            base.OnAuthorization(filterContext);
        }
    }
}