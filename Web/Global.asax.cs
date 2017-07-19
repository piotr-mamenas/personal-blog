using System;
using System.Diagnostics;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using PersonalBlog.Web.Controllers;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Facades;
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
            log4net.Config.XmlConfigurator.Configure();
            TemporaryDatabaseSeeder.Seed();

            var log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Debug("Hello");

            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
                FormsAuthentication.SignOut();
            }
        }

        protected void Application_BeginRequest()
        {
            if (HttpContext.Current.Session != null)
            {
                Response.Headers.Add("Refresh", Convert.ToString(Session.Timeout * 5));
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Trace.WriteLine("Application Error Encountered");

            var httpContext = ((MvcApplication) sender).Context;

            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            var currentController = " ";
            var currentAction = " ";

            if (currentRouteData != null)
            {
                if (!string.IsNullOrEmpty(currentRouteData.Values["controller"]?.ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (!string.IsNullOrEmpty(currentRouteData.Values["action"]?.ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = Server.GetLastError();

            if (ex != null)
            {
                Trace.WriteLine(ex.Message);

                if (ex.InnerException != null)
                {
                    Trace.WriteLine(ex.InnerException);
                    Trace.WriteLine(ex.InnerException.Message);
                }
            }

            var controller = new ErrorController();
            var routeData = new RouteData();
            var action = "ApplicationError";
            var statusCode = 500;

            //TODO: Need to pass relevant values here and handle error type determination
            var facade = ErrorHandlingFacade.Instance.GetErrorType(new BaseError
            {
                RedirectAction = "",
                RedirectController = "",
                Exception = ex,
                StatusCode = 500
            });

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }
    }
}
