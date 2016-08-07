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
using Domain.Entities;
using System.Globalization;
using Domain.Validation.Admin;
using WebUI.Infrastructure.Extentions.Admin;
using Domain.ViewModel.Admin;
using System.Text.RegularExpressions;
using Repository.Abstract;
using System.Xml;
using System.ServiceModel.Syndication;

namespace WebUI.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        INewsRepository _RNews;
       
        NewsExtentions ENews;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
       
        public NewsController(INewsRepository NewsRepository, IUserAccountRepository DefineUserRepository)
        {
            _RNews = NewsRepository;
            _RDefineUser = DefineUserRepository;
           
            ENews = new NewsExtentions(_RNews, _RDefineUser);
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }
        [HttpPost]
        public ActionResult OkActiveNews(int Id, int Page)
        {
            if (IsValidSessions())
            {

                News News = _RNews.DetailsNews(Id);
                News.IsShow = !News.IsShow;
                _RNews.SaveNews(News);

                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                int count = ENews.GetCountNews(LanguageId);
                TempData["Count"] = count;
                if (count % 10 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت ثبت شد.";
                return RedirectToAction("RefreshNewsList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult CreateNews()
        {

            if (IsValidSessions())
            {

                TempData["Folder"] = null;

                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveNews(string Title, string ShortDes, string LongDes, bool IsShow, string Tags ,string metaDescription)
        {
            if (IsValidSessions())
            {

                TempData["result"] = "OK";
                News News = new News();
                News.IsShow = IsShow;
                News.Title = Title;
                News.ShortDes = ShortDes;
                News.LongDes = LongDes;
                News.Tags = Tags;
                News.metaDescription = metaDescription;
                News.LanguageId = Convert.ToInt32(Session["Language"].ToString());
                News.CreationDate = DateTime.Now.Date;
                _RNews.SaveNews(News);
                TempData["Folder"] = News.Id;
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { Idms = News.Id }, JsonRequestBehavior.AllowGet);

            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult NewsList(int Page = 1)
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                int Start = (Page - 1) * 5;
                int End = 5;
                
                TempData["Count"] = ENews.GetCountNews(LanguageId);
                ViewBag.SitePartList = ENews.GetNews(Start, End, LanguageId).ToList();
                ViewBag.IsAdmin = true;
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult DeleteNews(int Id, int Page)
        {
            if (IsValidSessions())
            {
                News News = _RNews.DetailsNews(Id);
               
                if (Directory.Exists(Server.MapPath("~/Files/NewsGallery/" + Id.ToString())))
                {
                    DeleteDirectory(Server.MapPath("~/Files/NewsGallery/" + Id.ToString()));
                }
               
                _RNews.DeleteNews(News);
                int count = ENews.GetCountNews(Convert.ToInt32(Session["Language"].ToString()));
                TempData["Count"] = count;
                if (count % 5 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return RedirectToAction("RefreshNewsList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult EditNews(int Id, int Extparam)
        {
            if (IsValidSessions())
            {
                News News = _RNews.DetailsNews(Id);
                ViewBag.Cnt = News.NewsGallery.Count();

                ViewBag.alts = "";
                if (News.NewsGallery != null)
                {
                    ViewBag.alts = string.Join("|", News.NewsGallery.Select(_ => _.alt).ToArray());
                }

                var meta = "";
                if (!string.IsNullOrEmpty(News.metaDescription))
                {
                    meta = News.metaDescription;
                }
                else
                {
                    if (!string.IsNullOrEmpty(News.ShortDes))
                    {
                        meta = News.ShortDes;
                    }
                    else
                    {
                        meta = StripHTML(News.LongDes);

                        meta = meta.Length > 1000 ? meta.Substring(0, 1000) : meta;
                    }
                }
                viewmodelNews viewmodelNews = new viewmodelNews() { Tags = News.Tags, ShortDes = News.ShortDes, IsShow = !News.IsShow, Id = News.Id, LongDes = News.LongDes, Title = News.Title  ,metaDescription =meta};
                ViewBag.Page = Extparam;
                TempData["Folder"] = Id;
                
                return View(viewmodelNews);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        public static string StripHTML(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }



        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditNews(string Title, string ShortDes, string LongDes, bool IsShow, int Page, string Tags, string metaDescription, string alts)
        {
            if (IsValidSessions())
            {
                int NewsId = Convert.ToInt32(TempData["Folder"]);
                TempData["Folder"] = NewsId;
                TempData["Page"] = Page;
                News News = _RNews.DetailsNews(NewsId);
                News.LongDes = LongDes;
                News.Title = Title;
                News.Tags = Tags;
                News.IsShow = !IsShow;
                News.ShortDes = ShortDes;
                News.metaDescription = metaDescription;
                News.ModifiedDate = DateTime.Now.Date;

                try
                {
                    if (News.NewsGallery != null)
                    {
                        var i = 0;
                        var altsArray = alts.Split(new string[] { "|" }, StringSplitOptions.None);
                        foreach (var item in News.NewsGallery)
                        {
                            item.alt = altsArray[i++];
                        }
                    }
                }
                catch{

                }

                _RNews.SaveNews(News);
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { isok = "yes", JsonRequestBehavior.AllowGet });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult BackNewsList(int Page)
        {
            return RedirectToAction("NewsList", new { Page = Page });
        }
        [HttpGet]
        public ActionResult DetailNews(int Id, string Extparam)
        {
            if (IsValidSessions())
            {
                TempData["Folder"] = null;
                News News = _RNews.DetailsNews(Id);
                TempData["Folder"] = Id;
                ViewBag.Page = Extparam;
                return View(News);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult RefreshNewsList(int Page = 1)
        {
            if (IsValidSessions())
            {
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 5;
                int End = 5;
                ViewBag.PageNumber = Page;
                ViewBag.SitePartList = ENews.GetNews(Start, End, Convert.ToInt32(Session["Language"].ToString())).ToList();
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return PartialView("_NewsList");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }
        public void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, true);
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
        //ایجاد منوی سمت راست قسمت ادمین
        

    }
}
