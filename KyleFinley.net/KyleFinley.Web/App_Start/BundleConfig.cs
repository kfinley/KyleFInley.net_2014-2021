using System.Web.Optimization;

namespace KyleFinley.Web {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/scripts/bootstrap.js",
                      "~/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/content/bootstrap.css",
                      "~/content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                "~/content/scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/analytics").Include(
                "~/content/scripts/analytics.js"));
            
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
