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
//using Domain.Validation.Admin;
//using WebUI.Infrastructure.Extentions.Admin;
//
//using System.Text.RegularExpressions;
//using Repository.Abstract;

//namespace WebUI.Areas.Admin.Controllers
//{
//    public class ArticleController : Controller
//    {
//        IArticleRepository _RArticle;
//        ArticleExtentions EArticle;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        public ArticleController(IArticleRepository ArticleRepository, IUserAccountRepository userRepository)
//        {
//            _RArticle = ArticleRepository;
//            _RDefineUser = userRepository;
//            EArticle = new ArticleExtentions(_RArticle, _RDefineUser);
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }
//        [HttpPost]
//        public ActionResult OkActiveArticle(int Id, int Page)
//        {
//            if (IsValidSessions())
//            {

//                Article Article = _RArticle.DetailsArticle(Id);
//                Article.IsShow = !Article.IsShow;
//                _RArticle.SaveArticle(Article);

//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                int count = EArticle.GetCountArticle(LanguageId);
//                TempData["Count"] = count;
//                if (count % 10 == 0)
//                {
//                    Page = Page - 1;
//                }
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت ثبت شد.";
//                return RedirectToAction("RefreshArticleList", new { Page = Page });
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult CreateArticle()
//        {

//            if (IsValidSessions())
//            {

//                TempData["Folder"] = null;

//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult SaveArticle(string Title, string ShortDes, string LongDes, bool IsShow, string Tags,string metaDescription)
//        {
//            if (IsValidSessions())
//            {

//                TempData["result"] = "OK";
//                Article Article = new Article();
//                Article.IsShow = IsShow;
//                Article.Title = Title;
//                Article.ShortDes = ShortDes;
//                Article.LongDes = LongDes;
//                Article.Tags = Tags;
//                Article.LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                Article.CreationDate = DateTime.Now;
//                Article.metaDescription = metaDescription;

//                _RArticle.SaveArticle(Article);
//                TempData["Folder"] = Article.Id;
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                return Json(new { Idms = Article.Id }, JsonRequestBehavior.AllowGet);

//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult ArticleList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                int Start = (Page - 1) * 5;
//                int End = 5;
//                TempData["Count"] = EArticle.GetCountArticle(LanguageId);
//                ViewBag.SitePartList = EArticle.GetArticle(Start, End, LanguageId).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpPost]
//        public ActionResult DeleteArticle(int Id, int Page)
//        {
//            if (IsValidSessions())
//            {
//                Article Article = _RArticle.DetailsArticle(Id);
             
//                if (Directory.Exists(Server.MapPath("~/Files/ArticleGallery/" + Id.ToString())))
//                {
//                    DeleteDirectory(Server.MapPath("~/Files/ArticleGallery/" + Id.ToString()));
//                }
//                //   EArticle.DeleteArticle(Article);
//                _RArticle.DeleteArticle(Article);
//                int count = EArticle.GetCountArticle(Convert.ToInt32(Session["Language"].ToString()));
//                TempData["Count"] = count;
//                if (count % 5 == 0)
//                {
//                    Page = Page - 1;
//                }
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                return RedirectToAction("RefreshArticleList", new { Page = Page });
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        public ActionResult EditArticle(int Id, int Extparam = 0)
//        {
//            if (IsValidSessions())
//            {
//                Article Article = _RArticle.DetailsArticle(Id);
//                if (Article.ArticleGallery == null)
//                {
//                    ViewBag.Cnt = 0;
//                }
//                else
//                {
//                    ViewBag.Cnt = Article.ArticleGallery.Count();
//                }

//                var meta = "";
//                if (!string.IsNullOrEmpty(Article.metaDescription))
//                {
//                    meta = Article.metaDescription;
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(Article.ShortDes))
//                    {
//                        meta = Article.ShortDes;
//                    }
//                    else
//                    {
//                        meta = StripHTML(Article.LongDes);

//                        meta = meta.Length > 1000 ? meta.Substring(0, 1000) : meta;
//                    }
//                }
//                viewmodelArticle viewmodelArticle = new viewmodelArticle() { Tags = Article.Tags, ShortDes = Article.ShortDes, IsShow = !Article.IsShow, Id = Article.Id, LongDes = Article.LongDes, Title = Article.Title ,metaDescription =meta};
//                ViewBag.Page = Extparam;
//                TempData["Folder"] = Id;

//                ViewBag.alts = "";
//                if (Article.ArticleGallery != null)
//                {
//                    ViewBag.alts = string.Join("|", Article.ArticleGallery.Select(_ => _.alt).ToArray());
//                }

//                return View(viewmodelArticle);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        public static string StripHTML(string HTMLText, bool decode = true)
//        {
//            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
//            var stripped = reg.Replace(HTMLText, "");
//            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
//        }

//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult EditArticle(string Title, string ShortDes, string LongDes, bool IsShow, int Page, string Tags, string metaDescription, string alts)
//        {
//            if (IsValidSessions())
//            {
//                int ArticleId = Convert.ToInt32(TempData["Folder"]);
//                TempData["Folder"] = ArticleId;
//                TempData["Page"] = Page;
//                Article Article = _RArticle.DetailsArticle(ArticleId);
//                Article.LongDes = LongDes;
//                Article.Title = Title;
//                Article.Tags = Tags;
//                Article.IsShow = !IsShow;
//                Article.ShortDes = ShortDes;
//                Article.ModifiedDate = DateTime.Now;
//                Article.metaDescription = metaDescription;
              

//                try
//                {
//                    if (Article.ArticleGallery != null)
//                    {
//                        var i = 0;
//                        var altsArray = alts.Split(new string[] { "|" }, StringSplitOptions.None);
//                        foreach (var item in Article.ArticleGallery)
//                        {
//                            item.alt = altsArray[i++];
//                        }
//                    }
//                }
//                catch
//                {

//                }

//                _RArticle.SaveArticle(Article);
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                return Json(new { isok = "yes", JsonRequestBehavior.AllowGet });
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        public ActionResult BackArticleList(int Page)
//        {
//            return RedirectToAction("ArticleList", new { Page = Page });
//        }
//        [HttpGet]
//        public ActionResult DetailArticle(int Id, string Extparam)
//        {
//            if (IsValidSessions())
//            {
//                TempData["Folder"] = null;
//                Article Article = _RArticle.DetailsArticle(Id);
//                TempData["Folder"] = Id;
//                Article.IsShow = !Article.IsShow;
//                ViewBag.Page = Extparam;
//                return View(Article);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        public ActionResult RefreshArticleList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                if (Page == 0) Page = 1;
//                int Start = (Page - 1) * 5;
//                int End = 5;
//                ViewBag.PageNumber = Page;
//                ViewBag.SitePartList = EArticle.GetArticle(Start, End, Convert.ToInt32(Session["Language"].ToString())).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return PartialView("_ArticleList");
//            }
//            else
//            {
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//            }
//        }
//        public void DeleteDirectory(string target_dir)
//        {
//            string[] files = Directory.GetFiles(target_dir);
//            string[] dirs = Directory.GetDirectories(target_dir);

//            foreach (string file in files)
//            {
//                System.IO.File.SetAttributes(file, FileAttributes.Normal);
//                System.IO.File.Delete(file);
//            }

//            foreach (string dir in dirs)
//            {
//                DeleteDirectory(dir);
//            }

//            Directory.Delete(target_dir, true);
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
//        //ایجاد منوی سمت راست قسمت ادمین



//    }
//}
