using System.Web;
using System.Web.Optimization;

namespace FastService
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/assets/global/plugins/jquery.min.js",
                        "~/Scripts/jquery-ui.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery.ui").Include(
                       "~/Scripts/jquery-ui.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-ui.structure.min.css",
                      "~/Content/jquery-ui.theme.min.css",
                      "~/css/hover.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/global/css/components.min.css",
                      "~/Content/assets/global/css/plugins.min.css",
                      "~/Content/assets/pages/css/pricing.min.css",
                      "~/Content/assets/layouts/layout5/css/layout.min.css",
                      "~/Content/assets/layouts/layout5/css/custom.min.css",
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/pages/css/profile-2.min.css",
                      "~/Content/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css",
                      "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css",
                      "~/Content/assets/global/plugins/morris/morris.css",
                      "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css",
                      "~/Content/assets/global/plugins/jqvmap/jqvmap/jqvmap.css",
                      "~/Content/assets/global/css/components.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                       "~/Content/assets/global/plugins/js.cookie.min.js",
                       "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                       "~/Content/assets/global/plugins/jquery.blockui.min.js",
                       "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                       "~/Content/assets/global/plugins/moment.min.js",
                       "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                       "~/Content/assets/global/plugins/morris/morris.min.js",
                       "~/Content/assets/global/plugins/morris/raphael-min.js",
                       "~/Content/assets/global/plugins/counterup/jquery.waypoints.min.js",
                       "~/Content/assets/global/plugins/counterup/jquery.counterup.min.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/amcharts.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/serial.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/pie.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/radar.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/themes/light.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/themes/patterns.js",
                       "~/Content/assets/global/plugins/amcharts/amcharts/themes/chalk.js",
                       "~/Content/assets/global/plugins/amcharts/ammap/ammap.js",
                       "~/Content/assets/global/plugins/amcharts/ammap/maps/js/worldLow.js",
                       "~/Content/assets/global/plugins/amcharts/amstockcharts/amstock.js",
                       "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js",
                       "~/Content/assets/global/plugins/horizontal-timeline/horozontal-timeline.min.js",
                       "~/Content/assets/global/plugins/flot/jquery.flot.min.js",
                       "~/Content/assets/global/plugins/flot/jquery.flot.resize.min.js",
                       "~/Content/assets/global/plugins/flot/jquery.flot.categories.min.js",
                       "~/Content/assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
                       "~/Content/assets/global/plugins/jquery.sparkline.min.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/jquery.vmap.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
                       "~/Content/assets/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js",
                       "~/Content/assets/global/scripts/app.min.js",
                       "~/Content/assets/pages/scripts/dashboard.min.js",
                       "~/Content/assets/layouts/layout5/scripts/layout.min.js",
                       "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js"
                       ));
        }
    }
}
