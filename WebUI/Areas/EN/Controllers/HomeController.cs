//using DataLayer.Context;
//using Domain.Entities;
//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using Domain.ViewModel.User;
//using Repository.Abstract;
//using WebUI.Models;
//
//using WebUI.Common;
//using System.Transactions;
//using WebUI.Areas.Admin.Controllers;
//using System.Text;
//using System.Security.Cryptography;
//using System.IO;
//using Domain.Validation.User;

//namespace WebUI.Areas.EN.Controllers
//{
//    public class HomeController : Controller
//    {
//        //
//        // GET: /Home/

//        IUnitOfWork _uow;
//        IServiceRepository _RService;
//        ISeoMngRepository _RSeoMng;
//        INewsRepository _rNewsRepository;
       
       
//        IUserAccountRepository _rAccountRepository;
//        List<KeyValuePair<string, string>> productImages = new List<KeyValuePair<string, string>>();
//        static readonly string PasswordHash = "P@@Sw0rd";
//        static readonly string SaltKey = "S@LT&KEY";
//        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
//        public HomeController(IUnitOfWork uow, INewsRepository newsRepository, IServiceRepository ServiceRepository,
//           ISeoMngRepository SeoRepository,
//           IUserAccountRepository userAccountRepository)
//        {
//            _uow = uow;
//            _RService = ServiceRepository;
//            _rNewsRepository = newsRepository;
//            _RSeoMng = SeoRepository;
//            _rAccountRepository = userAccountRepository;
//        }

//        public ActionResult Index()
//        {

//            InitializeData();
//            List<ProductViewModel> productImages = new List<ProductViewModel>();

//            var list = new List<int>();

//            var Setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 2);

//            if (Setting != null)
//            {

//                ViewBag.CompanyIntroductionTitle = Setting.CompanyIntroductionTitle ?? "Company Introduction";

//            }
//            ViewBag.Banners = _uow.Set<Banner>().Where(x => x.LanguageId == 2).OrderByDescending(x => x.Id).ToList();

//            var q = _RService.ServiceTabs.Where(s => s.Name == "Product Description" && s.ServiceGroup.IsHome);
//            foreach (var tab in q)
//            {
//                if (!_RService.ServiceTabFiles.Any(s => s.ServiceTabId == tab.Id)) continue;
//                var imageRow = _RService.ServiceTabFiles.FirstOrDefault(s => s.ServiceTabId == tab.Id);


//                var obj = new ProductViewModel();
//                obj.Title = tab.Title;
//                obj.ImagePath = "/Files/ServiceGallery/" + tab.Id + "/" + "" + imageRow.File;
//                obj.GroupId = tab.ServiceGroupId;
//                obj.Price = tab.Price.Value;
//                obj.Discount = tab.Discount.Value;
//                obj.IsExist = tab.IsExist;

//                productImages.Add(obj);
//            }

//            var topMostProduct = _RService.GetTopmostLeavefromTree(true, new ServiceGroup(), 1);

//            if (topMostProduct != null && topMostProduct.Name != null)
//            {
//                ViewBag.AllProductLink = string.Format("/Product/{0}/{1}", topMostProduct.Id,
//                topMostProduct.Name.Replace(" ", "-"));
//            }

//            ViewBag.ProductImages = productImages;

//            if (Setting.CompanyIntroduce != null)
//            {
//                ViewBag.Introduce = Setting.CompanyIntroduce;
//            }

//            List<viewModelNews> result = new List<viewModelNews>();
//            var latestNews = _rNewsRepository.News.Where(_ => _.LanguageId == 2).OrderByDescending(_ => _.CreationDate).Take(10);
//            foreach (var news in latestNews)
//            {

//                var res = new viewModelNews()
//                {
//                    id = news.Id,
//                    title = news.Title,
//                    shortDesc = news.ShortDes
//                };
//                result.Add(res);
//            }

//            #region CompanyIntroductionImagePath
//            //var path = Server.MapPath("~/Images/HomePageCompanyIntroduction");
//            //var images = System.IO.Directory.GetFiles(path);
//            //if (images.Length > 0)
//            //{
//            //    var fileName = System.IO.Path.GetFileName(images[0]);
//            //    ViewBag.CompanyIntroductionImage = "/Images/HomePageCompanyIntroduction/" + fileName;
//            //}
//            //else
//            //    ViewBag.CompanyIntroductionImage = "";

