//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using DataLayer.Context;
//using RepositoryLayer.Abstract;
//using System.IO;
//using System.Web.Helpers;
//using WebUI.Infrastructure.Utility;
//using Domain.Entities;
//using System.Globalization;
//using WebUI.Infrastructure.Extentions.Admin;
//
//using Domain.Validation.Admin;
//using System.Text.RegularExpressions;
//using System.Web.Routing;
//using Repository.Abstract;

//namespace WebUI.Areas.Admin.Controllers
//{
//    public class SettingController : Controller
//    {
//        ISettingRepository _RSetting;
//        IUserAccountRepository _RDefineUser;
//        IContactInfoRepository _RContactInfo;
//        ISeoMngRepository _RSeoMng;
//        IUnitOfWork _uow;
//        int _LanguageId = 1;

//        public SettingController(IUnitOfWork uow, ISettingRepository RSetting, IUserAccountRepository RDefineUser, 
//            IContactInfoRepository ContactInfoRepository,ISeoMngRepository SeoRepository)
//        {
//            _RSetting = RSetting;
//            _RContactInfo = ContactInfoRepository;
//            _RDefineUser = RDefineUser;
//            _RSeoMng = SeoRepository;
//            _uow = uow;
//        }


//        protected override void Initialize(RequestContext requestContext)
//        {
//            base.Initialize(requestContext);
//            if(requestContext.HttpContext.Session["Language"] != null)
//            {
//                _LanguageId = Convert.ToInt32(Session["Language"].ToString());
//            }
//        }



//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult Edit(string ckEditor, int Type)
//        {
//            if (IsValidSessions())
//            {
//                int UserCode = Convert.ToInt32(Session["admin"].ToString());
//                var Setting = _RSetting.Settings;
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                if (Type == 2)
//                {
//                    if (IsValidSessions())
//                    {
//                        var About = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == _LanguageId);

//                        About.AboutUs = ckEditor;
                       
//                        _RSetting.SaveSetting(About);
//                        return RedirectToAction("succWritten");
//                    }
//                    else
//                        return RedirectToAction("Login", "Home");
//                }
//                if (Type == 3)
//                {
//                    if (IsValidSessions())
//                    {
//                        var Company = Setting.FirstOrDefault(x => x.LanguageId == _LanguageId);
//                        Company.CompanyIntroduce = ckEditor;
//                        Company.LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                        _RSetting.SaveSetting(Company);
//                        return RedirectToAction("succWritten");
//                    }
//                    else
//                        return RedirectToAction("Login", "Home");
//                }
               
//                if (Type == 5)
//                {
//                    if (IsValidSessions())
//                    {
//                        var Contact = Setting.FirstOrDefault(x => x.LanguageId == _LanguageId);
//                        Contact.ContactUs = ckEditor;
//                        Contact.LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                        _RSetting.SaveSetting(Contact);
//                        return RedirectToAction("succWritten");
//                    }
//                    else
//                        return RedirectToAction("Login", "Home");
//                }
//                return View("AboutUs");
//            }
//            else
//            {
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//            }
//        }

//        [HttpGet]
//        public ActionResult ContactUs()
//        {
//            if (IsValidSessions())
//            {
//                var Setting = _RSetting.Settings.FirstOrDefault();
                
//                ViewBag.ContactUs = Setting.ContactUs;
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

      
//        [HttpGet]
//        public ActionResult CompanyIntroduce()
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                var Setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == LanguageId);
               
//                ViewBag.CompanyIntroduce = Setting.CompanyIntroduce;
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        public ActionResult RajiStory()
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                var Setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == LanguageId);

//                ViewBag.RajiStory = Setting.RajiStory;
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult RajiStory(string ckEditor)
//        {
//            if (IsValidSessions())
//            {
//                var Setting = _RSetting.Settings;
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                var setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == _LanguageId);
//                setting.RajiStory = ckEditor;

//                _RSetting.SaveSetting(setting);
//                return RedirectToAction("succWritten");
//            }
//            else
//            {
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//            }
//        }

//        [HttpGet]
//        public ActionResult AboutUs()
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                var Setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == LanguageId);
                
