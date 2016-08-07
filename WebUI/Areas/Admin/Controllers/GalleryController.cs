//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using WebUI.Infrastructure.Extentions.Admin;
//using Domain.Entities;
//using System.IO;
//
//using System.Text.RegularExpressions;
//using System.Web.Routing;
//using Repository.Abstract;

//namespace WebUI.Areas.Admin.Controllers
//{
//    public class GalleryController : Controller
//    {
     
//        IUserAccountRepository _RDefineUser;
//        IWorkExperienceRepository _RWorkExperience;
//        UserAccountExtentions EDefineUser;
//        int _LanguageId = 1;
//        public GalleryController(IUserAccountRepository DefineUserRepository, IWorkExperienceRepository WorkExperienceRepository)
//        {
//            _RDefineUser = DefineUserRepository;
//            _RWorkExperience = WorkExperienceRepository;
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

//        [HttpPost]
//        public ActionResult GetInfoWorkGroup(int id)
//        {
//            WorkExperienceGroup WorkExperienceGroup =_RWorkExperience.DetailsWorkExperienceGroup(id);
//            return Json(new { MenuNameTitle = WorkExperienceGroup.Name }, JsonRequestBehavior.AllowGet);
//        }
//        [HttpPost]
//        public ActionResult GetData(int id)
//        {
//          //  viewmodelNews
//            WorkExperience WorkExperience = _RWorkExperience.DetailsWorkExperience(id);
//        if (WorkExperience!=null)
//            TempData["Folder"] = WorkExperience.Id;
//            return Json(new { Partial = RenderPartialViewToString("~/Areas/Admin/Views/WorkExperience/_FileUpload.cshtml", null) }, JsonRequestBehavior.AllowGet);
//        }

//        [ValidateInput(false)]
//        [HttpPost]
//        public ActionResult SavePost(int Id,int GroupId ,string LongDes , string title ,string metaDescription ,string keywords, string alts)
//        {
//            if (IsValidSessions())
//            {
//                WorkExperience workExperience = _RWorkExperience.DetailsWorkExperience(Id);
//                if (workExperience==null)
//                { 
//                workExperience = new WorkExperience();
//                workExperience.LongDes = LongDes;
//                workExperience.WorkExperienceGroupId = GroupId;
//                workExperience.CreationDate = DateTime.Now.Date;


//                workExperience.title = title;
//                workExperience.metaDescription = metaDescription;
//                workExperience.keyWords = keywords;
                 
               
//                }
//                else
//                {
//                    workExperience.LongDes = LongDes;
//                    workExperience.ModifiedDate = DateTime.Now.Date;
                  
//                    workExperience.title = title;
//                    workExperience.metaDescription = metaDescription;
//                    workExperience.keyWords = keywords;
//                }

//                try
//                {
//                    if (workExperience.WorkExperiencesGallery != null && workExperience.WorkExperiencesGallery.Any())
//                    {
//                        var i = 0;
//                        var altsArray = alts.Split(new string[] { "|" }, StringSplitOptions.None);
//                        foreach (var item in workExperience.WorkExperiencesGallery)
//                        {
//                            item.alt = altsArray[i++];
//                            _RWorkExperience.SaveWorkExperienceGallery(item);
//                        }
//                    }
//                }
//                catch
//                {

//                }

//                _RWorkExperience.SaveWorkExperience(workExperience);
//                TempData["Folder"] = workExperience.Id;
//                return Json(new { Idms = workExperience.Id }, JsonRequestBehavior.AllowGet);
//            }
//            else
//                return RedirectToAction("Login", "Home");
//        }
//        [HttpGet]
//        public ActionResult GalleryList(int id = 0, int prev = 0)
//        {
//            if (IsValidSessions())
//            {
//                if (id == 133 && prev == 0)
//                    ViewBag.IsFirstRun = "Yes";

//                TempData["Folder"] = null;
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                ViewBag.MenuList =_RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == LanguageId).ToList();
//                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
//                var group = _RWorkExperience.WorkExperienceGroups.FirstOrDefault(_ => _.Id == id);
//                viewmodelWorkExperience vmWorkExperience = new viewmodelWorkExperience();
//              if (id!=0)
//              {
//                  ViewBag.IsNode = "Yes";     
//                  ViewBag.prev = prev;
//                  ViewBag.Ids = id;
                  
//                  WorkExperience WorkExperience = _RWorkExperience.WorkExperiences.FirstOrDefault(x=>x.WorkExperienceGroupId==id);
//                  string navigation = "";
//                  if (WorkExperience!=null)
//                  {
//                    string meta = string.Empty;
//                    navigation = GetNavigation(WorkExperience.WorkExperienceGroupId);
//                    var title = group.Name;
//                    if (WorkExperience.WorkExperiencesGallery != null && WorkExperience.WorkExperiencesGallery.Count > 0)
//                      TempData["Folder"] = WorkExperience.Id;
//                     vmWorkExperience.Id=WorkExperience.Id;
//                     vmWorkExperience.LongDes = WorkExperience.LongDes;

