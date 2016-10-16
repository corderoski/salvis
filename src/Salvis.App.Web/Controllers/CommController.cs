using Salvis.Entities;
using Salvis.Framework.Helpers;
using Salvis.Framework.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Salvis.App.Web.Controllers
{
    /// <summary>
    /// Controller for commonly used operations.
    /// </summary>
    [AllowAnonymous]
    public class CommController : Controller
    {



        private readonly ICatalogService _catalogService;

        public CommController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>es - if spanish or inherint, otherwise 'en'.</returns>
        private CatalogDescriptionLang GetCulture()
        {
            var lang = Session["Lang"];
            if (lang == null)
            {
                lang = FormatHelper.APP_SPANISH_CURRENCY;
            }
            return (CatalogDescriptionLang) Enum.Parse(typeof (CatalogDescriptionLang), lang.ToString().Substring(0, 2).ToUpperInvariant());
        }

        //
        // GET: /Comm/

        public JsonResult GetTypeDescription(string cat)
        {
            var culture = GetCulture();
            var items = _catalogService.Get(cat);
            var result = items.Select(i => new 
                {
                    Id = i.SubCategoryId + "",
                    Value = culture == CatalogDescriptionLang.ES ? i.DescriptionES : i.DescriptionEN,
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTypesWithValue(string cat)
        {
            var items = _catalogService.Get(cat);
            var result = items.Select(i => new 
                {
                    Id = i.SubCategoryId + "",
                    Value = i.Value,
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetValueDescription(string cat)
        {
            var culture = GetCulture();
            var items = _catalogService.Get(cat);
            var result = items.Select(i => new 
                {
                    Id = i.Value + "",
                    Value = culture == CatalogDescriptionLang.ES ? i.DescriptionES : i.DescriptionEN,
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
