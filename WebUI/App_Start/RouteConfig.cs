
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("sitemap.xml", "sitemap.xml", new { controller = "Site", action = "SitemapXml" });

            routes.MapRoute("robots", "robots.txt", new { controller = "Site", action = "RobotsTxt" });
            routes.MapRoute(
                 name: "testSendEmail",
                 url: "TestSnd/Index",
                defaults: new { controller = "TestSnd", action = "Index", type = UrlParameter.Optional }
                , namespaces: new[] { "WebUI.Controllers" }
                );
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
            name: "VideoGroup",
            url: "Video/GetVideos/{title}/{id}",
            defaults: new { controller = "Video", action = "GetVideos", id = UrlParameter.Optional,  title = UrlParameter.Optional }
          , namespaces: new[] { "WebUI.Controllers" }
                      );
            ////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                   name: "ContactUs",
                   url: "ContactUs",
                  defaults: new { controller = "Home", action = "ContactUs", type = UrlParameter.Optional }
                  , namespaces: new[] { "WebUI.Controllers" }
                  );

            //////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
             name: "RajiGazSpecificationPage",
                url: "Introduction/RajiSpecifications",
               defaults: new { controller = "Home", action = "Introduction", type = "RajiSpecifications" }
               , namespaces: new[] { "WebUI.Controllers" }
             );
            ///////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
               name: "CompanyHistory",
                  url: "Introduction/CompanyHistory",
                 defaults: new { controller = "Home", action = "Introduction", type = "CompanyHistory" }
                 , namespaces: new[] { "WebUI.Controllers" }
               );
            //////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
               name: "MissionStatement",
                  url: "Introduction/MissionStatement",
                 defaults: new { controller = "Home", action = "Introduction", type = "MissionStatement" }
                 , namespaces: new[] { "WebUI.Controllers" }
               );
            ///////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
               name: "Perspective",
                  url: "Introduction/Perspective",
                 defaults: new { controller = "Home", action = "Introduction", type = "Perspective" }
                 , namespaces: new[] { "WebUI.Controllers" }
               );
            ////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
               name: "Awards",
                  url: "Introduction/Award",
                 defaults: new { controller = "Home", action = "Introduction", type = "Award" }
                 , namespaces: new[] { "WebUI.Controllers" }
               );
            ///////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
               name: "Certificates",
                  url: "Introduction/Certificates",
                 defaults: new { controller = "Home", action = "Introduction", type = "Certificates" }
                 , namespaces: new[] { "WebUI.Controllers" }
               );
            ///////////////////////////////////////////////////////////////////////////////
          
            routes.MapRoute(
                 name: "LinkableProducts",
                url: "Category/{serviceGroupId}/{name}",
                defaults: new { controller = "Category", action = "GetRootLeaves", serviceGroupId = UrlParameter.Optional ,name = UrlParameter.Optional },
                namespaces: new[] { "WebUI.Controllers" }
                );
            ////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                   name: "AboutUsType",
                   url: "AboutUs",
                  defaults: new { controller = "Home", action = "AboutUs", type = UrlParameter.Optional }
                  , namespaces: new[] { "WebUI.Controllers" }
                  );
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                 name: "RajiStory",
                 url: "RajiStory",
                defaults: new { controller = "Home", action = "RajiStory", type = UrlParameter.Optional }
                , namespaces: new[] { "WebUI.Controllers" }
                );
            ////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                  name: "SaleAgencyPage",
                  url: "SaleAgency",
                 defaults: new { controller = "SaleAgency", action = "Index" }
                 , namespaces: new[] { "WebUI.Controllers" }
                 );
            /////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                            name: "GalleryGetList",
                            url: "Gallery/GetList/{Id}",
                            defaults: new { controller = "Gallery", action = "GetList", Id = UrlParameter.Optional }
                           , namespaces: new[] { "WebUI.Controllers" }
                         );

            routes.MapRoute(
        name: "GalleryMenu",
        url: "Gallery/{Id}/{Title}",
        defaults: new { controller = "Gallery", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
        , namespaces: new[] { "WebUI.Controllers" }
    );
           ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
             routes.MapRoute(
                            name: "CommerceList",
                            url: "Commerce/CommerceList/{Id}",
                            defaults: new { controller = "Commerce", action = "CommerceList", Id = UrlParameter.Optional }
                           , namespaces: new[] { "WebUI.Controllers" }
                         );

            routes.MapRoute(
        name: "CommerceMenu",
        url: "Commerce/{Id}/{Title}",
        defaults: new { controller = "Commerce", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
        , namespaces: new[] { "WebUI.Controllers" }
    );
     /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                            name: "ResearchList",
                            url: "ResearchDevelopment/ResearchDevelopmentList/{Id}",
                            defaults: new { controller = "ResearchDevelopment", action = "ResearchDevelopmentList", Id = UrlParameter.Optional }
                           , namespaces: new[] { "WebUI.Controllers" }
                         );

            routes.MapRoute(
        name: "ResearchDevelopmentMenu",
        url: "ResearchDevelopment/{Id}/{Title}",
        defaults: new { controller = "ResearchDevelopment", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
        , namespaces: new[] { "WebUI.Controllers" }
    );
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                             name: "StructureList",
                             url: "OrganizationStructure/OrganizationStructureList/{Id}",
                             defaults: new { controller = "OrganizationStructure", action = "OrganizationStructureList", Id = UrlParameter.Optional }
                            , namespaces: new[] { "WebUI.Controllers" }
                          );

            routes.MapRoute(
        name: "OrganizationStructureMenu",
        url: "OrganizationStructure/{Id}/{Title}",
        defaults: new { controller = "OrganizationStructure", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
        , namespaces: new[] { "WebUI.Controllers" }
    );      
    ///////////////////////////////////////////////////////////////////////////////////////////////////  

            routes.MapRoute(
name: "ProductGetList1",
url: "Product/GetImageGalleryList/{Id}",
defaults: new { controller = "Product", action = "GetImageGalleryList", Id = UrlParameter.Optional }
, namespaces: new[] { "WebUI.Controllers" }
);

            routes.MapRoute(
name: "ProductGetList",
url: "Product/GetList/{Id}",
defaults: new { controller = "Product", action = "GetList", Id = UrlParameter.Optional }
, namespaces: new[] { "WebUI.Controllers" }
);
            routes.MapRoute(
                name: "Orders",
               url: "Product/OrderRegistration/{Id}",
               defaults: new { controller = "Product", action = "OrderRegistration", Id = UrlParameter.Optional }
               , namespaces: new[] { "WebUI.Controllers" }
               );

            routes.MapRoute(
        name: "ProductMenu",
        url: "Product/{Id}/{Title}",
        defaults: new { controller = "Product", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
        , namespaces: new[] { "WebUI.Controllers" }
    );
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
           name: "ArticlePage",
           url: "Article/{Page}",
           defaults: new { controller = "Article", action = "Index", Page = UrlParameter.Optional }
           , namespaces: new[] { "WebUI.Controllers" }
       );

            routes.MapRoute(
           name: "ArticleDetail",
           url: "Article/{Id}/{Title}",
           defaults: new { controller = "Article", action = "Detail", Page = UrlParameter.Optional }
           , namespaces: new[] { "WebUI.Controllers" }
       );

//////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
             name: "StoryPage",
             url: "Story/{Page}",
             defaults: new { controller = "Story", action = "Index", Page = UrlParameter.Optional }
             , namespaces: new[] { "WebUI.Controllers" }
         );


            routes.MapRoute(
           name: "StoryDetail",
           url: "Story/{Id}/{Title}",
           defaults: new { controller = "Story", action = "Detail", Page = UrlParameter.Optional }
           , namespaces: new[] { "WebUI.Controllers" }
       );
 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
           name: "NewsPage",
           url: "News/{Page}",
           defaults: new { controller = "News", action = "Index", Page = UrlParameter.Optional }
           , namespaces: new[] { "WebUI.Controllers" }
       );


            routes.MapRoute(
           name: "NewsDetail",
           url: "News/{Id}/{Title}",
           defaults: new { controller = "News", action = "Detail", Page = UrlParameter.Optional }
           , namespaces: new[] { "WebUI.Controllers" }
       );
 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , namespaces: new[] { "WebUI.Controllers" }
            );
  ///////////////////////////////////////////////////////////////////////////////////////////
            
        }

        
    }
}