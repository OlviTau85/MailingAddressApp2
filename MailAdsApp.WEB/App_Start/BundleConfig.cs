using System.Web;
using System.Web.Optimization;

namespace MailAdsApp.WEB
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Css/site.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/Css/Bootstrap/bootstrap.css",
                "~/Content/Css/Bootstrap/bootstrap-theme.css",
                "~/Content/Css/Bootstrap/bootstrap-additions.css",
                "~/Content/Css/Bootstrap/angular-ranger.css",
                "~/Content/Css/Bootstrap/ui-bootstrap-csp.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-animate.js",
                "~/Scripts/Angular/angular-touch.js",
                "~/Scripts/Angular/angular-locale_ru-ru.js",
                "~/Scripts/Angular/angular-ranger.js"
            ));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Bootstrap/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular-strap").Include(
                "~/Scripts/Angular/angular-strap/angular-strap.js",
                "~/Scripts/Angular/angular-strap/angular-strap.tpl.js",
                "~/Scripts/Angular/angular-strap/date-parser.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/angular-ui").Include(
                "~/Scripts/Angular/angular-ui/ui-bootstrap.js",
                "~/Scripts/Angular/angular-ui/ui-bootstrap-tpls.js"
            ));
        }
    }
}