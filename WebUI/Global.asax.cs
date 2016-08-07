using DataLayer.Context;
using System;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI.Infrastructure;

namespace WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
       
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (!Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
            {
                UriBuilder builder = new UriBuilder(Request.Url);
                builder.Host = "www." + Request.Url.Host;
                Response.Redirect(builder.ToString(), true);
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
          //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }

        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            string currentUrl = HttpContext.Current.Request.Path.ToLower();
            if (currentUrl.StartsWith("http://tandistalaei.com"))
            {
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", currentUrl.Replace("http://tandistalaei.com", "http://www.tandistalaei.com"));
                Response.End();
            }
        }
    }
}