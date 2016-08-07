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
    public class StoryController : Controller
    {
        IStoryRepository _RStory;
        StoryExtentions EStory;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
       
        public StoryController(IStoryRepository StoryRepository, IUserAccountRepository DefineUserRepository)
        {
            _RStory = StoryRepository;
            _RDefineUser = DefineUserRepository;
            EStory = new StoryExtentions(_RStory, _RDefineUser);
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }
        [HttpPost]
        public ActionResult OkActiveStory(int Id, int Page)
        {
            if (IsValidSessions())
            {

                Story Story = _RStory.DetailsStory(Id);
                Story.IsShow = !Story.IsShow;
                _RStory.SaveStory(Story);

                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                int count = EStory.GetCountStory(LanguageId);
                TempData["Count"] = count;
                if (count % 10 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت ثبت شد.";
                return RedirectToAction("RefreshStoryList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult CreateStory()
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
        public ActionResult SaveStory(string Title, string ShortDes, string LongDes, bool IsShow, string Tags ,string metaDescription)
        {
            if (IsValidSessions())
            {

                TempData["result"] = "OK";
                Story Story = new Story();
                Story.IsShow = !IsShow;
                Story.Title = Title;
                Story.ShortDes = ShortDes;
                Story.LongDes = LongDes;
                Story.Tags = Tags;
                Story.metaDescription = metaDescription;
                Story.LanguageId = Convert.ToInt32(Session["Language"].ToString());
                Story.CreationDate = DateTime.Now.Date;
               
                _RStory.SaveStory(Story);
                TempData["Folder"] = Story.Id;
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { Idms = Story.Id }, JsonRequestBehavior.AllowGet);

            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult StoryList(int Page = 1)
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                int Start = (Page - 1) * 5;
                int End = 5;
                
                TempData["Count"] = EStory.GetCountStory(LanguageId);
                ViewBag.SitePartList = EStory.GetStory(Start, End, LanguageId).ToList();
                ViewBag.IsAdmin = true;
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult DeleteStory(int Id, int Page)
        {
            if (IsValidSessions())
            {
                Story Story = _RStory.DetailsStory(Id);
                
                if (Directory.Exists(Server.MapPath("~/Files/StoryGallery/" + Id.ToString())))
                {
                    DeleteDirectory(Server.MapPath("~/Files/StoryGallery/" + Id.ToString()));
                }
               
                _RStory.DeleteStory(Story);
                int count = EStory.GetCountStory(Convert.ToInt32(Session["Language"].ToString()));
                TempData["Count"] = count;
                if (count % 5 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return RedirectToAction("RefreshStoryList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult EditStory(int Id, int Extparam)
        {
            if (IsValidSessions())
            {
                Story Story = _RStory.DetailsStory(Id);
                ViewBag.Cnt = Story.StoryGallery.Count();

                ViewBag.alts = "";
                if (Story.StoryGallery != null)
                {
                    ViewBag.alts = string.Join("|", Story.StoryGallery.Select(_ => _.alt).ToArray());
                }

                var meta = "";
                if (!string.IsNullOrEmpty(Story.metaDescription))
                {
                    meta = Story.metaDescription;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Story.ShortDes))
                    {
                        meta = Story.ShortDes;
                    }
                    else
                    {
                        meta = StripHTML(Story.LongDes);

                        meta = meta.Length > 1000 ? meta.Substring(0, 1000) : meta;
                    }
                }
                viewmodelStory viewmodelStory = new viewmodelStory() { Tags = Story.Tags, ShortDes = Story.ShortDes, IsShow = !Story.IsShow, Id = Story.Id, LongDes = Story.LongDes, Title = Story.Title  ,metaDescription =meta};
                ViewBag.Page = Extparam;
                TempData["Folder"] = Id;
                
                return View(viewmodelStory);
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
        public ActionResult EditStory(string Title, string ShortDes, string LongDes, bool IsShow, int Page, string Tags, string metaDescription, string alts)
        {
            if (IsValidSessions())
            {
                int StoryId = Convert.ToInt32(TempData["Folder"]);
                TempData["Folder"] = StoryId;
                TempData["Page"] = Page;
                Story Story = _RStory.DetailsStory(StoryId);
                Story.LongDes = LongDes;
                Story.Title = Title;
                Story.Tags = Tags;
                Story.IsShow = !IsShow;
                Story.ShortDes = ShortDes;
                Story.metaDescription = metaDescription;
                Story.ModifiedDate = DateTime.Now.Date;

                try
                {
                    if (Story.StoryGallery != null)
                    {
                        var i = 0;
                        var altsArray = alts.Split(new string[] { "|" }, StringSplitOptions.None);
                        foreach (var item in Story.StoryGallery)
                        {
                            item.alt = altsArray[i++];
                        }
                    }
                }
                catch{

                }

                _RStory.SaveStory(Story);
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { isok = "yes", JsonRequestBehavior.AllowGet });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult BackStoryList(int Page)
        {
            return RedirectToAction("StoryList", new { Page = Page });
        }
        [HttpGet]
        public ActionResult DetailStory(int Id, string Extparam)
        {
            if (IsValidSessions())
            {
                TempData["Folder"] = null;
                Story Story = _RStory.DetailsStory(Id);
                TempData["Folder"] = Id;
                ViewBag.Page = Extparam;
                return View(Story);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult RefreshStoryList(int Page = 1)
        {
            if (IsValidSessions())
            {
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 5;
                int End = 5;
                ViewBag.PageNumber = Page;
                ViewBag.SitePartList = EStory.GetStory(Start, End, Convert.ToInt32(Session["Language"].ToString())).ToList();
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return PartialView("_StoryList");
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
