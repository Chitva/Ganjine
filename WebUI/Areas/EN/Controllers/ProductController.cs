using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Domain.Validation.Admin;
using Repository.Abstract;
using Domain.ViewModel.User;

namespace WebUI.Areas.EN.Controllers
{
    public class ProductController : Controller
    {
      
        IUnitOfWork _uow;
        IServiceRepository _RService;
        IWorkExperienceRepository _RWorkExperience;
        IFooterRepository _RFooter;
        IProductOrderRepository _rOrder;
        List<ProductLinksViewModel> menuNodes = new List<ProductLinksViewModel>();


        public ProductController(IUnitOfWork uow ,
            IFooterRepository FooterRepository, IWorkExperienceRepository WorkExperienceRepository,
            IServiceRepository ServiceRepository, IProductOrderRepository orderRepository
            )
        {
            _uow = uow;
            _RService = ServiceRepository;
            _RFooter = FooterRepository;
            _RWorkExperience = WorkExperienceRepository;
            _rOrder = orderRepository;
        }
        [HttpPost]
        public ActionResult GetImageGalleryList(int id)
        {
            ViewBag.ImageGalleryList = _RService.ServiceTabFiles.Where(x => x.ServiceTabId == id).ToList();
            return Json(new { Partial = RenderPartialViewToString("_ImageGalleryService", null) }, JsonRequestBehavior.AllowGet);
        }

        //serviceGroup ViewBag modified by Azizi 
        public ActionResult Index(int id = 0, string Title = "")
        {
            if (_RService.ServiceTabs.Any(_ => _.ServiceGroupId == id) || id == 0)
            {
                InitializeData();
                if (id == 0)
                {
                    var list = _RService.ServiceGroups;
                    if (list.Any())
                    {
                        id = list.FirstOrDefault(_ => !list.Any(s => s.ParentID == _.Id)).Id;
                    }
                }
                ViewBag.catids = id;
                var ServiceGroup = _RService.ServiceGroups.Where(x => x.LanguageId == 2).ToList();
                ViewBag.ServiceGroup = ServiceGroup;
                var result = ServiceGroup.Where(p => (ServiceGroup.Any(p2 => p2.Id == p.ParentID) || p.ParentID == null) && p.ServiceTab.Count > 0).ToList();


                if (result.Count > 0)
                {

                    List<string> Nav = new List<string>();
                    int Groupint = id;
                    while (Groupint != 0)
                    {
                        ServiceGroup ServiceGroup1 = ServiceGroup.FirstOrDefault(x => x.Id == Groupint);
                        if (ServiceGroup1 != null)
                        {
                            Nav.Add(ServiceGroup1.Name);
                            Groupint = Convert.ToInt32(ServiceGroup1.ParentID);
                        }
                        else
                            break;
                    }
                    ViewBag.Nav = Nav;

                    var ServiceTabList = _RService.ServiceTabs.Where(x => x.ServiceGroupId == id).ToList();
                    if (ServiceTabList.Count > 0 && ServiceTabList[0].TabType == 2)
                    {
                        int ids = ServiceTabList[0].Id;
                        ViewBag.ImageGalleryList = _RService.ServiceTabFiles.Where(x => x.ServiceTabId == ids).ToList();
                    }
                    ViewBag.Tabs = ServiceTabList;

                }
                if (ServiceGroup.Any(_ => _.Id == id))
                    FillingRelatedViewBags(id);

                if (TempData["Msg"] != null && !string.IsNullOrEmpty(TempData["Msg"].ToString()))
                {
                    ViewBag.Msg = TempData["Msg"].ToString();
                }

                return View();
            }
            else
                return HttpNotFound();
        }

        //Azizi
        public ActionResult OrderRegistration()
        {
            InitializeData();
            return PartialView("_OrderRegistration");

        }

        //Azizi
        [HttpPost]
        [CaptchaValidator]
        public ActionResult OrderRegistration(OrderValidation orderValidation, bool captchaIsValid = true)
        {
            var title = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == orderValidation.ServiceGroupId)?.Name;

