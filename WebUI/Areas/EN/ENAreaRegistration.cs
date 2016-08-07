using System.Web.Mvc;

namespace WebUI.Areas.EN
{
    public class ENAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EN";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
            name: "AboutUsTypeEN",
            url: "EN/AboutUs/{type}",
            defaults: new { controller = "Home", action = "AboutUs", type = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "EmploymentPageEN",
               url: "EN/Employment",
               defaults: new { controller = "Home", action = "Employment", type = UrlParameter.Optional },
               namespaces: new[] { "WebUI.Areas.EN.Controllers" }
               );

            context.MapRoute(
                 name: "CooperatePageEN",
                url: "EN/Cooperate",
                defaults: new { controller = "Home", action = "Cooperate", type = UrlParameter.Optional },
                namespaces: new[] { "WebUI.Areas.EN.Controllers" }
                );

            context.MapRoute(
            name: "ArticlePageEN",
            url: "EN/Article/{Page}",
            defaults: new { controller = "Article", action = "Index", Page = UrlParameter.Optional }
        );
            context.MapRoute(
           name: "ArticleDetailEN",
           url: "EN/Article/{Id}/{Title}",
           defaults: new { controller = "Article", action = "Detail", Page = UrlParameter.Optional }
       );
   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    context.MapRoute(
        name: "GalleryGetListEN",
        url: "EN/Gallery/GetList/{Id}",
        defaults: new { controller = "Gallery", action = "GetList", Id = UrlParameter.Optional }
        );
                    context.MapRoute(
                name: "GalleryMenuEN",
                url: "EN/Gallery/{Id}/{Title}",
                defaults: new { controller = "Gallery", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
            );
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                    context.MapRoute(
        name: "LabratoryGetListEN",
        url: "EN/Labratory/GetList/{Id}",
        defaults: new { controller = "Labratory", action = "GetList", Id = UrlParameter.Optional }
        );
                    context.MapRoute(
                name: "LabratoryMenuEN",
                url: "EN/Labratory/{Id}/{Title}",
                defaults: new { controller = "Labratory", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
                
            );
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
                        name: "ENLinkableProducts",
                       url: "EN/Category/{serviceGroupId}/{name}",
                       defaults: new { controller = "Category", action = "GetRootLeaves", serviceGroupId = UrlParameter.Optional, name = UrlParameter.Optional }
                       );
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
     name: "UnitGetListEN",
        url: "EN/Unit/GetList/{Id}",
        defaults: new { controller = "Unit", action = "GetList", Id = UrlParameter.Optional }
        );
                    context.MapRoute(
                name: "UnitMenuEN",
                url: "EN/Unit/{Id}/{Title}",
                defaults: new { controller = "Unit", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
            );
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
   name: "NewsPage1",
   url: "EN/News/{Page}",
   defaults: new { controller = "News", action = "Index", Page = UrlParameter.Optional }
);
            context.MapRoute(
           name: "NewsDetail2",
           url: "EN/News/{Id}/{Title}",
           defaults: new { controller = "News", action = "Detail", Page = UrlParameter.Optional }
       );
/////////////////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
            name: "ProductGetList1EN",
            url: "EN/Product/GetImageGalleryList/{Id}",
            defaults: new { controller = "Product", action = "GetImageGalleryList", Id = UrlParameter.Optional }
            );

            context.MapRoute(
            name: "ProductGetListEN",
            url: "EN/Product/GetList/{Id}",
            defaults: new { controller = "Product", action = "GetList", Id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "OrdersEN",
             url: "EN/Product/OrderRegistration/{Id}",
             defaults: new { controller = "Product", action = "OrderRegistration", Id = UrlParameter.Optional }
             , namespaces: new[] { "WebUI.Areas.EN.Controllers" }
             );

            context.MapRoute(
            name: "ProductMenuEN",
            url: "EN/Product/{Id}/{Title}",
            defaults: new { controller = "Product", action = "Index", Id = UrlParameter.Optional, Title = UrlParameter.Optional }
            );
//////////////////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
                name: "ProfileEN",
                url: "EN/Profile/Details/{Id}/{Title}",
               defaults: new { controller = "Profile", action = "Details",
                   Id = UrlParameter.Optional, Title = UrlParameter.Optional }
               );
///////////////////////////////////////////////////////////////////////////////////////////////////////
              context.MapRoute(
                name: "FaqENRoute",
                url: "EN/FAQs/{Action}/{Id}",
               defaults: new { controller = "Faq", action = "Index", Id = UrlParameter.Optional }
               );
            ////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
                        name: "ENCompanyHistory",
                           url: "EN/Introduction/CompanyHistory",
                          defaults: new { controller = "Home", action = "Introduction", type = "CompanyHistory" }
                         
                        );
            context.MapRoute(
          name: "ENRajiGazSpecificationPage",
             url: "EN/Introduction/RajiSpecifications",
            defaults: new { controller = "Home", action = "Introduction", type = "RajiSpecifications" }
           
          );
            context.MapRoute(
           name: "ENCertificates",
              url: "EN/Introduction/Certificates",
             defaults: new { controller = "Home", action = "Introduction", type = "Certificates" }
          
           );
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            context.MapRoute(
                "EN_default",
                "EN/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}