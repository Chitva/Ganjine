using System.Web.Mvc;

namespace WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
           "Admin_default01",
           "Admin/Gallery/GalleryList/{id}/{prev}",
           new { action = "GalleryList", controller = "Gallery", id = UrlParameter.Optional, prev = UrlParameter.Optional }
       );

          
            //////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
           "Commerce_01",
           "Admin/Commerce/CommerceList/{id}/{prev}",
           new
           {
               action = "CommerceList",
               controller = "Commerce",
               id = UrlParameter.Optional,
               prev = UrlParameter.Optional
           }

       );
        /////////////////////////////////////////////////////////////////
            context.MapRoute(
          "RssFeeds",
          "Admin/RssFeedUrl/RssFeedUrlList/{page}",
          new
          {
              action = "RssFeedUrlList",
              controller = "RssFeedUrl",
              page = UrlParameter.Optional
          }

      );
            ////////////////////////////////////////////////////////////////////////////////////

            context.MapRoute(
                "Admin_default11",
                "Admin/{controller}/{action}/{id}/{*Extparam}",
                new { action = "Login", controller = "Home", id = UrlParameter.Optional }
            );
            context.MapRoute("by-month",
"Admin/{controller}/{action}/{id}",
new
{
    controller = "Home",
    action = "Login",
    id = UrlParameter.Optional
}
);     
        }
    }
}
