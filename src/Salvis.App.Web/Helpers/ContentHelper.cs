using System.Collections.Generic;
using System.Web.Mvc;
using Salvis.Framework.Helpers;
using Salvis.App.Web.Models;
using Salvis.App.Web.Services;
using System.Globalization;

namespace Salvis.App.Web.Helpers
{
    public static class ContentHelper
    {

        public static MvcHtmlString GetContent(this HtmlHelper htmlHelper, string id, string culture = "")
        {
            if (string.IsNullOrWhiteSpace(culture))
                culture = CultureInfo.DefaultThreadCurrentCulture.Name;
            var conditions = new Dictionary<string, string> { { "id", id }, { "culture", culture.Substring(0, 2) } };
            var path = ConfigurationHelper.GetSetting("pathDbContent");
            var xmlHelper = new XmlHelper<ContentObject>(path);
            var result = xmlHelper.Get(conditions);
            return MvcHtmlString.Create(result.Content);
        }

        public static MvcHtmlString GetCurrentCultureDateFormat(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(FormatHelper.GetDateFormatByCulture(CultureInfo.CurrentUICulture.Name));
        }

        public static MvcHtmlString GetCurrentCultureCurrencySymbol(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol);
        }
    }
}