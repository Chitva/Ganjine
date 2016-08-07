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
//using System.Web.Routing;
//using System.Drawing;
//using System.Drawing.Imaging;
//using Repository.Abstract;

//namespace WebUI.Areas.Admin.Controllers
//{
//    public class BannerController : Controller
//    {
//        IUserAccountRepository _RDefineUser;
//        IBannerRepository _RBanner;
//        BannerExtentions EBanner;
//        UserAccountExtentions EDefineUser;
//        int _LanguageId = 1;
//        public BannerController(IUserAccountRepository DefineUserRepository, IBannerRepository BannerRepository)
//        {
//            _RDefineUser = DefineUserRepository;
//            _RBanner = BannerRepository;
//            EBanner = new BannerExtentions(_RBanner, _RDefineUser);
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }

//        protected override void Initialize(RequestContext requestContext)
//        {
//            base.Initialize(requestContext);
//            if (requestContext.HttpContext.Session["Language"] != null)
//            {
//                _LanguageId = Convert.ToInt32(Session["Language"].ToString());
//            }
//        }

//        [HttpGet]
//        public ActionResult CreateBanner()
//        {
//            if (IsValidSessions())
//            {
//                if (TempData["result"] != null)
//                {
//                    ViewBag.result = "OK";
//                }
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult CreateBanner(validationImage validationBanner, string alt)
//        {
//            if (IsValidSessions())
//            {
//                string filename = SaveSitePartImage(validationBanner.ImageBanner);
//                if (filename != "")
//                {
//                    TempData["result"] = "OK";

//                    Banner Banner = new Banner() { LanguageId = Convert.ToInt32(Session["Language"].ToString()),
//                       CreationDate = DateTime.Now.Date, Image = filename, alt = alt };
//                    _RBanner.SaveBanner(Banner);
                  
//                    TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                    ViewData.ModelState.Clear();
//                }
//                else
//                {
//                    TempData["result"] = "Error";
//                    TempData["Message"] = "فایل ارسالی مجاز نمی باشد";
//                    return View("CreateBanner");
//                }

//                return RedirectToAction("BannerList");
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult BannerList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                int Start = (Page - 1) * 5;
//                int End = 5;
//                TempData["Count"] = EBanner.GetCountBanner(Convert.ToInt32(Session["admin"].ToString()), Convert.ToInt32(Session["Language"]));
//                ViewBag.SitePartList = EBanner.GetBanner(Start, End, Convert.ToInt32(Session["Language"])).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpPost]
//        public ActionResult DeleteBanner(int Id, int Page)
//        {
//            if (IsValidSessions())
//            {
//                Banner Banner = _RBanner.DetailsBanner(Id);
//                EBanner.DeleteBanner(Banner);
//                int count = EBanner.GetCountBanner(Convert.ToInt32(Session["admin"].ToString()), 26);
//                TempData["Count"] = count;
//                if (count % 5 == 0)
//                {
//                    Page = Page - 1;
//                }
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                return RedirectToAction("RefreshBannerList", new { Page = Page });
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult RefreshBannerList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                if (Page == 0) Page = 1;
//                int Start = (Page - 1) * 5;
//                int End = 5;
//                ViewBag.SitePartList = EBanner.GetBanner(Start, End, Convert.ToInt32(Session["Language"])).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return PartialView("_BannerList");
//            }
//            else
//            {
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//            }
//        }
//        private string SaveSitePartImage(HttpPostedFileBase picture)
//        {
//            var filename = "";

//            if (picture != null && picture.ContentLength > 0)
//            {
//                ImageFormat format = null;

//                var ext = new GetFileExtension().GetExtension(picture.FileName);

//                filename = DateTime.Now.ToString("ddMMyyhhmmss") + "." + ext;

//                switch (ext)
//                {
//                    case "png":
//                        {
//                            format = ImageFormat.Png;

//                            break;
//                        }
//                    default:
//                        {
//                            format = ImageFormat.Jpeg;

//                            break;
//                        }
//                }

//                var path = Server.MapPath("~/Images/Banner");

//                int width = 1172, height = 355;

//                int x = 0, y = 0;

//                var source = new Bitmap(picture.InputStream);

//                if ((source.Width / source.Height) > (width / height))
//                {
//                    var newW = (width * source.Height) / height;
//                    var mod = (source.Width - newW) / 2;
//                    mod = mod < 0 ? 0 : mod;
//                    x = mod;
//                }
//                else
//                {
//                    var newH = (height * source.Width) / width;
//                    var mod = (source.Height - newH) / 2;
//                    mod = mod < 0 ? 0 : mod;
//                    y = mod;
//                }

//                var cropped = source.Clone(new Rectangle(x, y, source.Width - (x * 2), source.Height - (y * 2)), source.PixelFormat);

//                Bitmap target = null;

//                if (cropped.Width > width)
//                {
//                    target = new Bitmap(width, height);
//                }
//                else
//                {

//                    target = new Bitmap(cropped.Width, cropped.Height);
//                }

//                using (var graphics = Graphics.FromImage(target)) graphics.DrawImage(cropped, 0, 0, target.Width, target.Height);

//                target.Save($"{path}\\Large\\{filename}", format);

//                target = new Bitmap(50, 50);

//                using (var graphics = Graphics.FromImage(target)) graphics.DrawImage(cropped, 0, 0, target.Width, target.Height);

//                target.Save($"{path}\\Small\\{filename}", format);
//            }

//            return filename;
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
