using Domain.Entities;
using Domain.ViewModel.Admin;
using Repository.Abstract;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    public class SaleAgencyController : Controller
    {
        ISaleAgenciesRepository _rSaleRepository;
        ICityProvinceRepository _rCityProvincesRepository;
        int _LanguageId = 1;
        public SaleAgencyController(ISaleAgenciesRepository saleRepository , ICityProvinceRepository cityRepository)
        {
            _rSaleRepository = saleRepository;
            _rCityProvincesRepository = cityRepository;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (requestContext.HttpContext.Session["Language"] != null)
            {
                _LanguageId = Convert.ToInt32(Session["Language"].ToString());
            }
        }


        [HttpGet]
        public ActionResult CreateSaleAgency()
        {
            var model = new SalesAgencyViewModel();

            if (IsValidSessions())
            {
                ViewBag.ProvinceList = new SelectList(_rCityProvincesRepository.Provinces.ToList(), "Id", "ProvinceName");
                ViewBag.Province = _rCityProvincesRepository.Provinces.ToList();
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult CreateSaleAgency(SalesAgencyViewModel model) 
        {
            if (IsValidSessions())
            {
                var obj = new SaleAgency()
                {
                   Address = model.Address ,
                   CityId = model.CityId ,
                   ManagerName = model.ManagerName ,
                   Telephone = model.Telephone ,
                   LanguageId = model.LanguageId
                };
                _rSaleRepository.SaveSalesAgency(obj);

                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { Ids = obj.Id }, JsonRequestBehavior.AllowGet);

            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult GetCityByProvinceId(int ProvinceId)
        {
            List<City> objcity = new List<City>();
            //objcity = GetAllCity().Where(m => m.StateId == stateid).ToList();
            objcity = _rCityProvincesRepository.CitiesInProvince(ProvinceId).ToList();
            SelectList obgcity = new SelectList(objcity, "Id", "CityName", 0);
            return Json(obgcity);

        }

        [HttpGet]
        public ActionResult SaleAgencyList(int Page = 1)
        {
            List<SalesAgencyViewModel> list = new List<SalesAgencyViewModel>();
            if (IsValidSessions())
            {
                int start = (Page - 1) * 5;
                int end = 5;
                TempData["Count"] = _rSaleRepository.SalesAgencies.Where(_=>_.LanguageId == _LanguageId).Count();
                var entities = _rSaleRepository.GetSpecifiedSalesAgency(start, end, 1).ToList();
                foreach (var entity in entities)
                {
                    var obj = new SalesAgencyViewModel()
                    {
                        Id = entity.Id ,
                        Address = entity.Address ,
                        CityName = entity.City.CityName ,
                        LanguageId = entity.LanguageId ,
                        ManagerName = entity.ManagerName ,
                        Telephone = entity.Telephone ,
                        ProvinceName = entity.City.Province.ProvinceName ,
                        CityId = entity.CityId ,
                        ProvinceId = entity.City.ProvinceId
                    };
                    list.Add(obj);
                }
                return View(list);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult DeleteSaleAgency(int Id, int Page)
        {
            if (IsValidSessions())
            {
                SaleAgency saleAgency = _rSaleRepository.DetailsSalesAgency(Id);
                _rSaleRepository.DeleteSalesAgency(saleAgency);

                int count = _rSaleRepository.SalesAgencies.Where(_=>_.LanguageId == _LanguageId).Count();
                TempData["Count"] = count;
                if (count % 5 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return RedirectToAction("RefreshSaleAgencyList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult EditSaleAgency(int Id, int Extparam)
        {
            if (IsValidSessions())
            {
                ViewBag.Cnt = _rSaleRepository.SalesAgencies.Count();
                var detail = _rSaleRepository.DetailsSalesAgency(Id);
                
                SalesAgencyViewModel model = new SalesAgencyViewModel()
                {
                    Id = Id,
                    Address = detail.Address ,
                    CityId = detail.CityId ,
                    ProvinceId = detail.City.ProvinceId ,
                    ManagerName = detail.ManagerName ,
                    Telephone = detail.Telephone
                    
                };
                model.Provinces = _rCityProvincesRepository.Provinces.ToList();
                ViewBag.Page = Extparam;
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditSaleAgency(SalesAgencyViewModel model, int page = 1)
        {
            if (IsValidSessions())
            {
                TempData["Page"] = page;
                var entity = _rSaleRepository.DetailsSalesAgency(model.Id);
                entity.Address = model.Address;
                entity.CityId = model.CityId;
                entity.LanguageId = _LanguageId;
                entity.ManagerName = model.ManagerName;
                entity.Telephone = model.Telephone;

                _rSaleRepository.SaveSalesAgency(entity);

                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { Ids = model.Id }, JsonRequestBehavior.AllowGet);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult SaleAgencyDetails(int Id, string Extparam)
        {
            if (IsValidSessions())
            {
                SaleAgency entity = _rSaleRepository.DetailsSalesAgency(Id);
                ViewBag.Page = Extparam;
                return View(entity);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult RefreshSaleAgencyList(int Page = 1)
        {
            if (IsValidSessions())
            {
                if (Page == 0) Page = 1;
                int start = (Page - 1) * 5;
                int end = 5;
                ViewBag.PageNumber = Page;
                var models = _rSaleRepository.GetSpecifiedSalesAgency(start, end, _LanguageId).ToList();
               
                return PartialView("_SaleAgencyList", models);
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }


        private bool IsValidSessions()
        {
            if (Session["admin"] != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}