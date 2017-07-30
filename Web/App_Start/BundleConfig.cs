using System.Web.Optimization;

namespace PersonalBlog.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Script/Jquery").Include(
                "~/Content/vendor/jquery/jquery-1.9.1.js"));

            bundles.Add(new ScriptBundle("~/Script/Vendor").Include(
                "~/Content/vendor/bootstrap/js/bootstrap.js",
                "~/Content/vendor/summernote/summernote.js",
                "~/Content/vendor/summernote/typeahead.js"));

            bundles.Add(new ScriptBundle("~/Script/AppScripts").Include(
                "~/Content/scripts/*.js"
            ));

            bundles.Add(new StyleBundle("~/Style/Vendor").Include(
                "~/Content/vendor/bootstrap/css/bootstrap.css",
                "~/Content/vendor/font-awesome/css/font-awesome.css",
                "~/Content/vendor/summernote/summernote.css",
                "~/Content/global.css"));
        }
    }
}