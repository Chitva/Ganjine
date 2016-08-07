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
using PersianToolS;
using System.Text;
using WebUI.Infrastructure.Extentions.Admin;
using System.Web.Routing;
using Repository.Abstract;

namespace WebUI.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        
        IUnitOfWork _uow;
        IContactRepository _RContact;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
        ContactExtensions EContact;
        int _LanguageId = 1;
        public ContactController(IUnitOfWork uow, IContactRepository ContactRepository,
            IUserAccountRepository DefineUserRepository)
        {
            _uow = uow;
            _RContact = ContactRepository;
            _RDefineUser = DefineUserRepository;
            EDefineUser = new UserAccountExtentions(_RDefineUser);
            EContact = new ContactExtensions(_RContact, _RDefineUser);
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
        public ActionResult ContactUsList(int Page = 1)
        {
            if (IsValidSessions())
            {
                //if languageId is the Id of Persian Language 
                //all messages for all languages will be shown to user
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                int Start = (Page - 1) * 10;
                int End = 10;
                if (LanguageId != 1)
                {
                    TempData["Count"] = EContact.GetCountContactUsAdmin(LanguageId);
                    ViewBag.ContactUsList = EContact.GetContactUsAdmin(Start, End, LanguageId).ToList();
                }
                else
                {
                    TempData["Count"] = EContact.GetAllMessageCount();
                    ViewBag.ContactUsList = EContact.GetContactUsAdmin(Start, End).ToList();
                }
                
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult RefreshContactUs(int Page = 1)
        {
            if (IsValidSessions())
            {
                //if languageId is the Id of Persian Language 
                //all messages for all languages will be shown to user
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 10;
                int End = 10;
                if(LanguageId != 1)
                {
                    ViewBag.ContactUsList = EContact.GetContactUsAdmin(Start, End, LanguageId).ToList();
                }
                else
                {
                    ViewBag.ContactUsList = EContact.GetContactUsAdmin(Start, End).ToList();
                }

                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return PartialView("_ContactUsList");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }
        [HttpPost]
        public ActionResult DeleteContactUs(int Id, int Page)
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                ContactUs ContactUs =_RContact.DetailsContactUs(Id);
                _RContact.DeleteContactUs(ContactUs);
                int count = 0;
                if(LanguageId != 1)
                {
                    count = EContact.GetCountContactUsAdmin(LanguageId);
                    TempData["Count"] = count;
                }
                else
                {
                    count = EContact.GetAllMessageCount();
                    TempData["Count"] = count;
                }
                
                if (count % 10 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return RedirectToAction("RefreshContactUs", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult DetailContactUs(int Id, int Extparam)
        {
            if (IsValidSessions())
            {
                ContactUs ContactUs =_RContact.DetailsContactUs(Id);
                validationContactUs validationContactUs = new validationContactUs()
                {
                    Email = ContactUs.Email,
                    Message = ContactUs.Message,
                    Name = ContactUs.Name,
                    ReadDate = ContactUs.ReadDate,
                    Reader = ContactUs.Reader,
                    CompanyName =ContactUs.CompanyName,
                    OfficeAddress =ContactUs.OfficeAddress,
                    BusinessArea =ContactUs.BusinessAreas,
                    CompanyAddress =ContactUs.CompanyAddress,
                    StatusMSG = ContactUs.StatusMSG,
                    Subject = ContactUs.Subject,
                    Tell = ContactUs.Tell,
                    Id = ContactUs.Id,
                    SaveMessageDate = ContactUs.SaveMessageDate
                };
             
                ViewBag.DpStatus1 = new SelectList(new Dictionary<string, string> { { "1", "جدید" }, { "2", "خوانده شده" }, { "3", "پاسخ داده شده" } }, "Key", "Value", ContactUs.StatusMSG);
                ViewBag.Page = Extparam;
                return View(validationContactUs);
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult BackListContactUs(int Page)
        {
            return RedirectToAction("ContactUsList", new { Page = Page });
        }
        public ActionResult SaveReadInfo(int Page, int dpStatus, string Reader, string ReadDate, int ContactId)
        {
            if (IsValidSessions())
            {
                ContactUs ContactUs = _RContact.DetailsContactUs(ContactId);
                ContactUs.StatusMSG = dpStatus;
                ContactUs.ReadDate = ReadDate;
                ContactUs.Reader = Reader;
                _RContact.SaveContactUs(ContactUs);
                return RedirectToAction("ContactUsList", new { Page = Page });
            }
            else
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