//            #endregion

//            #region Single Viewpoint

//            //var viewPointVM = new ViewpointViewModel();
//            //var viewpoint = _rViewpointRepository.Viewpoints.FirstOrDefault(_ => _.IsShown && _.LanguageID == 2);
//            //if (viewpoint != null)
//            //{
//            //    viewPointVM.Id = viewpoint.Id;
//            //    viewPointVM.LanguageID = 2;
//            //    viewPointVM.Options = _rViewpointRepository.GetOptions(viewpoint.Id);
//            //    viewPointVM.Text = viewpoint.Text;
//            //}

//            //ViewBag.viewpoint = viewPointVM;

//            #endregion

//            #region BestSpeech

//            //var entities = _uow.Set<BestSpeech>().Where(_ => _.IsEnabled).ToList();
//            //var speech = new List<BestSpeechVM>();

//            ////default value of  BestSpeech in HomePage
//            //int listCount = 10;

//            //if (entities.Count < 10)
//            //{
//            //    listCount = entities.Count;
//            //}

//            //if (listCount < 10)
//            //{
//            //    foreach (var entity in entities)
//            //    {
//            //        var obj = new BestSpeechVM()
//            //        {
//            //            Id = entity.Id,
//            //            IsEnabled = entity.IsEnabled,
//            //            SpeechText = entity.Text
//            //        };
//            //        speech.Add(obj);
//            //    }
//            //}
//            //else
//            //{
//            //    int counter = 0;
//            //    var helperList = entities;
//            //    var random = new Random();

//            //    while (counter < listCount)
//            //    {
//            //        var i = random.Next(0, helperList.Count());
//            //        var obj = new BestSpeechVM()
//            //        {
//            //            Id = helperList[i].Id,
//            //            IsEnabled = helperList[i].IsEnabled,
//            //            SpeechText = helperList[i].Text
//            //        };

//            //        speech.Add(obj);
//            //        helperList.Remove(helperList[i]);
//            //        counter++;
//            //    }
//            //}

//            //ViewBag.Speech = speech;

//            #endregion

//            #region LinkBox
//            //var links = _uow.Set<LinkBox>().Where(_ => _.LanguageId == 2 && _.IsShown).ToList();
//            //ViewBag.Links = links;
//            #endregion

//            FillingRelatedViewBags();
//            return View(result);
//        }

//        //[HttpPost]
//        //public ActionResult SaveNewsLetterEmail(string emailAddress)
//        //{
//        //    bool res = false;
//        //    var obj = new NewsLetterEmails()
//        //    {
//        //        Email = emailAddress
//        //    };
//        //    _rNewsLetterRepositoy.SaveEmail(obj, out res);

//        //    if (res)
//        //    {
//        //        return Json(new { Email = true }, JsonRequestBehavior.AllowGet);
//        //    }
//        //    else
//        //    {
//        //        return Json(new { Email = false }, JsonRequestBehavior.AllowGet);
//        //    }
//        //}

//        [HttpPost]
//        [CaptchaValidator]
//        public ActionResult SaveMsg(string name, string lastname, string tell,
//                                    string email, string mobile, string companyName,
//                                    string businessAreas, string officeAddress,
//                                    string subject, string message, bool captchaIsValid = true)
//        {
//            if (captchaIsValid)
//            {
//                ContactUs ContactUs = new ContactUs()
//                {
//                    Email = email,
//                    CompanyName = companyName,
//                    BusinessAreas = businessAreas,
//                    Mobile = mobile,
//                    OfficeAddress = officeAddress,
//                    Message = message,
//                    Name = name + " " + lastname,
//                    StatusMSG = 1,
//                    LanguageId = 2,
//                    Tell = tell,
//                    Subject = subject,
//                    SaveMessageDate = DateTime.Now.Date

//                };

//                _uow.Set<ContactUs>().Add(ContactUs);
//                _uow.SaveChanges();

//                return Json(new { IsOk = "Ok" }, JsonRequestBehavior.AllowGet);
//            }

//            return Json(new { IsOk = "false" }, JsonRequestBehavior.AllowGet);
//        }

//        [HttpGet]
//        public ActionResult ContactUs()
//        {
//            InitializeData();

