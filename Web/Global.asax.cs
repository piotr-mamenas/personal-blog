﻿using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
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
            Trace.WriteLine("Exception Occurred");

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
        }
    }
}
