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
//using PersianToolS;
//using System.Text;
//using WebUI.Infrastructure.Extentions.Admin;
//using Repository.Abstract;

//namespace WebUI.Areas.Admin.Controllers
//{
//    public class QuestionController : Controller
//    {
//        //
//        // GET: /Book/
//        IUnitOfWork _uow;
//        IQuestionRepository _RQuestion;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        QuestionExtensions EQuestion;
//        public QuestionController(IUnitOfWork uow, IQuestionRepository QuestionRepository, IUserAccountRepository DefineUserRepository)
//        {
//            _uow = uow;
//            _RQuestion = QuestionRepository;
//            _RDefineUser = DefineUserRepository;
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//            EQuestion = new QuestionExtensions(_RQuestion ,_RDefineUser);
//        }
      
//        [HttpGet]
//        public ActionResult QuestionList(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                int Start = (Page - 1) * 10;
//                int End = 10;
//                TempData["Count"] =EQuestion.GetCountQuestionAdmin(LanguageId);
//                ViewBag.QuestionList = EQuestion.GetQuestionAdmin(Start, End, LanguageId).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return View();
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult RefreshQuestion(int Page = 1)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                if (Page == 0) Page = 1;
//                int Start = (Page - 1) * 10;
//                int End = 10;
//                ViewBag.QuestionList = EQuestion.GetQuestionAdmin(Start, End, LanguageId).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                return PartialView("_QuestionList");
//            }
//            else
//            {
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//            }
//        }
//        [HttpPost]
//        public ActionResult DeleteQuestion(int Id, int Page)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                Question Question =_RQuestion.DetailsQuestion(Id);
//                _RQuestion.DeleteQuestion(Question);
//                int count = EQuestion.GetCountQuestionAdmin(LanguageId);
//                TempData["Count"] = count;
//                if (count % 10 == 0)
//                {
//                    Page = Page - 1;
//                }
//                TempData["result"] = "OK";
//                TempData["Message"] = "عملیات با موفقیت انجام شد.";
//                return RedirectToAction("RefreshQuestion", new { Page = Page });
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult DetailQuestion(int Id, int Extparam)
//        {
//            if (IsValidSessions())
//            {
//                Question Question =_RQuestion.DetailsQuestion(Id);
//                validationQuestion validationQuestion = new validationQuestion()
//                {
//                    Email = Question.Email,
//                    Message = Question.Message,
//                    Name = Question.Name,
//                    ReadDate = Question.ReadDate,
//                    Reader = Question.Reader,
//                    StatusMSG = Question.StatusMSG,
//                    Subject = Question.Subject,
//                    Tell = Question.Tell,
//                    Id = Question.Id,
//                     SaveMessageDate=Question.SaveMessageDate
//                };
//               // _RQuestion.SaveQuestion(Question);
//                ViewBag.DpStatus1 = new SelectList(new Dictionary<string, string> { { "1", "جدید" }, { "2", "خوانده شده" }, { "3", "پاسخ داده شده" } }, "Key", "Value", Question.StatusMSG);
//                ViewBag.Page = Extparam;
//                return View(validationQuestion);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        public ActionResult BackListQuestion(int Page)
//        {
//            return RedirectToAction("QuestionList", new { Page = Page });
//        }
//        public ActionResult SaveReadInfo(int Page, int dpStatus, string Reader, string ReadDate, int QuestionId)
//        {
//            if (IsValidSessions())
//            {
//                Question Question = _RQuestion.DetailsQuestion(QuestionId);
//                Question.StatusMSG = dpStatus;
//                Question.ReadDate = ReadDate;
//                Question.Reader = Reader;
//                _RQuestion.SaveQuestion(Question);
//                return RedirectToAction("QuestionList", new { Page = Page });
//            }
//            else
//                return RedirectToAction("Login", "Home");
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
