using System.Web.Optimization;

namespace ProfessorCourse_BestFit
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(

                        "~/Content/plugins/jquery/jquery.min.js",


                        "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
                                                "~/Content/plugins/select2/js/select2.full.min.js",
                                                                        "~/Content/plugins/sweetalert2/sweetalert2.min.js",

                        "~/Content/plugins/chart.js/Chart.min.js",
                        "~/Content/plugins/sparklines/sparkline.js",
                        "~/Content/plugins/jqvmap/jquery.vmap.min.js",
                        "~/Content/plugins/jqvmap/maps/jquery.vmap.usa.js",
                        "~/Content/plugins/jquery-knob/jquery.knob.min.js",
                        "~/Content/plugins/moment/moment.min.js",
                        "~/Content/plugins/daterangepicker/daterangepicker.js",
                        "~/Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                        "~/Content/plugins/summernote/summernote-bs4.min.js",
                        "~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                        "~/Content/dist/js/adminlte.min.js",
                        "~/Content/dist/js/demo.js",
                        "~/Content/dist/js/pages/dashboard.js",

                        "~/Content/plugins/datatables/jquery.dataTables.min.js",
                        "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                        "~/Content/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                        "~/Content/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                        "~/Content/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                        "~/Content/plugins/jszip/jszip.min.js",
                        "~/Content/plugins/pdfmake/pdfmake.min.js",
                        "~/Content/plugins/pdfmake/vfs_fonts.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.html5.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.print.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.colVis.min.js"









                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/plugins/select2/css/select2.min.css",
                 "~/Content/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css",

                      "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",



                      "~/Content/dist/css/adminlte.min.css",
                      "~/Content/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                      "~/Content/plugins/jqvmap/jqvmap.min.css",
                      "~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                      "~/Content/plugins/daterangepicker/daterangepicker.css",
                      "~/Content/plugins/summernote/summernote-bs4.min.css",
                      "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                      "~/Content/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css",


                      "~/Content/site.css"));
        }
    }
}
