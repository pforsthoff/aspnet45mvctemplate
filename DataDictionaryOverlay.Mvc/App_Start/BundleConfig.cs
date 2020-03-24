using System.Web;
using System.Web.Optimization;

namespace DataDictionaryOverlay
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-dialog.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/datatables").Include(
                "~/Scripts/datatables/jquery.dataTables.min.js",
                "~/Scripts/datatables/dataTables.buttons.min.js",
                "~/Scripts/datatables/dataTables.checkboxes.min.js",
                "~/Scripts/datatables/dataTables.cellEdit.js",
                "~/Scripts/datatables/jquery.dataTables.yadcf.js",
                "~/Scripts/datatables/dataTables.select.min.js",
                "~/Scripts/datatables/jquery.dataTables.min.js",
                "~/Scripts/datatables/dataTables.rowgroup.js",
                "~/Scripts/datatables/dataTables.order.neutral.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css/datatables").Include(
                "~/Content/datatables/datatables.min.css",
                "~/Content/datatables/dataTables.checkboxes.css",
                "~/Content/datatables/jquery.dataTables.yadcf.css",
                "~/Content/datatables/buttons.dataTables.min.css",
                "~/Content/datatables/select.dataTables.min.css"
            ));
        }
    }
}