//                ViewBag.AboutUs = Setting.AboutUs;
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        public ActionResult ContactInfo()
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"]);
//                var ContactInfo = _RContactInfo.ContactInfos.FirstOrDefault(x => x.LanguageId == LanguageId);
//                return View(ContactInfo);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult ContactInfo(ContactInfo ContactInfo, string ckEditor)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"]);
//                var EditContactInfo = _RContactInfo.ContactInfos.FirstOrDefault(x => x.LanguageId == LanguageId);
//                EditContactInfo.Address = ContactInfo.Address;
//                EditContactInfo.Map = ContactInfo.Map;
//                EditContactInfo.FormTopText = ckEditor;
//                EditContactInfo.Tell = ContactInfo.Tell;
//                EditContactInfo.Fax = ContactInfo.Fax;
//                EditContactInfo.Email = ContactInfo.Email;
//                _RContactInfo.SaveContactInfo(EditContactInfo);
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد .";
//                return RedirectToAction("SuccWrittenBy");
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }


//        public PartialViewResult SuccWrittenBy()
//        {


//            return PartialView("~/Areas/Admin/Views/Setting/_SuccWrittenBy.cshtml");
//        }
//        public PartialViewResult succ()
//        {
//            TempData["Message"] = "عملیات با موفقیت انجام شد .";
//            return PartialView("~/Areas/Admin/Views/Alerts/_Successful.cshtml");
//        }
//        public PartialViewResult succWritten()
//        {
//            return PartialView("_SuccWrittenBy");
//        }
//        public ActionResult CheckValue(int id)
//        {
//            switch (id)
//            {

               
//                case 8:
//                    return RedirectToAction("NewsList", "News");
//                case 9:
//                    return RedirectToAction("AboutUs", "Setting");
//                case 10:
//                    return RedirectToAction("ContactUs", "Setting");
//                case 15:
//                    return RedirectToAction("ChangePass", "UserAdmin");
//                case 16:
//                    return RedirectToAction("UserInfoEdit", "UserAdmin");
//                case 20:
//                    return RedirectToAction("ProductList", "Product");
//                case 24:
//                    return RedirectToAction("ArticleList", "Article");
//                case 26:
//                    return RedirectToAction("BannerList", "Banner");
//                case 28:
//                    return RedirectToAction("CreateUser", "UserAdmin");
//                case 29:
//                    return RedirectToAction("UserList", "UserAdmin");
//                case 30:
//                    return RedirectToAction("ChangePassByAdmin", "UserAdmin");
//                case 39:
//                    return RedirectToAction("QuestionList", "Question");
//                case 72:
//                    return RedirectToAction("ContactUsList", "Contact");
//                case 104:
//                    return RedirectToAction("ProductList", "Product");
//                case 105:
//                    return RedirectToAction("UploadList", "Upload");
//                case 107:
//                    return RedirectToAction("ContactInfo", "Setting");
//                case 110:
//                    return RedirectToAction("Order", "Setting");
//                case 111:
//                    return RedirectToAction("CompanyIntroduce", "Setting");
//                case 112:
//                    return RedirectToAction("Founder", "Setting");
//                case 121:
//                    return RedirectToAction("ContactUs", "Setting");
//                case 122:
//                    return RedirectToAction("ContactInfo", "Setting");
//                case 123:
//                    return RedirectToAction("ArticleList", "Article");
//                case 127:
//                    return RedirectToAction("ProductList", "Product");
//                case 128:
//                    return RedirectToAction("QuestionList", "Question");
//                case 130:
//                    return RedirectToAction("FaqList", "Faq");
//                case 131:
//                    return RedirectToAction("FooterList", "Footer");
//                case 132:
//                    return RedirectToAction("LabratoryList", "Labratory");
//                case 133:
//                    return RedirectToAction("GalleryList", "Gallery");
//                case 134:
//                    return RedirectToAction("PriceList", "Price");
//                case 135:
//                    return RedirectToAction("UnitList", "Unit");
//                case 136:
//                    return RedirectToAction("Structure", "Setting");
//                case 138:
//                    return RedirectToAction("HonourList", "Honour");
//                case 140 :
//                    return RedirectToAction("OrderList", "Order");
//                case 148:
//                    return RedirectToAction("ApplicantList", "Employment");
//                case 150:
//                    return RedirectToAction("ResearchDevelopmentList", "ResearchDevelopment");
//                case 151:
//                    return RedirectToAction("CommerceList","Commerce");
//                case 152:
//                    return RedirectToAction("OrganizationStructureList","OrganizationStructure");
//                case 153: 
//                    return RedirectToAction("ContactInfoSeoMng", "Setting");
//                case 154:
//                    return RedirectToAction("LinkMng", "Setting");
//                case 155:
//                    return RedirectToAction("TitleMng", "Setting");
//                case 156:
//                    return RedirectToAction("CompanyIntroductionImage", "Setting");
//                case 160:
//                    return RedirectToAction("StoryList", "Story");
//                case 201:
//                    return RedirectToAction("BestSpeechList", "BestSpeech");
//                case 202:
//                    return RedirectToAction("LinkBoxList", "LinkBox");
//                case 203:
//                    return RedirectToAction("ViewPointList", "Viewpoint");
//            }
//            return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        public ActionResult CompanyIntroductionImage()
//        {
//            if (IsValidSessions())
//            {
//                var path = Server.MapPath("~/Images/HomePageCompanyIntroduction");
//                if (!Directory.Exists(path))
//                {
//                    Directory.CreateDirectory(path);
//                }