            if (captchaIsValid)
            {
                InitializeData();

                ProductOrder obj = new ProductOrder()
                {
                    Name = orderValidation.Name,
                    Family = orderValidation.Family,
                    IsMale = orderValidation.IsMale,
                    Tell = orderValidation.Tell,
                    LanguageId = 2,
                    Address = orderValidation.Address,
                    Email = orderValidation.Email,
                    OrderDate = DateTime.Now.Date,
                    OrderText = orderValidation.OrderText,
                    ServiceGroupId = orderValidation.ServiceGroupId,
                    OrderStatus = OrderStatus.New
                };
                _rOrder.SaveProductOrder(obj);
                TempData["Msg"] = "1";

                return RedirectToAction("index", new { id = orderValidation.ServiceGroupId, title = title });
            }

            TempData["Msg"] = "2";

            return RedirectToAction("index", new { id = orderValidation.ServiceGroupId, title = title });
        }

        [HttpPost]
        public ActionResult GetList(int Id)
        {
            var ServiceGroup = _RService.ServiceGroups.Where(x => x.LanguageId == 2).ToList();
            List<string> Nav = new List<string>();
            int Groupint = Id;
            while (Groupint != 0)
            {
                ServiceGroup ServiceGroup1 = ServiceGroup.FirstOrDefault(x => x.Id == Groupint);
                Nav.Add(ServiceGroup1.Name);
                Groupint = Convert.ToInt32(ServiceGroup1.ParentID);
            }
            ViewBag.Nav = Nav;


            var ServiceTabList = _RService.ServiceTabs.Where(x => x.ServiceGroupId == Id).ToList();
            if (ServiceTabList != null && ServiceTabList.Count > 0 && ServiceTabList[0].TabType == 2)
            {
                int id = ServiceTabList[0].Id;
                ViewBag.ImageGalleryList = _RService.ServiceTabFiles.Where(x => x.ServiceTabId == id).ToList();
            }
            ViewBag.Tabs = ServiceTabList;
            return Json(new
            {
                Partial = RenderPartialViewToString("_Service", null),
                Partial1 = RenderPartialViewToString("_Nav", null)
            }, JsonRequestBehavior.AllowGet);
        }

        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public void InitializeData()
        {
            ViewBag.FooterColName = _RFooter.FooterColumnNames.Where(x => x.Name.Length > 0 && x.LanguageId == 2).OrderByDescending(x => x.Id).ToList();
            var x1 = _RService.ServiceGroups.Where(x => x.LanguageId == 2);
            if (x1.Any())
                ViewBag.ServiceGroup = x1.ToList();

            var x2 = _RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == 2);
            if (x2.Any())
                ViewBag.WorkExGroup = x2.ToList();
           

            var x7 = _uow.Set<ServiceTab>();
            if (x7.Any())
                ViewBag.ServiceTabs = x7.ToList();
        }


        //written By Azizi 94_08_25
        private void FillingRelatedViewBags(int serviceGroupId)
        {
            string description = string.Empty;


            var serviceGroup = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == serviceGroupId);
            var servicetabs = _RService.ServiceTabs.Where(_ => _.ServiceGroupId == serviceGroupId).ToList();
            if (!string.IsNullOrEmpty(serviceGroup.metaDescription))
            {
                ViewBag.MetaDescription = serviceGroup.metaDescription;
            }
            else if (servicetabs.Any())
            {
                if (servicetabs.Any())
                {
                    description = StripHTML(servicetabs.FirstOrDefault().TabTypeText);
                    ViewBag.MetaDescription = description.Length > 1000
                        ? description.Substring(0, 1000)
                        : description.Substring(0, description.Length);
                }
            }

            if (!string.IsNullOrEmpty(serviceGroup.Title))
            {
                ViewBag.Title = serviceGroup.Title;

            }
            else
            {
                ViewBag.Title = serviceGroup.Name;
            }


            if (!string.IsNullOrEmpty(serviceGroup.keywords))
            {
                ViewBag.MetaKeywords = serviceGroup.keywords;

            }
            else
            {
                foreach (var tab in servicetabs)
                {
                    if (!string.IsNullOrEmpty(tab.Tags))
                        ViewBag.MetaKeywords += tab.Tags + ",";
                }

            }
        }


        //written By Azizi 94_08_25
        public static string StripHTML(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }
    }
}
