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
using Repository.Abstract;

namespace WebUI.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
       
        IUserAccountRepository _RUser;
       
        UserAccountExtentions EDefineUser;
        public UserAdminController(IUserAccountRepository DefineUserRepository)
        {
          
            _RUser = DefineUserRepository;
          
            EDefineUser = new UserAccountExtentions(_RUser);
        }
        
        [HttpGet]
        public ActionResult ChangePass()
        {
            if (IsValidSessions())
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult ChangePass(ChangePasswordModel model)
        {
            if (IsValidSessions())
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    if (EDefineUser.IsTruePassword(Convert.ToInt32(Session["admin"].ToString()), model.OldPassword))
                    {
                        UserAccount DefineUser = _RUser.UserAccountDetails(Convert.ToInt32(Session["admin"].ToString()));
                        DefineUser.EncrypedPass =Common.CommonMethods.Encrypt(model.NewPassword);
                        _RUser.SaveUserAccount(DefineUser);
                       
                    }
                }
                return RedirectToAction("succ");
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            if (IsValidSessions())
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult UserList(int Page = 1)
        {
            if (IsValidSessions())
            {
                int Start = (Page - 1) * 5;
                int End = 5;
                TempData["Count"] = EDefineUser.GetCountUsers();
                ViewBag.UserList = EDefineUser.GetUser(Start, End).ToList();
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
     
        public ActionResult BackList(int Page)
        {
            return RedirectToAction("UserList", new { Page = Page });
        }
        [HttpPost]
        public ActionResult DeleteUser(int id, int Page = 1)
        {
            if (IsValidSessions())
            {
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                EDefineUser.DeleteUser(id);
                int count = EDefineUser.GetCountUsers();
                TempData["Count"] = count;
                if (count % 5 == 0)
                {
                    Page = Page - 1;
                }
                return RedirectToAction("refreshUser", new { Page = Page });
            }
            return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        [HttpGet]
        public ActionResult refreshUser(int Page = 1)
        {
            if (IsValidSessions())
            {
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 5;
                int End = 5;
                TempData["Count"] = EDefineUser.GetCountUsers();
                ViewBag.UserList = EDefineUser.GetUser(Start, End).Where(p => p.IsActive == true).ToList();
                return PartialView("_UserList");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }
        [HttpPost]
        public ActionResult CreateUser(RegisterModel RegisterModel)
        {
            if (IsValidSessions())
            {
                if (ModelState.IsValid)
                {
                    if (EDefineUser.IsExistUserName(RegisterModel.UserName))
                    {
                        UserAccount Define = EDefineUser.FillDefineUser(RegisterModel);
                        Define.RoleId = 1;
                        Define.IsActive = true;
                        _RUser.SaveUserAccount(Define);
                        UserAccount Def = _RUser.UserAccountDetails(Convert.ToInt32(Session["admin"]));
                        _RUser.SaveUserAccount(Define);
                    }
                    TempData["Message"] = "عملیات با موفقیت انجام شد.";
                    TempData["result"] = "OK";
                    return RedirectToAction("UserList");
                }
                else
                {
                    return View();
                }
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        public PartialViewResult succ()
        {
            ViewBag.result = TempData["result"];
            string Result = ViewBag.result;
            switch (Result)
            {
                case null: { TempData["Message"] = "عملیات با موفقیت انجام شد."; break; }
                case "Error": { TempData["Message"] = "گذر واژه جاری اشتباه است ."; break; }
                case "ErrorRole": { TempData["Message"] = "گزینه ای برای نقش انتخاب نشده است .";break; }
            }
            if (Result == null)
            {
                return PartialView("~/Areas/Admin/Views/Alerts/_Successful.cshtml");
            }
            else
            {
                return PartialView("~/Areas/Admin/Views/Alerts/_Error.cshtml");
            }
        }
        [HttpGet]
        public ActionResult EditUser(int id, int Extparam)
        {
            if (IsValidSessions())
            {
                UserAccount DefineUser = _RUser.UserAccountDetails(id);
                ViewBag.EditId = id;
                ViewBag.Pg = Extparam;
                EditUserModel n = new EditUserModel() { Email = DefineUser.Email, Family = DefineUser.LastName, Name = DefineUser.Name };
                return View(n);
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult EditUser(string EditId, EditUserModel EditUserModel, int Page)
        {
            if (IsValidSessions())
            {
                int Id = Convert.ToInt32(EditId.ToString());
                var DefineUser = _RUser.UserAccountDetails(Id);
                DefineUser.Name = EditUserModel.Name;
                DefineUser.LastName = EditUserModel.Family;
                DefineUser.Email = EditUserModel.Email;
              
                _RUser.SaveUserAccount(DefineUser);
                return RedirectToAction("UserList", new { Page = Page });
            }
            return RedirectToAction("Login", "Home");
        }
     
        [HttpGet]
        public ActionResult ChangePassByAdmin()
        {
            if (IsValidSessions())
            {
                ViewBag.UserList = EDefineUser.UserList.ToList();
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult ChangePassByAdmin(int dpUserList, ChangePasswordModel model)
        {
            if (IsValidSessions())
            {
                EDefineUser.ChangePassByAdmin(dpUserList, model.NewPassword);
                ViewBag.result = "OK";
                return RedirectToAction("succ");
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        [HttpGet]
        public ActionResult UserInfoEdit()
        {
            if (IsValidSessions())
            {
                Domain.Entities.UserAccount DefUser = _RUser.UserAccountDetails(Convert.ToInt32(Session["admin"].ToString()));
                EditUserModel User = new EditUserModel() { Name = DefUser.Name, Family = DefUser.LastName, Email = DefUser.Email, Id = DefUser.Id };
                return View(User);
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult UserInfoEdit(EditUserModel EditUserModel)
        {
            if (IsValidSessions())
            {
                Domain.Entities.UserAccount DefineUser = _RUser.UserAccountDetails(EditUserModel.Id);
                DefineUser.Email = EditUserModel.Email;
                DefineUser.Name = EditUserModel.Name;
                DefineUser.LastName = EditUserModel.Family;
               
                _RUser.SaveUserAccount(DefineUser);
                ViewBag.result = "OK";
                return RedirectToAction("succ");
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            if (IsValidSessions())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
     
        public ActionResult BackListRole(int Page)
        {
            return RedirectToAction("RoleList", new { Page = Page });
        }
    
        [HttpPost]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
        public ActionResult CheckUserName(string UserName)
        {
            var Query = EDefineUser.IsExistUserName(UserName);
            if (!Query) return Json(false);
            return Json(true);
        }
        [HttpPost]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
        public ActionResult CheckPassword(string OldPassword)
        {
            var Query = EDefineUser.IsExistPassword(Convert.ToInt32(Session["admin"]), OldPassword);
            if (!Query) return Json(false);
            return Json(true);
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
        //بررسی می کند که آیا مدیر اصلی سایت به این صفحه دسترسی پیدا کرده یا خیر
        //private bool IsMasterValidSession()
        //{
        //    return (Session["admin"] != null && EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()))) ? true : false;
        //}
    }
}