//                if (TempData["result"] != null)
//                {
//                    ViewBag.result = "OK";
//                }
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
           
//        }

//        //writtenBy Azizi 95_03_12
//        [HttpPost]
//        public ActionResult CompanyIntroductionUpload(validationImage imageValidation,string alt)
//        {
//            if (IsValidSessions())
//            {
//                string filename = SaveImage(imageValidation.ImageBanner,alt);
//                if (filename != "")
//                {
//                    TempData["result"] = "OK";
//                    TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                    ViewData.ModelState.Clear();
//                }
//                else
//                {
//                    TempData["result"] = "Error";
//                    TempData["Message"] = "فایل ارسالی مجاز نمی باشد";
                  
//                }

//                return View("CompanyIntroductionImage");
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        //written By Azizi 94_08_27
//        public ActionResult SeoMng()
//        {
//            var result = new viewModelServiceSeo();

//            if (IsValidSessions())
//            {
//                if (Convert.ToInt32(Session["Language"]) == 1)
//                {
//                    var setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 1);
//                    var AboutSeo = _RSeoMng.ReadSeoMang("/aboutus");
//                    if (AboutSeo != null)
//                    {
//                        if (!string.IsNullOrEmpty(AboutSeo.title))
//                        {
//                            result.title = AboutSeo.title;
//                        }
//                        else
//                        {
//                            result.title = " ";
//                        }

//                        if (!string.IsNullOrEmpty(AboutSeo.metaDescription))
//                        {
//                            result.metaDescription = AboutSeo.metaDescription;
//                        }
//                        else
//                        {
//                            result.metaDescription = StripHTML(setting.AboutUs ?? " ");
//                        }

//                        if (!string.IsNullOrEmpty(AboutSeo.keywords))
//                        {
//                            result.keyWords = AboutSeo.keywords;
//                        }
//                        else
//                        {
//                            result.keyWords = "";
//                        }
//                    }
//                    else
//                    {
//                        result.title = "";
//                        result.metaDescription = StripHTML(setting.AboutUs ?? "");
//                        result.keyWords = "";
//                    }
//                }
//                else //english
//                {
//                    var AboutSeo = _RSeoMng.ReadSeoMang("/en/aboutus");
//                    var Setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 2);
//                    if (AboutSeo != null)
//                    {
//                        if (!string.IsNullOrEmpty(AboutSeo.title))
//                        {
//                            result.title = AboutSeo.title;
//                        }
//                        else
//                        {
//                            result.title = "  | About Us";
//                        }

//                        if (!string.IsNullOrEmpty(AboutSeo.metaDescription))
//                        {
//                            result.metaDescription = AboutSeo.metaDescription;
//                        }
//                        else
//                        {
//                            result.metaDescription = StripHTML(Setting.AboutUs ?? "  | About Us");
//                        }

//                        if (!string.IsNullOrEmpty(AboutSeo.keywords))
//                        {
//                            result.keyWords = AboutSeo.keywords;
//                        }
//                        else
//                        {
//                            result.keyWords = "";
//                        }
//                    }
//                    else
//                    {
//                        result.title = "  | HomePage";
//                        result.metaDescription = StripHTML(Setting.AboutUs ?? "  | About Us");
//                        result.keyWords = "";
//                    }
//                }

//                return View(result);
//            }
//            else
//                return RedirectToAction("Login", "Home");

//        }


//        [HttpGet]
//        public ActionResult ContactInfoSeoMng()
//        {
//            var result = new viewModelServiceSeo();

//            if (IsValidSessions())
//            {
//                if (Convert.ToInt32(Session["Language"]) == 1)
//                {
//                    var contactInfo = _RContactInfo.ContactInfos.FirstOrDefault(_ => _.LanguageId == 1);
//                    var seo = _RSeoMng.ReadSeoMang("/contactinfo");
//                    if (seo != null)
//                    {
//                        if (!string.IsNullOrEmpty(seo.title))
//                        {
//                            result.title = seo.title;
//                        }
//                        else
//                        {
//                            result.title = " ";
//                        }

