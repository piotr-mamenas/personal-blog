using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PersonalBlog.Web.Helpers;

namespace PersonalBlog.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MapProfile.Map();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            TemporaryDatabaseSeeder.Seed();
        }
    }
}
