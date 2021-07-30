using System.Web;
using System.Web.Optimization;

namespace GroupProject.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/popper").Include(
            //            "~/Scripts/jsFrontend/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                "~/Scripts/umd/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/slick").Include(
                        "~/Scripts/slick.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/owl.carousel").Include(
                        "~/Scripts/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                        "~/Scripts/select2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/magnific-popup").Include(
                        "~/Scripts/jquery.magnific-popup.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/slick-animation").Include(
                        "~/Scripts/slick-animation.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                        "~/Scripts/jsFrontend/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/MainSearchScript").Include(
                        "~/Scripts/jsFrontend/MainSearchScript.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/cssfrontend/typography.css",
                "~/Content/cssfrontend/style.css",
                "~/Content/cssfrontend/responsive.css"
                      ));


        }
    }
}
