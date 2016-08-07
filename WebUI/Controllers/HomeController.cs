using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Domain.ViewModel.User;
using Repository.Abstract;
using WebUI.Models;
using Domain.ViewModel.Admin;
using WebUI.Common;
using System.Transactions;
using WebUI.Areas.Admin.Controllers;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Domain.Validation.User;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {


        IUnitOfWork _uow;
        IServiceRepository _RService;
        ISeoMngRepository _RSeoMng;
        INewsRepository _rNewsRepository;
        IUserAccountRepository _rAccountRepository;
        List<KeyValuePair<string, string>> productImages = new List<KeyValuePair<string, string>>();
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public HomeController(IUnitOfWork uow, INewsRepository newsRepository, IServiceRepository ServiceRepository,
           ISeoMngRepository SeoRepository,
           IUserAccountRepository userAccountRepository)
        {
            _uow = uow;
            _RService = ServiceRepository;
            _rNewsRepository = newsRepository;
            _RSeoMng = SeoRepository;
            _rAccountRepository = userAccountRepository;
        }

        public ActionResult Index()
        {
            DTO.Pages.Home.Index model = new DTO.Pages.Home.Index();

            #region MenuBarFooter
            model.Menubar = FillMenuBar();
            model.Footers = FillFooter();
            #endregion

            #region Banners
            var banners = _uow.Set<Banner>().Where(_ => _.LanguageId == 1);

            foreach (var banner in banners)
            {
                var obj = new DTO.Logo.Details()
                {
                    Id = banner.Id,
                    Alt = banner.alt,
                    Path = $"/Images/Banner/Large/{banner.Image}"
                };

                model.Sliders.Add(obj);

            }
            #endregion

            #region CompanyIntroduction
            var setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 1);

            if (setting != null)
            {

                model.Introduction = setting.CompanyIntroduce != null ? setting.CompanyIntroduce : "";

            }
            #endregion

            #region Customers 

            var customers = _uow.Set<Customer>().Where(_ => _.LanguageId == 1);
            foreach (var item in customers)
            {
                var obj = new DTO.Customer.Details()
                {
                    Id = item.Id,
                    Link = item.Url
                };
                obj.Image = new DTO.Logo.Details()
                {
                    Alt = item.Alt,
                    Path = $"/Images/Customers/Large/{item.ImagePath}"
                };

                model.Customers.Add(obj);
            }
            #endregion


            FillingRelatedViewBags();
            return View(model);
        }

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
        //                    LanguageId = 1,
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

        //            var model = _uow.Set<ContactInfo>().FirstOrDefault(x => x.LanguageId == 1);
        //            ViewBag.Info = _uow.Set<ContactInfo>().FirstOrDefault(x => x.LanguageId == 1);

        //            FillingRelatedViewBagsAboutUs();
        //            return View();
        //        }


        //        [HttpGet]
        //        public ActionResult RajiStory()
        //        {
        //            InitializeData();


        //            ViewBag.RajiStory = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 1).RajiStory;

        //            FillingRelatedViewBagsAboutUs();
        //            return View();
        //        }

        //        [HttpGet]
        //        public ActionResult Introduction(string type)
        //        {
        //            InitializeData();
        //            ViewBag.type = type.ToLower();
        //            var setting = _uow.Set<Setting>().FirstOrDefault(_ => _.LanguageId == 1);
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
        //                    //case "Perspective":
        //                    //    ViewBag.Perspective = setting.Perspective;
        //                    //    break;
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
        //            ViewBag.Info = _uow.Set<ContactInfo>().FirstOrDefault(x => x.LanguageId == 1);
        //            FillingRelatedViewBagsAboutUs();
        //            return View();
        //        }


        //        [HttpGet]
        //        public ActionResult Register()
        //        {
        //            InitializeData();
        //            return View();
        //        }

        //        [HttpPost]
        //        [CaptchaValidator]
        //        public ActionResult Register(UserAccountViewmodel model, bool captchaIsValid = false)
        //        {
        //            dynamic result = null;

        //            if (captchaIsValid)
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
        //                        LastName = model.LastName ,
        //                        IsActive = true ,
        //                        CreateDate = DateTime.Now ,
        //                        RoleId = 2 
        //                    };

        //                    var res = _rAccountRepository.SaveUserAccount(user);

        //                    Response.StatusCode = res ? 200 : 500;
        //                    result = res ? "ثبت نام با موفقیت انجام شد" : "خطادر ثبت اطلاعات";

        //                    TempData["FullName"] = user.Name + " " + user.LastName;
        //                    Session.Add("UserId", user.Id);
        //                }
        //                else
        //                {
        //                    Response.StatusCode = 400;
        //                    result = "ایمیل تکراری است";
        //                }
        //            }
        //            else
        //            {
        //                Response.StatusCode = 400;
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


        //        public ActionResult Login()
        //        {
        //            HttpCookie email = Request.Cookies["userName"];
        //            HttpCookie pass = Request.Cookies["tandisPass"];

        //            if(email != null && pass != null)
        //            {
        //                ViewBag.Email = email.Value;
        //                ViewBag.Pass = pass.Value;

        //            }
        //            InitializeData();

        //            return View();
        //        }

        //        [HttpPost]
        //        public ActionResult Login(UserLoginViewmodel model)
        //        {
        //            dynamic result = null;

        //            var pass = Encrypt(model.Password);
        //            var isExist = _rAccountRepository.UserAccounts
        //                .FirstOrDefault(x => x.Email == model.Email && x.EncrypedPass == pass);
        //            if (isExist != null)
        //            {
        //                Session.Add("UserId", isExist.Id);
        //                TempData["FullName"] = isExist.Name + " " + isExist.LastName;
        //                result = $"userId = {isExist.Id}";
        //                Response.StatusCode = 200;
        //                if(model.RememberMe)
        //                {
        //                    Response.Cookies["userName"].Value = isExist.Email;
        //                    Response.Cookies["userName"].Expires = DateTime.Now.AddDays(1);
        //                    Response.Cookies["tandisPass"].Value = Decrypt(isExist.EncrypedPass);
        //                    Response.Cookies["tandisPass"].Expires = DateTime.Now.AddDays(1);
        //                }
        //            }
        //            else
        //            {
        //                Response.StatusCode = 400;
        //                result  = "نام کاربری یا رمز عبور اشتباه است .";
        //            }

        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }

        //        private void InitializeData()
        //        {
        //            ViewBag.FooterColName = _uow.Set<FooterColumnName>().Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
        //            var x1 = _RService.ServiceGroups.Where(x => x.LanguageId == 1);
        //            if (x1.Any())
        //                ViewBag.ServiceGroup = x1.ToList();

        //            var x2 = _uow.Set<WorkExperienceGroup>().Where(x => x.LanguageId == 1);
        //            if (x2.Any())
        //                ViewBag.WorkExGroup = x2.ToList();

        //            var x7 = _uow.Set<ServiceTab>();
        //            if (x7.Any())
        //                ViewBag.ServiceTabs = x7.ToList();
        //        }

        //written By Azizi 94_08_27
        private void FillingRelatedViewBags()
        {

            var setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 1);
            var homeSeo = _RSeoMng.ReadSeoMang("/home");
            if (homeSeo != null)
            {
                if (!string.IsNullOrEmpty(homeSeo.title))
                {
                    ViewBag.Title = homeSeo.title;
                }
                else
                {
                    ViewBag.Title = " ";
                }

                if (!string.IsNullOrEmpty(homeSeo.metaDescription))
                {
                    ViewBag.MetaDescription = homeSeo.metaDescription;
                }
                else
                {
                    ViewBag.MetaDescription = StripHTML(setting.CompanyIntroduce ?? " ");
                }

                if (!string.IsNullOrEmpty(homeSeo.keywords))
                {
                    ViewBag.MetaKeywords = homeSeo.keywords;
                }
                else
                {
                    ViewBag.MetaKeywords = " ";
                }
            }
            else
            {
                ViewBag.Title = " ";
                ViewBag.MetaDescription = StripHTML(setting.CompanyIntroduce ?? " ");
                ViewBag.MetaKeywords = " ";
            }

        }

        private List<DTO.Template.Details> FillMenuBar()
        {
            var model = new List<DTO.Template.Details>();

            model[0].Title = "صفحه اصلی";
            model[0].Url = "/Home";


            model[1].Title = "محصولات";
            model[1].Url = "/Products";

            model[2].Title = "نمایندگی های فروش";
            model[2].Url = "/SaleAgency";


            model[3].Title = "اخبار مجموعه";
            model[3].Url = "/News";

            model[4].Title = "سوالات متداول";
            model[4].Url = "/FAQ";

            model[5].Title = "درباره ی ما";
            model[5].Url = "#";

            model[5].Childs.Add(new DTO.Template.Details()
            {
                Title = "معرفی مجموعه",
                Url = "/AboutUs"
            });

            model[5].Childs.Add(new DTO.Template.Details()
            {
               Title ="ارتباط با ما" ,
               Url = "/ContactUs"
            });

            //??? پنل کاربری را پاس ندادم

            return model; 
        }

        public List<DTO.Template.Details> FillFooter()
        {
            var model = new List<DTO.Template.Details>();

            var footers = _uow.Set<FooterColumnName>().Where(_ => _.LanguageId == 1);
            int i = 0;
            foreach (var footer in footers)
            {
                var obj = new DTO.Template.Details()
                {
                   Title = footer.Name 
                };
                var childs = _uow.Set<Footer>().Where(_ => _.FooterColumnNameId == footer.Id);
                foreach (var child in childs)
                {
                    obj.Childs.Add(new DTO.Template.Details()
                    {
                       Title = child.FooterText ,
                       Url = child.FooterLink
                    });
                }
                model.Add(obj);
            }

            return model;
       } 

        //written By Azizi 94_08_27
        private void FillingRelatedViewBagsAboutUs()
        {
            var setting = _uow.Set<Setting>().FirstOrDefault(x => x.LanguageId == 1);
            var AboutSeo = _RSeoMng.ReadSeoMang("/aboutus");
            if (AboutSeo != null)
            {
                if (!string.IsNullOrEmpty(AboutSeo.title))
                {
                    ViewBag.Title = AboutSeo.title;
                }
                else
                {
                    ViewBag.Title = "درباره   ";
                }

                if (!string.IsNullOrEmpty(AboutSeo.metaDescription))
                {
                    ViewBag.MetaDescription = AboutSeo.metaDescription;
                }
                else
                {
                    ViewBag.MetaDescription = StripHTML(setting.AboutUs ?? " ");
                }

                if (!string.IsNullOrEmpty(AboutSeo.keywords))
                {
                    ViewBag.MetaKeywords = AboutSeo.keywords;
                }
                else
                {
                    ViewBag.MetaKeywords = "خوراک ; تغذیه ; طیور ; دام ; پاک آورد";
                }
            }
            else
            {
                ViewBag.Title = " ";
                ViewBag.MetaDescription = StripHTML(setting.AboutUs ?? " ");
                ViewBag.MetaKeywords = "خوراک ; تغذیه ; طیور ; دام ; پاک آورد";
            }
        }



        //written By Azizi 94_08_27
        public static string StripHTML(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }

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
        //                return Convert.ToBase64String(cipherTextBytes);
        //            }
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

    }
}
