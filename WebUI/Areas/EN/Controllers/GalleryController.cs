//using DataLayer.Context;
//using Domain.Entities;
//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using Repository.Abstract;

//namespace WebUI.Areas.EN.Controllers
//{
//    public class GalleryController : Controller
//    {
//        //
//        // GET: /WorkExperiences/
       
//        IWorkExperienceRepository _RWorkExperience;
//        IFooterRepository _RFooter;
//        IServiceRepository _RService;
       
//        public GalleryController( 
//            IFooterRepository FooterRepository,  IServiceRepository ServiceRepository, 
//            IWorkExperienceRepository WorkExperienceRepository            )
//        {
            
//            _RWorkExperience = WorkExperienceRepository;
//            _RFooter = FooterRepository;
//            _RService = ServiceRepository;
           
//        }
//        public ActionResult Index(int id,string Title)
//        {
//            InitializeData();
//            ViewBag.catids = id;
//            var WorkExperienceGroup =_RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == 2).ToList();
//            ViewBag.WorkExperienceGroup = WorkExperienceGroup;
//            List<string> Nav = new List<string>();
//            int Groupint = id;
//            while (Groupint != 0)
//            {
//                 WorkExperienceGroup WorkExperienceGroup1 = WorkExperienceGroup.FirstOrDefault(x => x.Id == Groupint);
//                Nav.Add(WorkExperienceGroup1.Name);
//                Groupint = Convert.ToInt32(WorkExperienceGroup1.ParentID);
//            }
//            ViewBag.Nav = Nav;
//            var result = WorkExperienceGroup.Where(p => (WorkExperienceGroup.Any(p2 => p2.Id == p.ParentID) || p.ParentID == null) && p.WorkExperience.Count>0).ToList();
//            int WorkExpGp = 0;
//          if (result.Count > 0)
//          {
//              WorkExpGp =id;
//              var wrkExperience = _RWorkExperience.WorkExperiences.FirstOrDefault(x => x.WorkExperienceGroupId == WorkExpGp);
//              if (wrkExperience != null)
//              {
//                  ViewBag.WorkExp = wrkExperience;
//                  if (wrkExperience != null)
//                  {
//                      FillingRelatedViewBags(wrkExperience.Id, wrkExperience.WorkExperienceGroupId);
//                  }
//              }
//          }
//            return View();
//        }
//        [HttpPost]
//        public ActionResult GetList(int Id)
//        {
//            var WorkExperienceGroup = _RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == 2).ToList();
//            List<string> Nav = new List<string>();
//            int Groupint = Id;
//            while (Groupint != 0)
//            {
//                WorkExperienceGroup WorkExperienceGroup1 = WorkExperienceGroup.FirstOrDefault(x => x.Id == Groupint);
//                Nav.Add(WorkExperienceGroup1.Name);
//                Groupint = Convert.ToInt32(WorkExperienceGroup1.ParentID);
//            }
//            ViewBag.Nav = Nav;
//            ViewBag.WorkExp =_RWorkExperience.WorkExperiences.FirstOrDefault(x => x.WorkExperienceGroupId == Id);
//            return Json(new { Partial = RenderPartialViewToString("_Gallery", null), Partial1 = RenderPartialViewToString("_Nav", null) }, JsonRequestBehavior.AllowGet);
//        }
//        public string RenderPartialViewToString(string viewName, object model)
//        {
//            if (string.IsNullOrEmpty(viewName))
//                viewName = ControllerContext.RouteData.GetRequiredString("action");

//            ViewData.Model = model;

//            using (StringWriter sw = new StringWriter())
//            {
//                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
//                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
//                viewResult.View.Render(viewContext, sw);

//                return sw.GetStringBuilder().ToString();
//            }
//        }
//        public void InitializeData()
//        {
//            ViewBag.FooterColName = _RFooter.FooterColumnNames.Where(x => x.Name.Length > 0 && x.LanguageId == 2).OrderBy(x => x.Id).ToList();
//            ViewBag.ServiceGroup = _RService.ServiceGroups.Where(x => x.LanguageId == 2).ToList();
//            ViewBag.WorkExGroup = _RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == 2).ToList();
//        }


//        //written By Azizi 94_08_25
//        private void FillingRelatedViewBags(int workExperienceID, int groupID)
//        {
           

//            var workExperience = _RWorkExperience.WorkExperiences.FirstOrDefault(_ => _.Id == workExperienceID);

//            var workExperienceGrp = _RWorkExperience.WorkExperienceGroups.FirstOrDefault(_ => _.Id == groupID);

//            //metaDescription
//            if (!String.IsNullOrEmpty(workExperience.metaDescription))
//            {
//                ViewBag.MetaDescription = workExperience.metaDescription;
//            }
//            else
//            {
//                if (workExperience.LongDes != null)
//                {
//                    var meta = StripHTML(workExperience.LongDes);
//                    ViewBag.MetaDescription = meta.Length >= 1000 ? meta.Substring(0, 1000) : meta.Substring(0, meta.Length);
//                }
//                else
//                {
//                    ViewBag.MetaDescription = " ";
//                }

//            }

//            //title
//            if (!string.IsNullOrEmpty(workExperience.title))
//            {
//                ViewBag.Title = workExperience.title;

//            }
//            else
//            {
//                ViewBag.Title = workExperienceGrp.Name;
//            }

//            //keywords
//            if (!string.IsNullOrEmpty(workExperience.keyWords))
//            {
//                ViewBag.MetaKeywords = workExperience.keyWords;
//            }
//            else
//            {
//                ViewBag.MetaKeywords = "";
//            }
//        }


//        //written By Azizi 94_08_25
//        public static string StripHTML(string HTMLText, bool decode = true)
//        {
//            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
//            var stripped = reg.Replace(HTMLText, "");
//            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
//        }
//    }
//}