//                        if (!string.IsNullOrEmpty(seo.metaDescription))
//                        {
//                            result.metaDescription = seo.metaDescription;
//                        }
//                        else
//                        {
//                            result.metaDescription = contactInfo.Address ?? " ";
//                        }

//                        if (!string.IsNullOrEmpty(seo.keywords))
//                        {
//                            result.keyWords = seo.keywords;
//                        }
//                        else
//                        {
//                            result.keyWords = "";
//                        }
//                    }
//                    else
//                    {
//                        result.title = "";
//                        result.metaDescription = contactInfo.Address ?? " ";
//                        result.keyWords = "";
//                    }
//                }
//                else //english
//                {
//                    var seo = _RSeoMng.ReadSeoMang("/en/contactinfo");
//                    var contactInfo = _RContactInfo.ContactInfos.FirstOrDefault(_ => _.LanguageId == 2);
//                    if (seo != null)
//                    {
//                        if (!string.IsNullOrEmpty(seo.title))
//                        {
//                            result.title = seo.title;
//                        }
//                        else
//                        {
//                            result.title = "  | Contact Us";
//                        }

//                        if (!string.IsNullOrEmpty(seo.metaDescription))
//                        {
//                            result.metaDescription = seo.metaDescription;
//                        }
//                        else
//                        {
//                            result.metaDescription = contactInfo.Address ?? " ";
//                        }

//                        if (!string.IsNullOrEmpty(seo.keywords))
//                        {
//                            result.keyWords = seo.keywords;
//                        }
//                        else
//                        {
//                            result.keyWords = "";
//                        }
//                    }
//                    else
//                    {
//                        result.title = "  | ContactUs";
//                        result.metaDescription = contactInfo.Address ?? " ";
//                        result.keyWords = "";
//                    }
//                }

//                return View("SeoMng",result);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }


//        [HttpPost]
//        public ActionResult ContactInfoSeoMng(string title, string metaDescription, string keywords)
//        {
//            if (IsValidSessions())
//            {
//                SeoMng seoEntity = new SeoMng();
//                if (Convert.ToInt32(Session["Language"]) == 1)
//                {
//                    var persian = _RSeoMng.ReadSeoMang("/contactinfo");

//                    if (persian != null)
//                    {
//                        persian.title = title;
//                        persian.metaDescription = metaDescription;
//                        persian.keywords = keywords;
//                        _RSeoMng.SaveSeoMng(persian);
//                    }
//                    else
//                    {
//                        seoEntity.title = title;
//                        seoEntity.metaDescription = metaDescription;
//                        seoEntity.keywords = keywords;
//                        seoEntity.urlTillActions = "/contactinfo";
//                        _RSeoMng.SaveSeoMng(seoEntity);
//                    }
//                }
//                else
//                {
//                    var english = _RSeoMng.ReadSeoMang("/en/contactinfo");

//                    if (english != null)
//                    {
//                        english.title = title;
//                        english.metaDescription = metaDescription;
//                        english.keywords = keywords;
//                        _RSeoMng.SaveSeoMng(english);

//                    }
//                    else
//                    {

//                        seoEntity.title = title;
//                        seoEntity.metaDescription = metaDescription;
//                        seoEntity.keywords = keywords;
//                        seoEntity.urlTillActions = "/en/contactinfo";
//                        _RSeoMng.SaveSeoMng(seoEntity);
//                    }

//                }
//                TempData["Message"] = "عملیات با موفقیت انجام شد";
//                TempData["Result"] = "OK";
//                return Json(true);

//            }
//            return RedirectToAction("Login", "Home");
//        }

//        public static string StripHTML(string HTMLText, bool decode = true)
//        {
//            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
//            var stripped = reg.Replace(HTMLText, "");
//            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
//        }

//        [HttpPost]
//        //written By Azizi 94_08_27
//        public ActionResult SeoMng(string title, string metaDescription, string keywords)
//        {
//            if (IsValidSessions())
//            {
//                SeoMng seoEntity = new SeoMng();
//                if (Convert.ToInt32(Session["Language"]) == 1)
//                {
//                    var persian = _RSeoMng.ReadSeoMang("/aboutus");

//                    if (persian != null)
//                    {
//                        persian.title = title;
//                        persian.metaDescription = metaDescription;
//                        persian.keywords = keywords;
//                        _RSeoMng.SaveSeoMng(persian);
//                    }
//                    else
//                    {
//                        seoEntity.title = title;
//                        seoEntity.metaDescription = metaDescription;
//                        seoEntity.keywords = keywords;
//                        seoEntity.urlTillActions = "/aboutus";
//                        _RSeoMng.SaveSeoMng(seoEntity);
//                    }
//                }
//                else
//                {
//                    var english = _RSeoMng.ReadSeoMang("/en/aboutus");

