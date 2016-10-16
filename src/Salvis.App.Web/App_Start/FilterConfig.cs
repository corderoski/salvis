using Salvis.App.Web.Filters;
using System.Web.Mvc;

namespace Salvis.App.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var errorAttribute = new HandleErrorAttribute();
            filters.Add(new AuthorizeAttribute());
            errorAttribute.View = "Home/Error";
            filters.Add(errorAttribute);
            filters.Add(new SalvisAntiForgeryToken());
        }
    }
    
}