//                     if (WorkExperience.WorkExperiencesGallery != null)
//                     {
//                         ViewBag.alts = string.Join("|", WorkExperience.WorkExperiencesGallery.Select(_ => _.alt).ToArray());
//                     }

//                      //written By Azizi 94_08_25
//                     vmWorkExperience.keyWords = WorkExperience.keyWords ?? "";
//                      vmWorkExperience.title = !string.IsNullOrEmpty(WorkExperience.title) ? WorkExperience.title : title;

//                      if(!string.IsNullOrEmpty(WorkExperience.metaDescription))
//                      {
//                          vmWorkExperience.metaDescription =  WorkExperience.metaDescription;
//                      }
//                      else
//                      {
//                          if (WorkExperience.LongDes != null)
//                          {
//                              meta = StripHTML(WorkExperience.LongDes);
//                              vmWorkExperience.metaDescription = meta.Length >= 1000 ? meta.Substring(0, 1000) : meta.Substring(0, meta.Length);
//                          }
//                          else
//                          {
//                              vmWorkExperience.metaDescription = "";
//                          }

//                      }
//                  }
//                else
//                {
//                     navigation = GetNavigation(id);
//                    vmWorkExperience.Id = 0;
//                    vmWorkExperience.LongDes = "";

//                    //written By Azizi 94_08_25
//                    vmWorkExperience.keyWords = "";
//                    vmWorkExperience.title = group != null ? group.Name : " ";
//                    vmWorkExperience.metaDescription = "";

//                }
//                  SplitNavigation(navigation);
//                ViewBag.Items = vmWorkExperience;
//            }

//              return View("WorkExperienceList");
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
//        public ActionResult AddParentMenu(int? ParentId, string WhichOpen, string MenuTextHtml, string ParentMenuName, string Type, int Priority)
//        {
//            if (IsValidSessions())
//            {
//                WorkExperienceGroup WorkExperienceGroup = new WorkExperienceGroup();
//                if (Type == "add")
//                {
//                    int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                    WorkExperienceGroup.Name = ParentMenuName;
//                    WorkExperienceGroup.ParentID = ParentId;
//                    WorkExperienceGroup.Priority = Priority;
//                    WorkExperienceGroup.LanguageId = Convert.ToInt32(Session["Language"].ToString());
                 
//                    _RWorkExperience.SaveWorkExperienceGroup(WorkExperienceGroup);
//                    ViewBag.MenuList =_RWorkExperience.WorkExperienceGroups.Where(x=>x.LanguageId==LanguageId).ToList();
//                    return PartialView("_WorkExperienceGroupTreeview");
//                }
//                else
//                {
//                    WorkExperienceGroup =_RWorkExperience.DetailsWorkExperienceGroup(Convert.ToInt32(ParentId));
//                    WorkExperienceGroup.Name = ParentMenuName;
//                    _RWorkExperience.SaveWorkExperienceGroup(WorkExperienceGroup);
//                    return PartialView("_asdasd");
//                }
//            }
//            else
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//        }
//        [HttpPost]
//        public ActionResult GetInfoGalleryGroup(int id)
//        {
//            WorkExperienceGroup WorkExperienceGroup =_RWorkExperience.DetailsWorkExperienceGroup(id);
//            return Json(new { MenuNameTitle = WorkExperienceGroup.Name }, JsonRequestBehavior.AllowGet);
//        }
//        [HttpPost]
//        public ActionResult HasTab(int id)
//        {
//         int Count =_RWorkExperience.WorkExperiences.Count(x => x.WorkExperienceGroupId == id);


//         return Json(new { Count = Count }, JsonRequestBehavior.AllowGet);
//        }
//        [HttpPost]
//        public ActionResult GetPriority(int? id)
//        {
//            int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//            IQueryable<WorkExperienceGroup> WorkExperienceGroupList = (id == null) ?_RWorkExperience.WorkExperienceGroups.Where(p => p.ParentID == null && p.LanguageId == LanguageId) :_RWorkExperience.WorkExperienceGroups.Where(p => p.ParentID == id && p.LanguageId == LanguageId);
//            int Priority = 1;
//            if (WorkExperienceGroupList.Any())
//                Priority = WorkExperienceGroupList.Max(p => p.Priority) + 1;
//            return Json(new { Priority = Priority }, JsonRequestBehavior.AllowGet);