//                    if (english != null)
//                    {
//                        english.title = title;
//                        english.metaDescription = metaDescription;
//                        english.keywords = keywords;
//                        _RSeoMng.SaveSeoMng(english);

//                    }
//                    else
//                    {

//                        seoEntity.title = title;
//                        seoEntity.metaDescription = metaDescription;
//                        seoEntity.keywords = keywords;
//                        seoEntity.urlTillActions = "/en/aboutus";
//                        _RSeoMng.SaveSeoMng(seoEntity);
//                    }

//                }
//                TempData["Message"] = "عملیات با موفقیت انجام شد";
//                TempData["Result"] = "OK";
//                return Json(true);

//            }
//            return RedirectToAction("Login", "Home");
//        }

//        //[HttpGet]
//        //public ActionResult TitleMng()
//        //{
//        //    var Setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 1);

//        //    if (Setting != null)
//        //    {
//        //        var model = new TitleMngViewModel()
//        //        {
//        //            BestSpeechTitle = Setting.BestSpeechTitle,
//        //            CompanyIntroductionTitle = Setting.CompanyIntroductionTitle
//        //        };

//        //        return View(model);
//        //    }
//        //    return View(new TitleMngViewModel());
//        //}

//        [HttpPost]
//        public ActionResult TitleMng(TitleMngViewModel model)
//        {
//            var setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 1);

//            if (setting == null)
//            {
//                _RSetting.SaveSetting(new Setting() { LanguageId = 1 });
//                setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 1);
//            }

//            //setting.BestSpeechTitle = model.BestSpeechTitle;
//            setting.CompanyIntroductionTitle = model.CompanyIntroductionTitle;

//            _RSetting.SaveSetting(setting);


//            TempData["result"] = "OK";
//            TempData["Message"] = "عملیات با موفقیت انجام شد";
//            return View();
//        }

//        //[HttpGet]
//        //public ActionResult LinkMng()
//        //{
//        //    var Setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 1);

//        //    if (Setting != null)
//        //    {
//        //        var model = new LinkMngVM()
//        //        {
//        //            Menu1Link = Setting.Menu1Link,
//        //            Menu2Link = Setting.Menu2Link,
//        //            Menu3Link = Setting.Menu3Link,
//        //            Menu4Link = Setting.Menu4Link,
//        //            Menu5Link = Setting.Menu5Link
//        //        };

//        //        return View(model);
//        //    }
//        //    return View(new LinkMngVM());
//        //}


//        [HttpPost]
//        public ActionResult LinkMng(LinkMngVM model)
//        {
//           var setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 1);

//            if(setting == null)
//            {
//                _RSetting.SaveSetting(new Setting() { LanguageId = 1 });
//                setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 1);
//            }

//                //setting.Menu1Link = model.Menu1Link;
//                //setting.Menu2Link = model.Menu2Link;
//                //setting.Menu3Link = model.Menu3Link;
//                //setting.Menu4Link = model.Menu4Link;
//                //setting.Menu5Link = model.Menu5Link;

//               _RSetting.SaveSetting(setting);


//            TempData["result"] = "OK";
//            TempData["Message"] = "عملیات با موفقیت انجام شد";
//            return View();
//        }



//        //writtenBy Azizi 95_03_12
//        private string SaveImage(HttpPostedFileBase Picture,string alt)
//        {
            
//            try
//            {
//                if (Picture != null && Picture.ContentLength > 0)
//                {
//                    string fileExtension = Path.GetExtension(Picture.FileName);
//                    string fileName = (alt==string.Empty) ? "MainImage" : alt + fileExtension;
//                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".gif")
//                    {

//                        var path = Server.MapPath("~/Images/HomePageCompanyIntroduction");
//                        var files = Directory.GetFiles(path);

//                        if (files.Length > 0)
//                        {
//                            for (int i = 0; i < files.Length; i++)
//                            {
//                                System.IO.File.Delete(files[i]);
//                            }
//                        }

//                        Picture.SaveAs(path + "/" + fileName);

//                    }

//                    return "/Images/HomePageCompanyIntroduction/" + fileName;
//                }
//                else
//                    return "";
//            }
//            catch (Exception)
//            {
//                return "";
//            }
//        }

//        private bool IsValidSessions()
//        {
//            if (Session["admin"] != null)
//            {
//                return true;
//            }
//            else
//                return false;
//        }


//    }
//}
