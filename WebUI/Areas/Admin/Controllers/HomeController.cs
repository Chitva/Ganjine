using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using RepositoryLayer.Abstract;
using System.IO;
using System.Web.Helpers;
using WebUI.Infrastructure.Utility;
using System.Net.Mail;
using System.Net;
using Domain.Entities;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using WebUI.Infrastructure.Extentions.Admin;
using System.Text.RegularExpressions;
using Repository.Abstract;

namespace WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions DefineUserExtentions;
        ISettingRepository _RSetting;
        ISeoMngRepository _RSeoMng;
        
        public HomeController( IUserAccountRepository DefineUserRepository,
            ISettingRepository SettingRepository,ISeoMngRepository SeoRepository)
        {
            _RDefineUser = DefineUserRepository;
            _RSetting = SettingRepository;
            _RSeoMng = SeoRepository;
           
            DefineUserExtentions = new UserAccountExtentions(_RDefineUser);
        }
      
        public ActionResult Other()
        {
            if (IsValidSession())
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        [NonAction]
        public bool IsValidSession()
        {
            return (Session["admin"] != null) ? true : false;
        }
        [HttpGet]
        public ActionResult Login()
        {
            HttpCookie cookie = Request.Cookies["1dfaad2439d579667f007b1a3ed6800110"];
            if (cookie != null)
            {
                GetHashCode m = new GetHashCode();
                int ind = cookie.Values[1].IndexOf("21232f297a57a5a743894a0e4a801fc3");
                string pass = cookie.Values[1].Substring(0, ind);
                if (DefineUserExtentions.GetHashCodeValidation(cookie.Values[0].ToString(), pass))
                {
                    Session["admin"] = DefineUserExtentions.GetUserCodeByHashCode(cookie.Values[0].ToString());
                    return RedirectToAction("Index");
                }
            }
            if (Session["admin"] != null)
                return RedirectToAction("Index");
            return View();
        }
        public ActionResult Signout()
        {
            Session["admin"] = null;
            Session["Language"] = null;
            HttpCookie cookie = Request.Cookies["1dfaad2439d579667f007b1a3ed6800110"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-10);
                Response.Cookies.Add(cookie);
            }
            Session.Clear();
            TempData.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Login(LoginModel login, int dplanguage)
        {
            var DefineUser = DefineUserExtentions.ValidationUser(login.UserName, login.Password);
            if (DefineUser != null)
            {
                HttpContext.Session["admin"] = DefineUser.Id;
                HttpContext.Session["Language"] = dplanguage;
                ViewBag.res = "OK";
                if (login.RememberMe == true)
                {

                    Response.Cookies["1dfaad2439d579667f007b1a3ed6800110"]["7b8b965ad4bca0e41ab51de7b31363a1"] = Common.CommonMethods.Encrypt(login.Password);
                    Response.Cookies["1dfaad2439d579667f007b1a3ed6800110"]["03c7c0ace395d80182db07ae2c30f034"] = login.Password + "21232f297a57a5a743894a0e4a801fc3";
                    Response.Cookies["1dfaad2439d579667f007b1a3ed6800110"].Expires = DateTime.Now.AddDays(1);
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.res = "Error";
            }
            return View();
        }
        public ActionResult Index()
        {
            if (IsValidSession())
            {
                Session["IsMasterAdmin"] = "OK";
                UserAccount df = _RDefineUser.UserAccountDetails(Convert.ToInt32(Session["admin"].ToString()));
                var length = df.Email.Length;
                TempData["username"] = df.Email.Remove(length - 10);
                PersianToolS.PersinToolsClass dateFa = new PersianToolS.PersinToolsClass();
                string dtfa = dateFa.DateToPersian(Convert.ToDateTime(df.CreateDate)).month.ToString() + "/" +
                    dateFa.DateToPersian(Convert.ToDateTime(df.CreateDate)).day.ToString() + "/ " +
                    dateFa.DateToPersian(Convert.ToDateTime(df.CreateDate)).year.ToString();

                ViewBag.UserInfo = df.Name + " " + df.LastName + ":*" + df.Email + ":*" + dtfa;

                return View();
            }
            else
                return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        //written By Azizi 94_08_27
        public ActionResult SeoMng()
        {
            var result = new viewModelServiceSeo();
           
            if (IsValidSessions())
            {
                if (Convert.ToInt32(Session["Language"]) == 1)
                {
                    var setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 1);
                    var homeSeo = _RSeoMng.ReadSeoMang("/home");
                    if(homeSeo != null)
                    {
                          if(!string.IsNullOrEmpty(homeSeo.title))
                          {
                              result.title = homeSeo.title;
                          }
                          else
                          {
                              result.title = " ";
                          }

                        if(!string.IsNullOrEmpty(homeSeo.metaDescription))
                        {
                            result.metaDescription = homeSeo.metaDescription;
                        }
                        else
                        {
                            result.metaDescription = StripHTML(setting.CompanyIntroduce ?? " ");
                        }

                        if(!string.IsNullOrEmpty(homeSeo.keywords))
                        {
                            result.keyWords = homeSeo.keywords;
                        }
                        else
                        {
                            result.keyWords = " ";
                        }
                    }
                    else
                    {
                        result.title = " ";
                        result.metaDescription = StripHTML(setting.CompanyIntroduce ?? " ");
                        result.keyWords = " ";
                    }
                }
                else //english
                {
                    //var faqSeo = _RSeoMng.ReadSeoMang("/en/home");
                    var Setting = _RSetting.Settings.FirstOrDefault(x => x.LanguageId == 2);
                   
                        result.title = "  | HomePage";
                        result.metaDescription = StripHTML(Setting.CompanyIntroduce ?? " ");
                        result.keyWords = " ";
                  
                }

                return View(result);   
            }
           
                return RedirectToAction("Login", "Home");
           
        }

        public static string StripHTML(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }

        [HttpPost]
        //written By Azizi 94_08_27
        public ActionResult SeoMng(string title, string metaDescription, string keywords)
        {
            if (IsValidSessions())
            {
                SeoMng seoEntity = new SeoMng();
                if (Convert.ToInt32(Session["Language"]) == 1)
                {
                    var persian = _RSeoMng.ReadSeoMang("/home");

                    if (persian != null)
                    {
                        persian.title = title;
                        persian.metaDescription = metaDescription;
                        persian.keywords = keywords;
                        _RSeoMng.SaveSeoMng(persian);
                    }
                    else
                    {
                        seoEntity.title = title;
                        seoEntity.metaDescription = metaDescription;
                        seoEntity.keywords = keywords;
                        seoEntity.urlTillActions = "/home";
                        _RSeoMng.SaveSeoMng(seoEntity);
                    }
                }
                else
                {
                    var english = _RSeoMng.ReadSeoMang("/en/home");

                    if (english != null)
                    {
                        english.title = title;
                        english.metaDescription = metaDescription;
                        english.keywords = keywords;
                        _RSeoMng.SaveSeoMng(english);

                    }
                    else
                    {

                        seoEntity.title = title;
                        seoEntity.metaDescription = metaDescription;
                        seoEntity.keywords = keywords;
                        seoEntity.urlTillActions = "/en/home";
                        _RSeoMng.SaveSeoMng(seoEntity);
                    }

                }
                return Json(true);

            }
            return RedirectToAction("Login", "Home");
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
