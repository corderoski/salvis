using System.Configuration;
using System.Web.Optimization;

namespace Salvis.App.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;

            #region Basic bundles

            //Basic JS and CSS
            bundles.Add(new ScriptBundle("~/bundles/basic").Include(
                "~/Scripts/externals/jquery-2.2.0.js",
                "~/Content/themes/thirdParty/bootstrap/dist/js/bootstrap.js",
                "~/Content/themes/thirdParty/behaviour/general.js",
                "~/Scripts/externals/jquery-ui-1.11.2.js",
                "~/Scripts/externals/jquery.validate.min.js",
                "~/Scripts/externals/jquery.unobtrusive*",
                "~/Content/themes/thirdParty/jquery.nanoscroller/jquery.nanoscroller.js",
#if DEBUG
 "~/Scripts/externals/knockout-3.4.0.debug.js",
#else
 "~/Scripts/externals/knockout-3.4.0.js",
#endif
                "~/Scripts/internals/salvisApi.js",
                "~/Scripts/internals/salvisUi.js",
                "~/Scripts/internals/salvis-basic.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/extended").Include(
                "~/Scripts/externals/modernizr-*"
             ));

            bundles.Add(new StyleBundle("~/bundles/basic/css")
                .Include("~/Content/themes/thirdParty/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                //.Include("~/Content/themes/fonts/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Content/themes/style.css", new CssRewriteUrlTransform())
                .Include("~/Content/themes/basic.css", new CssRewriteUrlTransform())
                .Include("~/Content/themes/thirdParty/jquery.nanoscroller/nanoscroller.css", new CssRewriteUrlTransform())
                .Include("~/Content/themes/pygments.css", new CssRewriteUrlTransform()));

            #endregion

            #region App internal bundles

            bundles.Add(new StyleBundle("~/bundles/extra/css")
               .Include("~/Content/themes/extra.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/apps").Include(
                "~/Scripts/MonthlyViewer.js"));

            bundles.Add(new ScriptBundle("~/bundles/apps/planners").Include(
                "~/Content/themes/thirdParty/jquery.select2/select2.min.js",
                "~/Content/themes/thirdParty/jquery.parsley/dist/parsley.js",
                "~/Content/themes/thirdParty/fuelux/loader.js"));

            bundles.Add(new ScriptBundle("~/bundles/goals/saving").Include(
                "~/Scripts/internals/goals/goals.js",
                "~/Scripts/internals/goals/saving.js"));

            bundles.Add(new ScriptBundle("~/bundles/goals/debt").Include(
                "~/Scripts/internals/goals/goals.js",
                "~/Scripts/internals/goals/debt.js"));

            bundles.Add(new ScriptBundle("~/bundles/goals/recurrent").Include(
                 "~/Scripts/internals/goals/goals.js",
                "~/Scripts/internals/goals/recurrent.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                "~/Scripts/internals/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/message/index")
                .Include("~/Scripts/internals/message.js"));

            bundles.Add(new ScriptBundle("~/bundles/library/tip")
                .Include("~/Scripts/internals/tips.js"));

            #endregion

            #region App external bundles

            bundles.Add(new StyleBundle("~/bundles/fuelux/css").Include(
                "~/Content/themes/thirdParty/fuelux/css/fuelux.css",
                "~/Content/themes/thirdParty/fuelux/css/fuelux-responsive.css"));

            bundles.Add(new ScriptBundle("~/bundles/datespicker").Include(
                "~/Content/themes/thirdParty/bootstrap.datetimepicker/js/bootstrap-datetimepicker.js",
                "~/Content/themes/thirdParty/bootstrap.datetimepicker/js/bootstrap-datetimepicker.min.js",
                "~/Content/themes/thirdParty/bootstrap.datetimepicker/js/locales/bootstrap-datetimepicker.es.js",
                "~/Content/themes/thirdParty/bootstrap.datetimepicker/js/locales/bootstrap-datetimepicker.en.js"));

            bundles.Add(new ScriptBundle("~/bundles/datesrange").Include(
                "~/Content/themes/thirdParty/bootstrap.daterangepicker/daterangepicker.js",
                "~/Content/themes/thirdParty/bootstrap.daterangepicker/moment.js",
                "~/Content/themes/thirdParty/bootstrap.daterangepicker/moment.min.js"));

            bundles.Add(new StyleBundle("~/bundles/datespicker/css").Include(
                "~/Content/themes/thirdParty/bootstrap.datetimepicker/css/bootstrap-datetimepicker.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/datesrange/css").Include(
               "~/Content/themes/thirdParty/bootstrap.daterangepicker/daterangepicker-bs3.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/charts/css").Include(
                "~/Content/themes/thirdParty/jquery.easypiechart/jquery.easy-pie-chart.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                "~/Content/themes/thirdParty/jquery.flot/jquery.flot.js",
                "~/Content/themes/thirdParty/jquery.flot/jquery.flot.categories.js",
                "~/Content/themes/thirdParty/jquery.flot/jquery.flot.pie.js",
                "~/Content/themes/thirdParty/jquery.flot/jquery.flot.resize.js",
                "~/Content/themes/thirdParty/jquery.flot/jquery.flot.labels.js",
                "~/Content/themes/thirdParty/jquery.easypiechart/jquery.easy-pie-chart.js",
                "~/Scripts/internals/charts.js"));

            bundles.Add(new ScriptBundle("~/bundles/gritter").Include(
                "~/Content/themes/thirdParty/jquery.gritter/js/jquery.gritter.js"));

            bundles.Add(new StyleBundle("~/bundles/gritter/css").Include(
                "~/Content/themes/thirdParty/jquery.gritter/css/jquery.gritter.css"));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                "~/Content/themes/thirdParty/jquery.icheck/icheck.js"));

            bundles.Add(new StyleBundle("~/bundles/icheck/css").Include(
                "~/Content/themes/thirdParty/jquery.icheck/skins/square/blue.css"));

            bundles.Add(new ScriptBundle("~/bundles/nestable").Include(
                "~/Content/themes/thirdParty/jquery.nestable/jquery.nestable.js"));

            bundles.Add(new ScriptBundle("~/bundles/parsley").Include(
                "~/Content/themes/thirdParty/jquery.parsley/dist/parsley.js",
                "~/Content/themes/thirdParty/jquery.parsley/src/i18n/en.js",
                "~/Content/themes/thirdParty/jquery.parsley/src/i18n/en.extra.js",
                "~/Content/themes/thirdParty/jquery.parsley/src/i18n/es.js",
                "~/Content/themes/thirdParty/jquery.parsley/src/i18n/es.extra.js",
                "~/Content/themes/thirdParty/jquery.parsley/src/extra/dateiso.js"));

            bundles.Add(new ScriptBundle("~/bundles/sparkline").Include(
                "~/Content/themes/thirdParty/jquery.sparkline/jquery.sparkline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/skycons").Include(
                "~/Content/themes/thirdParty/skycons/skycons.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                "~/Content/themes/thirdParty/jquery.datatables/jquery.datatables.min.js",
                "~/Content/themes/thirdParty/jquery.datatables/bootstrap-adapter/js/datatables.js"));

            bundles.Add(new StyleBundle("~/bundles/datatable/css").Include(
                "~/Content/themes/thirdParty/jquery.datatables/bootstrap-adapter/css/datatables.css"));

            bundles.Add(new ScriptBundle("~/bundles/niftymodals").Include(
                "~/Content/themes/thirdParty/jquery.niftymodals/js/jquery.modalEffects.js"));

            bundles.Add(new StyleBundle("~/bundles/niftymodals/css").Include(
                "~/Content/themes/thirdParty/jquery.niftymodals/css/component.css"));

            bundles.Add(new ScriptBundle("~/bundles/wysihtml5").Include(
                "~/Content/themes/thirdParty/bootstrap.wysihtml5/dist/wysihtml5-0.3.0.js"));

            bundles.Add(new StyleBundle("~/bundles/wysihtml5/css").Include(
                "~/Content/themes/thirdParty/bootstrap.wysihtml5/dist/bootstrap3-wysihtml5.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/rssfeed").Include(
                "~/Content/themes/thirdParty/rssfeed/jquery.zrssfeed.min.js"));

            bundles.Add(new StyleBundle("~/bundles/rssfeed/css").Include(
                "~/Content/themes/thirdParty/rssfeed/jquery.zrssfeed.css", new CssRewriteUrlTransform()));

            #endregion

            BundleTable.EnableOptimizations = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EnableOptimization"]);
        }
    }
}