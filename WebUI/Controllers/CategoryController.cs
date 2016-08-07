using DataLayer.Context;
using Domain.ViewModel.User;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class CategoryController : Controller
    {
        List<ProductLinksViewModel> menuNodes = new List<ProductLinksViewModel>();
        IServiceRepository _RService;
        IUnitOfWork _uow;
        public CategoryController(IServiceRepository ServiceRepository ,IUnitOfWork uow)
        {
            _RService = ServiceRepository;
            _uow = uow;
        }

        public void InitializeData()
        {
            ViewBag.FooterColName = _uow.Set<FooterColumnName>().Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
            var x1 = _RService.ServiceGroups.Where(x => x.LanguageId == 1);
            if (x1.Any())
                ViewBag.ServiceGroup = x1.ToList();

            var x2 = _uow.Set<WorkExperienceGroup>().Where(x => x.LanguageId == 1);
            if (x2.Any())
                ViewBag.WorkExGroup = x2.ToList();
          
            var x7 = _uow.Set<ServiceTab>();
            if (x7.Any())
                ViewBag.ServiceTabs = x7.ToList();
        }

        //written by Azizi 95_03_10
        public ActionResult GetRootLeaves(int ServiceGroupId, string name = "")
        {
            InitializeData();
            ViewBag.Categories = _RService.ServiceGroups.Where(_ => _.LanguageId == 1)
                .Where(_ => _RService.ServiceGroups.Any(s => s.ParentID == _.Id) && _.GrandsonsCount > 0).OrderBy(_ => _.Id).ToList();
            GetChilds(ServiceGroupId);
            return View(menuNodes);
        }

        //written by Azizi 95_03_10
        private void GetChilds(int? rootID)
        {
            IEnumerable<ServiceGroup> childs;
            if (rootID != null)
                childs = _RService.ServiceGroups.Where(_ => _.ParentID == rootID);
            else
                childs = _RService.ServiceGroups.Where(_ => _.ParentID == null);

            foreach (var child in childs)
            {
                //check whether it has any tab or not
                var tabs = _RService.ServiceTabs.Where(_ => _.ServiceGroupId == child.Id);

                //if it has any tab so generate a link for this serviceGroupId
                if (tabs.Any())
                {
                    var firstTab = tabs.FirstOrDefault(_ => _.Name == "شرح محصول");
                 
                    var vm = new ProductLinksViewModel()
                    {
                        ProductGroupId = child.Id,
                        ProductName = firstTab.Title,
                        Price = firstTab.Price,
                        Discount = firstTab.Discount,
                        IsExist = firstTab.IsExist
                        //ShortDescription = StripHTML(firstTab.TabTypeText).Length > 300 ? StripHTML(firstTab.TabTypeText).Substring(0, 300) :
                        //StripHTML(firstTab.TabTypeText)

                    };

                    var tabIds = tabs.Select(_ => _.Id).ToList();

                    //get the first image of first tab
                    var mainImage = _RService.ServiceTabFiles
                        .FirstOrDefault(_ => tabIds.Contains(_.ServiceTabId));

                    if (mainImage != null)
                    {
                        vm.ImagePath = string.Format("/Files/ServiceGallery/{0}/{1}", mainImage.ServiceTabId, mainImage.File);
                        vm.Alt = mainImage.Alt;
                    }

                    menuNodes.Add(vm);
                }
                //if it hasnt any tab it may have child
                else
                    GetChilds(child.Id);
            }
        }

    }
}
