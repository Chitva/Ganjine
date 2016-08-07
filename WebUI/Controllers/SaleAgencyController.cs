//using DataLayer.Context;
//using Domain.Entities;
//using Domain.ViewModel.Admin;
//using Repository.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace WebUI.Controllers
//{
//    public class SaleAgencyController : Controller
//    {
//        IUnitOfWork _uow;
//        ISaleAgenciesRepository _rSaleAgencyRepository;
//        public SaleAgencyController(IUnitOfWork uow ,ISaleAgenciesRepository saleRepository)
//        {
//            _uow = uow;
//            _rSaleAgencyRepository = saleRepository;
//        }
//        public ActionResult Index(int Page = 1)
//        {
//            InitializeData();
//            List<SalesAgencyViewModel> list = new List<SalesAgencyViewModel>();
//            int Start = (Page - 1) * 10;
//            ViewBag.PageNumber = Page;

//            var entities = _rSaleAgencyRepository.SalesAgencies.Where(_ => _.LanguageId == 1);
//            foreach (var entity in entities)
//            {
//                var obj = new SalesAgencyViewModel()
//                {
//                    Address = entity.Address ,
//                    CityId = entity.Id ,
//                    CityName = entity.City.CityName ,
//                    Id = entity.Id ,
//                    LanguageId = entity.LanguageId ,
//                    ManagerName = entity.ManagerName ,
//                    ProvinceName = entity.City.Province.ProvinceName ,
                    
//                };
//                list.Add(obj);
//            }
//            return View(list);

//        }

//        public void InitializeData()
//        {
//            ViewBag.FooterColName = _uow.Set<FooterColumnName>().Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
//            var x1 = _uow.Set<ServiceGroup>().Where(x => x.LanguageId == 1);
//            if (x1.Any())
//                ViewBag.ServiceGroup = x1.ToList();

//            var x2 = _uow.Set<WorkExperienceGroup>().Where(x => x.LanguageId == 1);
//            if (x2.Any())
//                ViewBag.WorkExGroup = x2.ToList();
         
//            var x7 = _uow.Set<ServiceTab>();
//            if (x7.Any())
//                ViewBag.ServiceTabs = x7.ToList();
//        }


//    }
//}
