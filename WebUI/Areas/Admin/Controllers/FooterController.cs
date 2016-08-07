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
using WebUI.Infrastructure.Extentions.Admin;
using Domain.ViewModel.Admin;
using Domain.Validation.Admin;
using System.Web.Routing;
using Repository.Abstract;

namespace WebUI.Areas.Admin.Controllers
{
    public class FooterController : Controller
    {
        IFooterRepository _RFooter;
        FooterExtentions EFooter;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
        int _LanguageId = 1;
        public FooterController(IFooterRepository FooterRepository, IUserAccountRepository DefineUserRepository)
        {
            _RFooter = FooterRepository;
            _RDefineUser = DefineUserRepository;
            EFooter = new FooterExtentions(_RFooter);
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (requestContext.HttpContext.Session["Language"] != null)
            {
                _LanguageId = Convert.ToInt32(Session["Language"].ToString());
            }
        }
        [HttpGet]
        public ActionResult FooterColumnName()
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                ViewBag.FooterColumnName =_RFooter.FooterColumnNames.Where(x=>x.LanguageId==LanguageId).ToList();
                return View();
            }
            else
                return RedirectToAction("Login", "Home");

        }
        [HttpPost]
        public ActionResult EditFooterColumnName(int Id = 0, string NameM = "")
        {
            if (IsValidSessions())
            {
                FooterColumnName FooterColumnName =_RFooter.DetailsFooterColumnName(Id);
                FooterColumnName.Name = NameM;
                _RFooter.SaveFooterColumnName(FooterColumnName);
                return PartialView("_Menus");
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        public ActionResult FooterList(string Page)
        {
            if (IsValidSessions())
            {
                TempData["result"] = null;
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                try
                {
                    int PageNumber = 1;
                    int GroupID = 0;
                    if (Page != null)
                    {
                        string[] PageNumber_GroupID = Page.Split('/');
                        if (PageNumber_GroupID.Count() == 2)
                        {
                            PageNumber = Convert.ToInt32(PageNumber_GroupID[0]);
                            GroupID = Convert.ToInt32(PageNumber_GroupID[1]);
                        }
                    }
                    int Start = (PageNumber - 1) * 5;
                    int End = 5;
                    var model =_RFooter.FooterColumnNames.Where(x=>x.LanguageId==LanguageId).ToList();
                    if (GroupID == 0)
                    {
                        GroupID = model.FirstOrDefault().Id;
                    }
                    TempData["Columns"] = GroupID;
                    TempData["Count"] =EFooter.GetCountFooter(GroupID);
                    ViewBag.Footer =EFooter.GetFooter(GroupID, Start, End).ToList();
                    ViewBag.FooterColumnNameList = new SelectList(model, "Id", "Name", GroupID);
                    ViewBag.PageNumber = PageNumber;
                    ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                    return View(model);
                }
                catch
                {
                    ViewBag.ErrorMsg = "ستونی برای فوتر ثبت نشده است";
                    ViewBag.result = "Error";
                    return View();
                }
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult ShowFooterThisColumn(int Columns, int Page = 1)
        {
            if (IsValidSessions())
            {
                ViewBag.PageNumber = 0;
                TempData["Count"] =EFooter.GetCountFooter(Columns);
                ViewBag.Footer = EFooter.GetFooter(Columns, 0, 5).ToList();
                TempData["Columns"] = Columns;
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return PartialView(viewName: "_FooterList");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }
        [HttpPost]
        public ActionResult DeleteFooter(int Id, int Page = 1)
        {
            if (IsValidSessions())
            {
                _RFooter.DeleteFooter(_RFooter.DetailsFooter(Id));
                int cnn = Convert.ToInt32(TempData["Columns"]);
                TempData["Columns"] = cnn;
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت ثبت شد.";
                int count =EFooter.GetCountFooter(cnn);
                TempData["Count"] = count;
                if (count % 5 == 0)
                {
                    Page = Page - 1;
                }
                return RedirectToAction("RefreshFooter", new { Page = Page });
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }

        [HttpGet]
        public ActionResult RefreshFooter(int Page = 1)
        {
            if (IsValidSessions())
            {
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 5;
                int End = 5;
                int GroupId = Convert.ToInt32(TempData["Columns"]);
                TempData["Columns"] = GroupId;
                ViewBag.Footer =EFooter.GetFooter(GroupId, Start, End).ToList();
                TempData["Count"] = EFooter.GetCountFooter(GroupId);
                ViewBag.PageNumber = Page;
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return PartialView("_FooterList");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }

        [HttpGet]
        public ActionResult EditFooter(int Id, string Extparam)
        {
            if (IsValidSessions())
            {
                    Footer Footer =_RFooter.DetailsFooter(Id);
                    EditFooter EditFooter = new EditFooter() { Id = Footer.Id, FooterColumnNameId = Footer.FooterColumnNameId, FooterLink = Footer.FooterLink, FooterText = Footer.FooterText.Replace("</br>", "\r\n") };
                    ViewBag.Page = Extparam;
                    return View(EditFooter);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditFooter(EditFooter EditFooter, bool chkLink, string FooterText2, string Page)
        {
            if (IsValidSessions())
            {
                Footer Footer =_RFooter.DetailsFooter(EditFooter.Id);
                Footer.ModifiedDate = DateTime.Now.Date;
                Footer.FooterText = EditFooter.FooterText;
                if (FooterText2 != null && FooterText2.Length > 0)
                    Footer.FooterText = FooterText2.Replace("\r\n", "</br>") ;
                Footer.FooterLink = (chkLink == true) ? EditFooter.FooterLink : null;
                _RFooter.SaveFooter(Footer);
                return RedirectToAction("FooterList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult BackFooterList(string Page)
        {
            return RedirectToAction("FooterList", new { Page = Page });
        }
        [HttpGet]
        public ActionResult DetailFooter(int Id, string Extparam)
        {
            if (IsValidSessions())
            {
                Footer Footer = _RFooter.DetailsFooter(Id);
                ViewBag.Page = Extparam;
                return View(Footer);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult AddFooter()
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                ViewBag.FooterColNameList =_RFooter.FooterColumnNames.Where(x=>x.LanguageId==LanguageId).ToList();
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFooter(Footer Footer, bool chkLink, string FooterText2)
        {
            if (IsValidSessions())
            {
                if (FooterText2 != null && FooterText2.Length > 0)
                    Footer.FooterText = FooterText2.Replace("\r\n", "</br>");
                Footer.FooterLink = (chkLink == true) ? Footer.FooterLink : null;
                Footer.CreationDate = DateTime.Now.Date;
                _RFooter.SaveFooter(Footer);
                return RedirectToAction("succ");
            }
            else
                return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        public PartialViewResult succ()
        {
            TempData["result"] = "OK";
            TempData["Message"] = "عملیات با موفقیت ثبت شد .";
            return PartialView("~/Areas/Admin/Views/Alerts/_Successful.cshtml");

        }
        public PartialViewResult succWritten()
        {
            string UserName = _RDefineUser.UserAccountDetails(Convert.ToInt32(Session["admin"].ToString())).Email;
            ViewBag.UserNameList = UserName;
            return PartialView("_SuccWrittenBy");
        }
        public PartialViewResult succWrittenEmail()
        {
            string UserName = _RDefineUser.UserAccountDetails(Convert.ToInt32(Session["admin"].ToString())).Email;
            ViewBag.UserNameEmail = UserName;
            TempData["result"] = "OK";
            TempData["Message"] = "عملیات با موفقیت ثبت شد .";
            return PartialView("_SuccWrittenEmailBy");
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
        private bool IsMasterValidSession()
        {
            return (Session["admin"] != null && EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()))) ? true : false;
        }
    
    }
}