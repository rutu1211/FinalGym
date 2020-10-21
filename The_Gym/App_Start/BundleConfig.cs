using System.Web;
using System.Web.Optimization;

namespace The_Gym
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new StyleBundle("~/Content/layout").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/dashboard-boxes").Include(
                      "~/Content/Dashboard.css"));

            bundles.Add(new ScriptBundle("~/bundles/Mainly-Scripts").Include(
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/popper.min.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.metisMenu.js",
                "~/Scripts/jquery.slimscroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Flot").Include(
                "~/Scripts/jquery.flot.js"));

            bundles.Add(new ScriptBundle("~/bundles/Peity").Include(
                "~/Scripts/peity-demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/Plugin").Include(
                "~/Scripts/inspinia.js",
                "~/Scripts/pace.min.js"));
        }
    }
}