//            var model = _uow.Set<ContactInfo>().FirstOrDefault(x => x.LanguageId == 2);
//            ViewBag.Info = _uow.Set<ContactInfo>().FirstOrDefault(x => x.LanguageId == 2);

//            FillingRelatedViewBagsAboutUs();
//            return View();
//        }

//        [HttpGet]
//        public ActionResult Introduction(string type)
//        {
//            InitializeData();
//            ViewBag.type = type.ToLower();
//            var setting = _uow.Set<Setting>().FirstOrDefault(_ => _.LanguageId == 2);
//            if (setting != null)
//            {
//                switch (type)
//                {

//                    case "CompanyHistory":
//                        ViewBag.CompanyHistory = setting.CompanyHistory;
//                        break;
//                    case "MissionStatement":
//                        ViewBag.MissionStatement = setting.MissionStatement;
//                        break;
//                    case "RajiSpecifications":
//                        ViewBag.Specifications = setting.RajiGazSpecifications;
//                        break;
//                    case "Certificates":
//                        ViewBag.Certificates = setting.Certificates;
//                        break;
//                    case "Award":
//                        ViewBag.Awards = setting.Awards;
//                        break;
//                }

//                FillingRelatedViewBagIntroduction();
//            }

//            return View();
//        }

//        [HttpGet]
//        public ActionResult AboutUs()
//        {
//            InitializeData();
//            ViewBag.Info = _uow.Set<ContactInfo>().FirstOrDefault(x => x.LanguageId == 2);
//            FillingRelatedViewBagsAboutUs();
//            return View();
//        }

//        [HttpGet]
//        public ActionResult RajiStory()
//        {
//            InitializeData();
//            ViewBag.RajiStory = _uow.Set<Setting>().FirstOrDefault(_=>_.LanguageId == 2).RajiStory;
//            FillingRelatedViewBagsAboutUs();
//            return View();
//        }


       

//        [HttpPost]
//        [CaptchaValidator]
//        public ActionResult Register(UserAccountViewmodel model, bool captchaIsValid = true)
//        {
//            dynamic result = null;
           
//            if (captchaIsValid )
//            {
//                var emailUniqueness = _uow.Set<UserAccount>().Any(_ => _.Email == model.Email);

//                if (!emailUniqueness)
//                {
//                    var pass = Encrypt(model.Password);
//                    var user = new UserAccount()
//                    {
//                        IsMale = model.IsMale,
//                        Email = model.Email,
//                        EncrypedPass = pass,
//                        Name = model.Name,
//                        LastName = model.LastName
//                    };

//                    var res = _rAccountRepository.SaveUserAccount(user);
                   
//                    Response.Status = res ? "200" : "500";
//                    result = res ? $"userId = {user.Id}" : "خطادر ثبت اطلاعات";

//                    TempData["FullName"] = user.Name + " " + user.LastName;
//                    Session.Add("UserId", user.Id);
//                }
//                else
//                {
//                    Response.Status = "400";
//                    result = "ایمیل تکراری است";
//                }
//            }
//            else
//            {
//                Response.Status = "400";
//                result = "خطا در ثبت  تصویر امنیتی";
//            }

//            return Json(result, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult SignOut()
//        {
//            Session["UserId"] = null;
//            TempData["FullName"] = null;
//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public ActionResult Login(UserLoginViewmodel model)
//        {
//            dynamic result = null;

//            var pass = Encrypt(model.Password);
//            var isExist = _rAccountRepository.UserAccounts.FirstOrDefault(x => x.Email == model.Email && x.EncrypedPass == pass);
//            if (isExist != null)
//            {
//                Session.Add("UserId", isExist.Id);
//                TempData["FullName"] = isExist.Name + " " + isExist.LastName;
//                result = $"userId = {isExist.Id}";
//            }
//            else
//            {
//                Response.Status = "400";
//                result  = "نام کاربری یا رمز عبور اشتباه است .";
//            }

//            return Json(result, JsonRequestBehavior.AllowGet);
//        }

//        private void InitializeData()
//        {
//            ViewBag.FooterColName = _uow.Set<FooterColumnName>().Where(x => x.Name.Length > 0 && x.LanguageId == 2).OrderByDescending(x => x.Id).ToList();
//            var x1 = _RService.ServiceGroups.Where(x => x.LanguageId == 2);
//            if (x1.Any())
//                ViewBag.ServiceGroup = x1.ToList();

