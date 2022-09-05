using System.Web.Optimization;

namespace ProfessorCourse_BestFit
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/chosen.jquery"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                        "~/Content/plugins/jquery/jquery.min.js",
                        "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
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
                        "~/Content/plugins/datatables-buttons/js/buttons.colVis.min.js",
                        "~/Content/dist/js/adminlte.min.js",
                        "~/Content/plugins/sweetalert2/sweetalert2.min.js",
                        "~/Content/dist/js/demo.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-icons/font/bootstrap-icons.min.css",
                      "~/Content/bootstrapMinty.min.css",
                      "~/Content/bootstrap-chosen.css",
                      "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/dist/css/adminlte.min.css",
                      "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                      "~/Content/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css",
                      "~/Content/site.css"));
        }
    }
}
