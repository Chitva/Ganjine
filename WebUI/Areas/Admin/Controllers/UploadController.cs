//using System;
//using System.Linq;
//using System.Web.Mvc;
//using RepositoryLayer.Abstract;
//using WebUI.Infrastructure.Utility;
//using Domain.Entities;
//using WebUI.Infrastructure.Extentions.Admin;
//using FileUpload.Web.Models;
//using Repository.Abstract;

//namespace WebUI.Areas.Admin.Controllers
//{
//    public class UploadController : Controller
//    {
//        IUserAccountRepository _RDefineUser;
//        IUploadRepository _RUpload;
//        UploadExtentions EUpload;
//        UserAccountExtentions EDefineUser;
//        public UploadController(IUserAccountRepository DefineUserRepository,
//            IUploadRepository UploadRepository)
//        {
//            _RDefineUser = DefineUserRepository;
//            _RUpload = UploadRepository;
//            EUpload = new UploadExtentions(_RUpload, _RDefineUser);
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }

//        [HttpGet]
//        public ActionResult CreateUpload()
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

//        [HttpPost]
//        public ActionResult UploadFiles(SelectedFileModel selectedFile)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                var fileName = selectedFile.fileName;
//                try
//                {
//                    GetFileExtension Ext = new GetFileExtension();
//                    var fileContent = selectedFile.fileContent;
//                    var category = selectedFile.category;
//                    var title = selectedFile.title;
//                    var base64String = fileContent.Split(',')[1];
//                    fileName = DateTime.Now.Ticks + fileName;

//                    var filePath = Server.MapPath("~/Files/Upload/") + fileName;
//                    var bytes = Convert.FromBase64String(base64String);
//                    System.IO.File.WriteAllBytes(filePath, bytes);

//                    Upload Upload = new Upload();
//                    Upload.LanguageId = LanguageId;
//                    Upload.FileName = fileName;
//                    Upload.CreationDate = DateTime.Now.Date;
//                    _RUpload.SaveUpload(Upload);
//                }
//                catch (Exception)
//                {
//                    return Json("خطا در آپلود فایل");
//                }
//                return Json(fileName, JsonRequestBehavior.AllowGet);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpGet]
//        public ActionResult UploadList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                int Start = (Page - 1) * 5;
//                int End = 5;
//                TempData["Count"] =EUpload.GetCountUpload(LanguageId);
//                ViewBag.SitePartList = EUpload.GetUpload(Start, End, LanguageId).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }

//        [HttpPost]
//        public ActionResult DeleteUpload(int Id, int Page)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                Upload Upload =_RUpload.DetailsUpload(Id);
             
//                EUpload.DeleteUpload(Upload);
//                int count = EUpload.GetCountUpload(LanguageId);
//                TempData["Count"] = count;
//                if (count % 5 == 0)
//                {
//                    Page = Page - 1;
//                }
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                return RedirectToAction("RefreshUploadList", new { Page = Page });
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
      
//        public ActionResult BackUploadList(int Page)
//        {
//            return RedirectToAction("UploadList", new { Page = Page });
//        }

//        [HttpGet]
//        public ActionResult RefreshUploadList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                if (Page == 0) Page = 1;
//                int Start = (Page - 1) * 5;
//                int End = 5;
//                ViewBag.SitePartList = EUpload.GetUpload(Start, End, LanguageId).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return PartialView("_UploadList");
//            }
//            else
//            {
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
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