//            var x2 = _uow.Set<WorkExperienceGroup>().Where(x => x.LanguageId == 2);
//            if (x2.Any())
//                ViewBag.WorkExGroup = x2.ToList();

//            var x7 = _uow.Set<ServiceTab>();
//            if (x7.Any())
//                ViewBag.ServiceTabs = x7.ToList();
//        }

//        //written By Azizi 94_08_27
//        private void FillingRelatedViewBags()
//        {

//            var setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 2);
//            var homeSeo = _RSeoMng.ReadSeoMang("/home");
//            if (homeSeo != null)
//            {
//                if (!string.IsNullOrEmpty(homeSeo.title))
//                {
//                    ViewBag.Title = homeSeo.title;
//                }
//                else
//                {
//                    ViewBag.Title = " ";
//                }

//                if (!string.IsNullOrEmpty(homeSeo.metaDescription))
//                {
//                    ViewBag.MetaDescription = homeSeo.metaDescription;
//                }
//                else
//                {
//                    ViewBag.MetaDescription = StripHTML(setting.CompanyIntroduce ?? " ");
//                }

//                if (!string.IsNullOrEmpty(homeSeo.keywords))
//                {
//                    ViewBag.MetaKeywords = homeSeo.keywords;
//                }
//                else
//                {
//                    ViewBag.MetaKeywords = " ";
//                }
//            }
//            else
//            {
//                ViewBag.Title = " ";
//                ViewBag.MetaDescription = StripHTML(setting.CompanyIntroduce ?? " ");
//                ViewBag.MetaKeywords = " ";
//            }

//        }

//        //written By Azizi 94_08_27
//        private void FillingRelatedViewBagsAboutUs()
//        {
//            var setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 2);
//            var AboutSeo = _RSeoMng.ReadSeoMang("/aboutus");
//            if (AboutSeo != null)
//            {
//                if (!string.IsNullOrEmpty(AboutSeo.title))
//                {
//                    ViewBag.Title = AboutSeo.title;
//                }
//                else
//                {
//                    ViewBag.Title = "درباره   ";
//                }

//                if (!string.IsNullOrEmpty(AboutSeo.metaDescription))
//                {
//                    ViewBag.MetaDescription = AboutSeo.metaDescription;
//                }
//                else
//                {
//                    ViewBag.MetaDescription = StripHTML(setting.AboutUs ?? " ");
//                }

//                if (!string.IsNullOrEmpty(AboutSeo.keywords))
//                {
//                    ViewBag.MetaKeywords = AboutSeo.keywords;
//                }
//                else
//                {
//                    ViewBag.MetaKeywords = "خوراک ; تغذیه ; طیور ; دام ; پاک آورد";
//                }
//            }
//            else
//            {
//                ViewBag.Title = " ";
//                ViewBag.MetaDescription = StripHTML(setting.AboutUs ?? " ");
//                ViewBag.MetaKeywords = "خوراک ; تغذیه ; طیور ; دام ; پاک آورد";
//            }
//        }

//        private void FillingRelatedViewBagIntroduction()
//        {

//        }

//        //written By Azizi 94_08_27
//        public static string StripHTML(string HTMLText, bool decode = true)
//        {
//            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
//            var stripped = reg.Replace(HTMLText, "");
//            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
//        }

//        //written By Azizi 95_03_24
//        private string Encrypt(string userPassword)
//        {
//            byte[] plainTextBytes = Encoding.UTF8.GetBytes(userPassword);

//            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
//            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
//            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

//            byte[] cipherTextBytes;

//            using (var memoryStream = new MemoryStream())
//            {
//                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
//                {
//                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
//                    cryptoStream.FlushFinalBlock();
//                    cipherTextBytes = memoryStream.ToArray();
//                    cryptoStream.Close();
//                }
//                memoryStream.Close();
//            }
//            return Convert.ToBase64String(cipherTextBytes);
//        }

//        //written By Azizi 95_03_24
//        private string Decrypt(string encryptedText)
//        {
//            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
//            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
//            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

//            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
//            var memoryStream = new MemoryStream(cipherTextBytes);
//            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
//            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

//            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
//            memoryStream.Close();
//            cryptoStream.Close();
//            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
//        }

//    }
//}
