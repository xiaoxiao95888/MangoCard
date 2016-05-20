using System.Web;
using System.Web.Optimization;

namespace Mango_Cards.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            //main
            bundles.Add(new ScriptBundle("~/bundles/Main").Include(
                "~/Scripts/JS/Main.js", "~/Scripts/jquery.easing.1.3.js"));
            //moment
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment-with-locales.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/font-awesome.css"
                ));
            //coverr
            bundles.Add(new StyleBundle("~/Content/coverr").Include(
                      "~/Content/coverr.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.2.0.js",
                "~/Scripts/knockout.mapping-latest.js"));

            //header
            bundles.Add(new ScriptBundle("~/bundles/header").Include(
                "~/Scripts/coverr.js", "~/Scripts/js/header.js"));
            //login
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
               "~/Scripts/jquery.qrcode.js",
               "~/Scripts/qrcode.js",
               "~/Scripts/JS/login.js"));
            //Home
            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
                "~/Scripts/jquery.qrcode.js",
                "~/Scripts/imagesloaded.pkgd.js",
                "~/Scripts/isotope.pkgd.js",
                "~/Scripts/qrcode.js",
                "~/Scripts/JS/Home.js"));
            //jquery-qrcode
            bundles.Add(new ScriptBundle("~/bundles/jqueryqrcode").Include(
                        "~/Scripts/jquery.qrcode.min.js", "~/Scripts/qrcode.min.js"));
            //GetMangoCard
            bundles.Add(new ScriptBundle("~/bundles/GetMangoCard").Include(
                      "~/Scripts/JS/GetMangoCard.js"));
            //LoginConfirmation
            bundles.Add(new ScriptBundle("~/bundles/LoginConfirmation").Include(
                      "~/Scripts/JS/LoginConfirmation.js"));
            //Cards
            bundles.Add(new ScriptBundle("~/bundles/cards").Include(
                "~/Scripts/imagesloaded.pkgd.js",
                "~/Scripts/isotope.pkgd.js",
                "~/Scripts/clipboard.js",
                "~/Scripts/jquery.nailthumb.1.1.js",
                "~/Scripts/codemirror.js",
                "~/Scripts/mode/javascript/javascript.js",
                "~/Scripts/mode/css/css.js",
                "~/Scripts/mode/xml/xml.js",
                "~/Scripts/mode/htmlmixed/htmlmixed.js",
                "~/Scripts/JS/Cards.js"));
            //Cards
            bundles.Add(new StyleBundle("~/Content/Cards").Include(
                "~/Content/codemirror.css",
                "~/Content/theme/ambiance.css",
                 "~/Content/card.css"
                ));

        }
    }
}
