using Domain.Entities;
using Repository.Abstract;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Areas.Admin.Controllers
{
    public class IntroductionController : Controller
    {
        ISettingRepository _rSettingRepository;
        IUserAccountRepository _rDefinedUserRepository;
        int _LanguageId = 1;
        public IntroductionController(ISettingRepository settingRepository,IUserAccountRepository definedRepository)
        {
            _rSettingRepository = settingRepository;
            _rDefinedUserRepository = definedRepository;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (requestContext.HttpContext.Session["Language"] != null)
            {
                _LanguageId = Convert.ToInt32(Session["Language"].ToString());
            }
        }

        [HttpGet]
        public ActionResult History()
        {
           if(IsValidSessions())
            {
                var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);
                
                if(setting != null)
                {
                    ViewBag.history = setting.CompanyHistory;
                   
                }
                return View();

            }
            else
                return RedirectToAction("login", "Home");
        }

        [HttpGet]
        public ActionResult MissionStatements()
        {
            if (IsValidSessions())
            {
                var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);
              
                if (setting != null)
                {
                    ViewBag.Mission = setting.MissionStatement;
                   
                }
                return View();
            }
            else
                return RedirectToAction("login", "Home");
        }

        [HttpGet]
        public ActionResult RajiGazSpecifications()
        {
            if (IsValidSessions())
            {
                var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);

                if (setting != null)
                {
                    ViewBag.Specifications = setting.RajiGazSpecifications;

                }
                return View();
            }
            else
                return RedirectToAction("login", "Home");
        }

        //[HttpGet]
        //public ActionResult Perspective()
        //{
        //    if (IsValidSessions())
        //    {
        //        var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);
               
        //        if (setting != null)
        //        {
        //            ViewBag.perspective = setting.Perspective;

        //        }
        //        return View();
        //    }
        //    else
        //        return RedirectToAction("login", "Home");
        //}

        //[HttpGet]
        //public ActionResult Provisions()
        //{
        //    if (IsValidSessions())
        //    {
        //        var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);
               
        //        if (setting != null)
        //        {
        //            ViewBag.provision = setting.Provisions;

        //        }
        //        return View();
        //    }
        //    else
        //        return RedirectToAction("login", "Home");
        //}

        [HttpGet]
        public ActionResult Certificates()
        {
            if (IsValidSessions())
            {
                var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);
               
                if (setting != null)
                {
                    ViewBag.certificate = setting.Certificates;

                }
                return View();
            }
            else
                return RedirectToAction("login", "Home");
        }

        [HttpGet]
        public ActionResult Awards()
        {
            if (IsValidSessions())
            {
                var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);
               
                if (setting != null)
                {
                    ViewBag.Award = setting.Awards;

                }
                return View();
            }
            else
                return RedirectToAction("login", "Home");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveSetting(string ckEditor, Introductiontype type)
        {
            var setting = _rSettingRepository.Settings.FirstOrDefault(_ => _.LanguageId == _LanguageId);

            if(setting == null)
            {
                var obj = new Setting()
                {
                    LanguageId = _LanguageId,
                    Certificates = " ",
                    CompanyHistory = " ",
                    MissionStatement = " ",
                    //Perspective = " ",
                    //Provisions = " ",
                    Awards = " " ,
                    AboutUs = " ",
                    CompanyIntroduce = " ",
                    ContactUs = " "
                };

                _rSettingRepository.SaveSetting(obj);
                setting = obj;
            }

            if(IsValidSessions())
            {
                switch (type)
                {
                    case Introductiontype.CompanyHistory:
                        setting.CompanyHistory = ckEditor;
                        _rSettingRepository.SaveSetting(setting);
                        TempData["result"] = "OK";
                        TempData["Message"] = "عملیات با موفقیت انجام شد.";
                        return PartialView("_SuccWrittenBy");
                    case Introductiontype.MissionStatement:
                        setting.MissionStatement = ckEditor;
                        _rSettingRepository.SaveSetting(setting);
                        TempData["result"] = "OK";
                        TempData["Message"] = "عملیات با موفقیت انجام شد.";
                        return PartialView("_SuccWrittenBy");
                   
                    case Introductiontype.Certificates:
                        setting.Certificates = ckEditor;
                        _rSettingRepository.SaveSetting(setting);
                        TempData["result"] = "OK";
                        TempData["Message"] = "عملیات با موفقیت انجام شد.";
                        return PartialView("_SuccWrittenBy");
                    case Introductiontype.Awards:
                        setting.Awards = ckEditor;
                        _rSettingRepository.SaveSetting(setting);
                        TempData["result"] = "OK";
                        TempData["Message"] = "عملیات با موفقیت انجام شد.";
                        return PartialView("_SuccWrittenBy");
                    case Introductiontype.Specifications:
                        setting.RajiGazSpecifications = ckEditor;
                        _rSettingRepository.SaveSetting(setting);
                        TempData["result"] = "OK";
                        TempData["Message"] = "عملیات با موفقیت انجام شد.";
                        return PartialView("_SuccWrittenBy");
                }

                return Json(new { result = "false" });
            }
            else
                return RedirectToAction("login", "Home");
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
    
    public enum Introductiontype
    {
        CompanyHistory = 1 , //تاریخچه
        MissionStatement = 2 , //بیانیه ماموریت
        Perspective = 3 ,//چشم انداز
        Provisions = 4 ,//اساسنامه شرکت
        Certificates = 5 ,//گواهینامه ها
        Awards = 6,//تندیس ها
        Specifications = 7 //خواص گز راجی
    }
}