//        }
//        [HttpPost]
//        public ActionResult DeleteGalleryGroup(int Id)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                WorkExperienceGroup WorkExperienceGroup =_RWorkExperience.DetailsWorkExperienceGroup(Id);
//                int Priority = WorkExperienceGroup.Priority;
//                WorkExperience WorkExperience = _RWorkExperience.WorkExperiences.FirstOrDefault(x => x.WorkExperienceGroupId == Id);
//                if (WorkExperience!=null)
//                { 
//                if (Directory.Exists(Server.MapPath("~/Files/Gallery/" + WorkExperience.Id.ToString())))
//                {
//                    DeleteDirectory(Server.MapPath("~/Files/Gallery/" + WorkExperience.Id.ToString()));
//                }
//                }
//                _RWorkExperience.DeleteWorkExperienceGroup(WorkExperienceGroup);
//                ViewBag.MenuList =_RWorkExperience.WorkExperienceGroups.Where(x=>x.LanguageId==LanguageId).ToList();
//                DeleteNodes(Id);
//                List<WorkExperienceGroup> UserMenuList = ViewBag.MenuList;
//                UserMenuList = UserMenuList.Where(p => p.ParentID == WorkExperienceGroup.ParentID && p.Priority > Priority).OrderBy(x => x.Priority).ToList();
//                foreach (var item in UserMenuList)
//                {
//                    item.Priority = Priority;
//                    _RWorkExperience.SaveWorkExperienceGroup(item);
//                    Priority++;
//                }
//                return PartialView("_WorkExperienceGroupTreeview");
//            }
//            else
//                return JavaScript("window.location.href ='/Admin/Home/Login';");
//        }
//        [HttpPost]
//        public ActionResult UpDownGalleryGroup(int CurrentId, int PrevId)
//        {
//            if (IsValidSessions())
//            {
//                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
//                List<WorkExperienceGroup> UserMenuList =_RWorkExperience.WorkExperienceGroups.Where(p =>( p.Id == CurrentId || p.Id == PrevId) && p.LanguageId==LanguageId).ToList();
//                int Priority = UserMenuList[1].Priority;
//                UserMenuList[1].Priority = UserMenuList[0].Priority;
//                UserMenuList[0].Priority = Priority;
//                foreach (var item in UserMenuList)
//                {
//                    _RWorkExperience.SaveWorkExperienceGroup(item);
                   
//                }
//                return Json("", JsonRequestBehavior.AllowGet);
//            }
//            else
//                return JavaScript("window.location.href ='/Admin/Home/Login';");

//        }

//        private void DeleteNodes(int? ParentID)
//        {
//            // string host = Request.Url.Host.ToLower();
//            List<WorkExperienceGroup> UserMenuList = ViewBag.MenuList;
//            UserMenuList = UserMenuList.Where(p => p.ParentID == ParentID).ToList();
//            int CountUserMenuList = UserMenuList.Count;
//            int GroupId=0;
//            if (CountUserMenuList > 0)
//            {
//                for (int i = 0; i < CountUserMenuList; i++)
//                {
//                    GroupId=UserMenuList[i].Id;
//                    WorkExperience WorkExperience = _RWorkExperience.WorkExperiences.FirstOrDefault(x => x.WorkExperienceGroupId == GroupId);
//                    if (WorkExperience!=null)
//                    {
//                    if (Directory.Exists(Server.MapPath("~/Files/Gallery/" + WorkExperience.Id.ToString())))
//                    {
//                        DeleteDirectory(Server.MapPath("~/Files/Gallery/" + WorkExperience.Id.ToString()));
//                    }
//                    _RWorkExperience.DeleteWorkExperienceGroup(UserMenuList[i]);
//                     }
//                    DeleteNodes(UserMenuList[i].Id);
//                }
//            }
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

//        private bool IsValidSessions()
//        {
//            if (Session["admin"] != null)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//        public bool ThumbnailCallback()
//        {
//            return false;
//        }
//        public string  GetNavigation(int? id)
//        {
//            string name = "";
//          WorkExperienceGroup WorkExperienceGroup= _RWorkExperience.DetailsWorkExperienceGroup(Convert.ToInt32( id));

//          if (WorkExperienceGroup != null)
//              name = WorkExperienceGroup.Name +">"+ GetNavigation(WorkExperienceGroup.ParentID);
//          return name;
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
//        public void SplitNavigation(string navigation)
//        {
//            string[] Splitnav = navigation.Split('>');
//            navigation = "";
//            for (int i = Splitnav.Count() - 1; i >= 0; i--)
//            {
//                navigation = navigation + ">" + Splitnav[i];

//            }
//            try
//            {
//                ViewBag.navigation = navigation.Remove(0, 2);
//            }
//            catch
//            {
//                ViewBag.navigation = navigation.Remove(0, 1);
//            }
//        }
//    }
//}
