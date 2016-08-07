using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Infrastructure.Extentions.Admin;
using Domain.Entities;
using System.IO;
using FileUpload.Web.Models;
using WebUI.Infrastructure.Utility;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using System.Web.Helpers;
using System.Text.RegularExpressions;
using System.Transactions;
using Repository.Abstract;

namespace WebUI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        IUserAccountRepository _RDefineUser;
        IServiceRepository _RService;
        ISettingRepository _RSetting;

        ServiceExtentions EService;
        UserAccountExtentions EDefineUser;

        public ProductController(ISettingRepository SettingRepository, IUserAccountRepository DefineUserRepository,
            IServiceRepository ServiceRepository)
        {
            _RDefineUser = DefineUserRepository;
            _RService = ServiceRepository;
            _RSetting = SettingRepository;

            EService = new ServiceExtentions(_RService, _RDefineUser);
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }

        [HttpGet]
        public ActionResult EditPost(int Id) 
        {
            if (IsValidSessions())
            {
                ServiceTab ServiceTab = _RService.DetailsServiceTab(Id);
                ViewBag.Cnt = ServiceTab.ServiceTabFile.Count();

                viewmodelService viewmodelService = new viewmodelService()
                {
                    TabName = ServiceTab.Name,
                    Tags = ServiceTab.Tags,
                    ShortDes = ServiceTab.ShortText,
                    Id = ServiceTab.Id,
                    LongDes = ServiceTab.TabTypeText,
                    Title = ServiceTab.Title,
                    IsExist = ServiceTab.IsExist ,
                    Price = ServiceTab.Price.Value,
                    ProductUnit = ServiceTab.ProductUnit ,
                    Discount = ServiceTab.Discount.Value
                };

                var images = _RService.ServiceTabFiles.Where(_ => _.ServiceTabId == ServiceTab.Id);
                List<Domain.ViewModel.Mutual.FileUpload> list = new List<Domain.ViewModel.Mutual.FileUpload>();

                foreach (var item in images)
                {
                    var obj = new Domain.ViewModel.Mutual.FileUpload()
                    {
                        description = item.Description,
                        url = $"/Files/ServiceGallery/{ServiceTab.Id}/{item.File}",
                        fileName = item.File,
                        id = item.Id,
                        title = item.Alt
                    };
                    list.Add(obj);
                }

                ViewBag.Images = Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.None);


                TempData["FolderId"] = Id;
                if (ServiceTab.Name == "شرح محصول" || ServiceTab.Name == "Product Description")
                {
                    TempData["HasTitle"] = true;
                }
                else
                {
                    TempData["HasTitle"] = false;
                }
                return View(viewmodelService);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult EditPost(string Title, string ShortDes, string LongDes,
            bool IsShow, string TabName, string Tags, int Price, int? Discount, bool IsExist, string ProductUnit,
            ICollection<Domain.ViewModel.Mutual.FileUpload> files)
        {
            if (IsValidSessions())
            {
                int FolderId = Convert.ToInt32(TempData["FolderId"]);
                TempData["FolderId"] = FolderId;

                ServiceTab ServiceTab = _RService.DetailsServiceTab(FolderId);

                ServiceTab.TabTypeText = LongDes;
                ServiceTab.Title = Title;
                ServiceTab.Name = TabName;
                ServiceTab.Tags = Tags;
                ServiceTab.IsShow = IsShow;
                ServiceTab.ShortText = ShortDes;
                ServiceTab.Price = Price;
                if (Discount.HasValue)
                    ServiceTab.Discount = Discount;
                ServiceTab.ModifiedDate = DateTime.Now.Date;
                ServiceTab.IsExist = IsExist;
                ServiceTab.ProductUnit = ProductUnit;
                _RService.SaveServiceTab(ServiceTab);


                #region Images CRUD

                var oldfiles = _RService.ServiceTabFiles.Where(_ => _.ServiceTabId == FolderId).ToList();
                var uploads = files.Where(_ => _.id.HasValue).Select(_ => _.id.Value).ToList();

                var deletedIds = oldfiles.Where(_ => !uploads.Contains(_.Id)).Select(_ => _.Id);

                foreach (var del in deletedIds.ToList())
                {
                    //delete its row from database
                    var file = _RService.DetailsServiceTabFile(del);
                    _RService.DeleteServiceTabFile(file);

                    //delete the file itself in its folder
                    if (System.IO.File.Exists(Server.MapPath($"~/Files/ServiceGallery/{FolderId}/{file.File}")))
                    {
                        System.IO.File.Delete(Server.MapPath($"~/Files/ServiceGallery/{FolderId}/{file.File}"));
                    }
                }

                if (files != null)
                {
                    foreach (var img in files)
                    {
                        if (System.IO.File.Exists(Server.MapPath($"~/Upload/{img.fileName}")))
                        {
                            if ((img.id == null || img.id == 0))
                            {
                                MoveTemporaryImage(img, ServiceTab.Id);
                            }
                        }
                        else if (img.id != null && img.id != 0)//it has uploaded and save before so update the fields of database
                        {
                            var dbEntity = _RService.ServiceTabFiles.FirstOrDefault(_ => _.Id == img.id);
                            dbEntity.Alt = img.title;
                            dbEntity.Description = img.description;

                            _RService.SaveServiceTabFile(dbEntity);
                        }
                    }
                }
                #endregion
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return Json(new { isok = "yes", JsonRequestBehavior.AllowGet });
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            if (!IsValidSessions()) return RedirectToAction("Login", "Home");
            Tabs Tab = (Tabs)TempData["Tabs"];
            if (Tab != null)
            {
                TempData["Tabs"] = Tab;
                TempData["FolderId"] = null;
                if (Tab.TabName == "شرح محصول" || Tab.TabName == "Product Description")
                {
                    TempData["HasTitle"] = true;
                }
                else
                {
                    TempData["HasTitle"] = false;
                }
                return View();
            }

            return RedirectToAction("ProductList");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SavePost(string Title, string ShortDes, string LongDes, bool IsShow, string Tags,
            int Price, int? Discount, bool IsExist, string ProductUnit, ICollection<Domain.ViewModel.Mutual.FileUpload> files)
        {
            if (IsValidSessions())
            {

                //TempData["result"] = "OK";
                Tabs Tab = (Tabs)TempData["Tabs"];
                int value1 = Tab.GroupId;
                string TabName = Tab.TabName;
                int ServiceTabId = Tab.TabId;
                ServiceTab ServiceTab = new ServiceTab();

                ServiceTab.Name = TabName;
                ServiceTab.ServiceGroupId = Convert.ToInt32(value1);
                ServiceTab.TabType = 1;
                ServiceTab.IsShow = IsShow;
                ServiceTab.Title = Title;
                ServiceTab.ProductUnit = ProductUnit;
                ServiceTab.ShortText = ShortDes;
                ServiceTab.TabTypeText = LongDes;
                ServiceTab.Price = Price;
                ServiceTab.IsExist = IsExist;



                if (Discount.HasValue)
                {
                    ServiceTab.Discount = Discount;
                    if (Price < Discount.Value)
                    {
                        TempData["Message"] = "امکان ثبت تخفیف بیش از قیمت وجود ندارد.";
                        return Json(false);
                    }
                }
                else
                {
                    ServiceTab.Discount = 0;
                }

                ServiceTab.Tags = Tags;
                ServiceTab.CreationDate = DateTime.Now.Date;
                _RService.SaveServiceTab(ServiceTab);

                //Update all its serviceGroup  grandsonCount of its ancestors 
                if (TabName == "شرح محصول" || TabName == "Product Description")
                {
                    UpdateGrandSonNumbers(ServiceTab.ServiceGroupId, true);
                }

                //update hasTab of parent servicegroup
                var parentNode = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == Tab.GroupId);
                parentNode.HasTab = true;
                _RService.SaveServiceGroup(parentNode);

                TempData["FolderId"] = ServiceTab.Id;
                if (files != null)
                {
                    foreach (var img in files)
                    {
                        if (System.IO.File.Exists(Server.MapPath($"~/Upload/{img.fileName}")))
                        {
                            MoveTemporaryImage(img, ServiceTab.Id);
                        }
                    }
                }

                TempData.Keep("FolderId");
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";

                return Json(new { Idms = ServiceTab.Id }, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Login", "Home");
        }

        private bool MoveTemporaryImage(Domain.ViewModel.Mutual.FileUpload image, int productId)
        {

            var extension = Path.GetExtension(image.fileName);
            var newName = DateTime.Now.Ticks.ToString() + extension;

            var path = $"/Files/ServiceGallery/{productId}";
            var serverPath = Server.MapPath($"~/Files/ServiceGallery/{productId}");

            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            System.IO.File.Move(Server.MapPath($"~/Upload/{image.fileName}"), serverPath + "/" + newName);

            var dBRecord = new ServiceTabFile()
            {
                File = newName,
                Alt = image.title,
                Description = image.description,
                ServiceTabId = productId
            };

            return _RService.SaveServiceTabFile(dBRecord);
        }

        [HttpPost]
        public ActionResult DeleteService(int Id, int Page) //Id is serviceTabId
        {
            if (IsValidSessions())
            {
                ServiceTab ServiceTab = _RService.DetailsServiceTab(Id);

                //if its tabName is ProductDescription in persian or english and it has any siblings it coudnt delete the node
                var gId = ServiceTab.ServiceGroupId;
                if ((ServiceTab.Name == "Product Description" || ServiceTab.Name == "شرح محصول") &&
                    _RService.TabsCountOfSpecificserviceGroup(gId) > 1)
                    return View("ProductList");

                //if this product has any order it cant be deleted too
                if ((ServiceTab.Name == "Product Description" || ServiceTab.Name == "شرح محصول") &&
                    _RService.HasAnyOrder(gId))
                {
                    return View("ProductList");
                }

                if (ServiceTab.TabType == 2)
                {
                    List<ServiceTabFile> List = _RService.ServiceTabFiles.Where(x => x.ServiceTabId == Id).ToList();
                    foreach (var item in List)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/TabGalleryService/" + item.File));
                        System.IO.File.Delete(Server.MapPath("~/Images/TabGalleryService/thum/" + item.File));
                    }
                }
                if (ServiceTab.TabType == 1)
                {
                    if (Directory.Exists(Server.MapPath("~/Files/ServiceGallery/" + Id)))
                    {
                        DeleteDirectory(Server.MapPath("~/Files/ServiceGallery/" + Id));
                    }
                }


                var tabName = ServiceTab.Name;
                _RService.DeleteServiceTab(ServiceTab);

                //after delete we should check all grandsonNumber field of its ancestors
                if (tabName == "Product Description" || tabName == "شرح محصول")
                {
                    UpdateGrandSonNumbers(gId, false);
                }


                int count = EService.GetCountServiceTabAdmin(ServiceTab.ServiceGroupId);
                TempData["Count"] = count;
                if (count % 5 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                GetServiceTabList(ServiceTab.ServiceGroupId, Page);
                return
                    Json(
                        new
                        {
                            Partial =
                                RenderPartialViewToString("~/Areas/Admin/Views/Product/_ServiceList.cshtml", null)
                        },
                        JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase image)
        {
            //var infos = data.Select(_=>Newtonsoft.Json.JsonConvert.DeserializeObject<ProductViewModel>(_)).ToList();

            //if (IsValidSessions())
            //{
            //addon();

            int userId = Convert.ToInt32(TempData["admin"]);
            string filePath = Common.CommonMethods.TemporarySaveImage(image, userId, 1);

            TempData["ImagePath"] = filePath;
            string relativePath = "";
            if (filePath != "")
            {
                var fileName = Path.GetFileName(filePath);
                relativePath = $"../../Images/{userId}/{fileName}";
            }

            return Json(new { RelativePath = relativePath });
            //}
            // return Json(false);
        }


        //written By Azizi 95_03_23
        private void UpdateGrandSonNumbers(int serviceGroupId, bool isIncrease, int count = 1)
        {
            var currentGroup = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == serviceGroupId);


            if (isIncrease)
            {
                currentGroup.GrandsonsCount += count;
                _RService.SaveServiceGroup(currentGroup);

                if (currentGroup.ParentID != null)
                {
                    UpdateGrandSonNumbers(currentGroup.ParentID.Value, true);
                }
                else
                {

                    return;
                }

            }
            else
            {
                currentGroup.GrandsonsCount -= count;
                _RService.SaveServiceGroup(currentGroup);

                if (currentGroup.ParentID != null)
                {
                    UpdateGrandSonNumbers(currentGroup.ParentID.Value, false);
                }
                else
                {
                    return;
                }
            }


        }

        [HttpPost]
        public ActionResult ChangeIsHomeState(int Id)
        {
            if (IsValidSessions())
            {
                var group = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == Id);
                if (group != null)
                {
                    group.IsHome = !group.IsHome;
                    _RService.SaveServiceGroup(group);
                }
                return Json(new { ServiceGroupId = Id, IsHome = group.IsHome });
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult EditGallery(validationEditGalleryTab validationEditGalleryTab, string alts)
        {
            if (IsValidSessions())
            {
                //  string filename = "";
                bool isok = true;
                GetFileExtension Ext = new GetFileExtension();
                ServiceTab ServiceTab = _RService.DetailsServiceTab(validationEditGalleryTab.id);

                if (ServiceTab.ServiceTabFile != null)
                {
                    var altsArray = string.IsNullOrEmpty(alts) ? new string[0] { } : alts.Split(new string[] { "|" }, StringSplitOptions.None);
                    foreach (var item in ServiceTab.ServiceTabFile)
                    {
                        if (altsArray.Length > 0)
                        {
                            foreach (var i in altsArray)
                            {
                                var j = i.Split(new string[] { ":" }, StringSplitOptions.None);

                                if (int.Parse(j[0]) == item.Id)
                                {
                                    item.Alt = j[1];
                                }
                            }
                        }
                    }
                }

                if (isok)
                {
                    ServiceTab.Name = validationEditGalleryTab.TabName;
                    ServiceTab.ModifiedDate = DateTime.Now.Date;

                    _RService.SaveServiceTab(ServiceTab);
                    TempData["result"] = "OK";
                    TempData["Message"] = "عملیات با موفقیت انجام شد.";
                    return RedirectToAction("ProductList");
                }
                ViewBag.ServiceTabFileList = EService.GetServiceTabFileAdmin(0, 5, validationEditGalleryTab.id).ToList();
                TempData["Count"] = EService.GetCountServiceTabFileAdmin(validationEditGalleryTab.id);
                ViewBag.PageNumber = 1;
                return View();
            }
            else
                return RedirectToAction("Login", "Home");


        }
        [HttpPost]
        public ActionResult DeleteImageGallery(int Id, int Page)
        {
            if (IsValidSessions())
            {
                var set2 = _RSetting.Settings.FirstOrDefault();

                try
                {
                    ServiceTabFile ServiceTabFile = _RService.DetailsServiceTabFile(Id);
                    EService.DeleteImageGallery(ServiceTabFile);
                    int count = EService.GetCountServiceTabFileAdmin(ServiceTabFile.ServiceTabId);
                    TempData["Count"] = count;
                    if (count % 5 == 0)
                    {
                        Page = Page - 1;
                    }
                    TempData["result"] = "OK";
                    TempData["Message"] = "عملیات با موفقیت انجام شد.";
                    return RedirectToAction("RefreshGallery", new { Page = Page, TabId = ServiceTabFile.ServiceTabId });
                }
                catch (Exception ex)
                {
                    var set3 = _RSetting.Settings.FirstOrDefault();

                    return RedirectToAction("Login", "Home");

                }


            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult RefreshGallery(int Page = 1, int TabId = 0)
        {
            if (IsValidSessions())
            {
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 5;

                ViewBag.ServiceTabFileList = EService.GetServiceTabFileAdmin(Start, 5, TabId).ToList();
                ViewBag.PageNumber = Page;
                return PartialView("_EditGallery");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }
        [HttpGet]
        public ActionResult EditGallery(int id)
        {
            if (IsValidSessions())
            {
                ViewBag.ServiceTabFileList = EService.GetServiceTabFileAdmin(0, 5, id).ToList();
                TempData["Count"] = EService.GetCountServiceTabFileAdmin(id);
                validationEditGalleryTab validationEditGalleryTab = new validationEditGalleryTab() { id = id, TabName = _RService.DetailsServiceTab(id).Name };
                ViewBag.PageNumber = 1;

                ServiceTab ServiceTab = _RService.DetailsServiceTab(validationEditGalleryTab.id);
                ViewBag.alts = string.Join("|", ServiceTab.ServiceTabFile.Select(_ => _.Alt).ToArray());

                return View(validationEditGalleryTab);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult EditService(int id)
        {
            if (IsValidSessions())
            {
                ServiceTab ServiceTab = _RService.DetailsServiceTab(id);
                int TabType = ServiceTab.TabType;
                switch (TabType)
                {
                    case 1:
                        {
                            return RedirectToAction("EditPost", "Product", new { Area = "Admin", id = ServiceTab.Id });

                        }
                    case 2:
                        {
                            return RedirectToAction("EditGallery", "Product", new { Area = "Admin", id = ServiceTab.Id });

                        }
                    case 4:
                        {

                            return RedirectToAction("EditText", "Product", new { Area = "Admin", id = ServiceTab.Id });
                        }

                }
                return View();
            }

            return RedirectToAction("Login", "Home");
        }
     
      

        [HttpGet]
        public ActionResult AddFlash()
        {
            if (IsValidSessions())
            {
                Tabs Tab = (Tabs)TempData["Tabs"];

                if (Tab != null)
                {
                    return View();
                }
                else
                    return RedirectToAction("ProductList");
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AddFlash(HttpPostedFileBase FlashFile)
        {
            if (IsValidSessions())
            {
                Tabs Tabs = (Tabs)TempData["Tabs"];
                int value1 = Tabs.GroupId;
                string TabName = Tabs.TabName;
                int ServiceTabId = Tabs.TabId;
                // TempData["Tabs"] = Tabs;
                bool isValid1 = false;
                string filename1 = "";
                if (FlashFile != null)
                {
                    if (FlashFile.ContentLength > 0)
                    {
                        filename1 = SaveFlash(FlashFile, null);
                        if (filename1 != "")
                        {
                            if (_RService.DetailsServiceTab(ServiceTabId) == null)
                            {
                                ServiceTab ServiceTab = new ServiceTab()
                                {
                                    CreationDate = DateTime.Now.Date,
                                    Name = TabName,
                                    ServiceGroupId = Convert.ToInt32(value1),
                                    TabType = 3

                                };
                                _RService.SaveServiceTab(ServiceTab);
                                ServiceTabId = ServiceTab.Id;
                                Tabs.TabId = ServiceTab.Id;
                            }
                            isValid1 = true;
                            TempData["result"] = "OK";
                            TempData["Message"] = "عملیات با موفقیت ثبت شد.";
                        }
                        else
                        {
                            TempData["result"] = "Error";
                            TempData["Message"] = "فایل ارسالی مجاز نمی باشد";
                            return View();
                        }
                    }

                    if (isValid1)
                    {
                        ServiceTabFile ServiceTabFile = new ServiceTabFile();
                        ServiceTabFile.File = filename1;
                        ServiceTabFile.ServiceTabId = ServiceTabId;
                        _RService.SaveServiceTabFile(ServiceTabFile);
                        ViewBag.FileName = filename1;
                        TempData["result"] = "OK";
                        TempData["Message"] = "عملیات با موفقیت ثبت شد.";
                    }
                }
                //  TempData["Tabs"] = Tabs;
                return RedirectToAction("ProductList");
            }
            else
                return RedirectToAction("Login", "Home");
        }
      
       
        [HttpPost]
        public ActionResult UploadFiles(SelectedFileModel elem)
        {
            if (IsValidSessions())
            {
                var fileName = elem.fileName;

                GetFileExtension Ext = new GetFileExtension();
                if (Ext.GetExtension(fileName) == "jpg" ||
                    Ext.GetExtension(fileName) == "png" ||
                    Ext.GetExtension(fileName) == "jpeg" ||
                    Ext.GetExtension(fileName) == "png")
                {
                    Tabs Tab = (Tabs)TempData["Tabs"];

                    int value1 = Tab.GroupId;
                    string TabName = Tab.TabName;
                    int ServiceTabId = Tab.TabId;

                    if (_RService.DetailsServiceTab(ServiceTabId) == null)
                    {
                        ServiceTab ServiceTab = new ServiceTab()
                        {
                            CreationDate = DateTime.Now.Date,
                            Name = TabName,
                            ServiceGroupId = Convert.ToInt32(value1),
                            TabType = 2

                        };
                        _RService.SaveServiceTab(ServiceTab);
                        ServiceTabId = ServiceTab.Id;
                        Tab.TabId = ServiceTab.Id;
                    }
                    TempData["Tabs"] = Tab;

                    var fileContent = elem.fileContent;
                    var category = elem.category;
                    var title = elem.title;
                    var base64String = fileContent.Split(',')[1];
                    fileName = DateTime.Now.Ticks + "_" + category + "_" + title + "_" +
                        CommonMethods.ChangeUnKnownCharacters(Path.GetFileNameWithoutExtension(fileName))
                        + "." + Ext.GetExtension(fileName);
                    ServiceTabFile ServiceTabFile = new ServiceTabFile
                    {
                        File = fileName,
                        ServiceTabId = ServiceTabId
                    };
                    _RService.SaveServiceTabFile(ServiceTabFile);
                    var filePath = Server.MapPath("~/Images/TabGalleryService/") + fileName;
                    var bytes = Convert.FromBase64String(base64String);

                    System.IO.File.WriteAllBytes(filePath, bytes);

                    ImageResizer.ImageBuilder.Current.Build(Server.MapPath("~/Images/TabGalleryService/")
                        + fileName,
                        Server.MapPath("~/Images/TabGalleryService/thum/") + fileName,
                        new ImageResizer.ResizeSettings(200, 200, ImageResizer.FitMode.Crop, ""));

                    #region Comments
                    //ImageResizer.ImageBuilder.Current.Build(Server.MapPath("~/Images/TabGalleryService/") + fileName,
                    //    Server.MapPath("~/Images/TabGalleryService/") + fileName,
                    //    new ImageResizer.ResizeSettings(400, 400, ImageResizer.FitMode.Stretch, ""));

                    //System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(Server.MapPath("~/Images/TabGalleryService/" + fileName));
                    //System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = (ThumbnailCallback);
                    //int height = fullSizeImg.Height;
                    //int width = fullSizeImg.Width;
                    //if (height > width)
                    //{
                    //    height = (height * 200) / width;
                    //    width = 200;
                    //}
                    //else
                    //{
                    //    width = (width * 200) / height;
                    //    height = 200;
                    //}
                    //System.Drawing.Image thumbNailImg = fullSizeImg.GetThumbnailImage(width, height, dummyCallBack, IntPtr.Zero);
                    //thumbNailImg.Save(Server.MapPath("~/Images/TabGalleryService/thum/") + fileName);

                    //ImageResizer.ImageBuilder.Current.Build(Server.MapPath("~/Images/TabGalleryService/thum/") + fileName, Server.MapPath("~/Images/TabGalleryService/thum/") + fileName, new ImageResizer.ResizeSettings(200, 200, ImageResizer.FitMode.Crop, ""));
                    //thumbNailImg.Dispose();
                    //fullSizeImg.Dispose();
                    #endregion

                    TempData["result"] = "OK";
                    TempData["Message"] = "عملیات با موفقیت ثبت شد.";
                    return Json(fileName, JsonRequestBehavior.AllowGet);
                }
                return Json("error", JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Login", "Home");
        }



        [HttpPost]
        public ActionResult GetList(int id, int Page = 1)
        {
            GetServiceTabList(id, Page);
            return Json(
                new
                {
                    Partial = RenderPartialViewToString("~/Areas/Admin/Views/Product/_ServiceList.cshtml", null),
                    ServiceGroupId = id
                },
                JsonRequestBehavior.AllowGet);
        }
        //modified By Aziz 94_09_01
        [HttpGet]
        public ActionResult AddTab(int id)
        {
            if (!IsValidSessions()) return RedirectToAction("Login", "Home");
            TempData["ServiceGroup"] = id;
            if (_RService.IsLastLevelNodeInGroup(id) && _RService.HasNotAnyTab(id))
            {
                if (Session["Language"] != null)
                {
                    int langId = Convert.ToInt32(Session["Language"].ToString());
                    return AddTab(langId == 2 ? "Product Description" : "شرح محصول", 1);
                }
                return AddTab("شرح محصول", 1);
            }

            return View("AddTab");

        }
        //modified By Aziz 94_09_01
        [HttpPost]
        public ActionResult AddTab(string TabName, int dpTabType)
        {
            if (IsValidSessions())
            {
                int value = Convert.ToInt32(TempData["ServiceGroup"]);
                TempData["ServiceGroup"] = value;
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                if (_RService.ServiceGroups.Any(x => x.ParentID == value && x.LanguageId == LanguageId))
                {
                    TempData["result"] = "Error";
                    TempData["Message"] = "خطا در ثبت اطلاعات";
                    return View();
                }
                else
                {
                    Tabs Tabs = new Tabs()
                    {
                        GroupId = value,
                        TabId = 0,
                        TabName = TabName
                    };
                    TempData["Tabs"] = Tabs;
                    switch (dpTabType)
                    {
                        case 1:
                            {
                                return RedirectToAction("AddPost");
                            }
                        case 2:
                            {
                                return RedirectToAction("AddGallery");
                            }
                    }
                    return RedirectToAction("AddInfo");
                }

            }
            else
                return RedirectToAction("Login", "Home");
        }

        //writtenBy Azizi 95_03_12
        [HttpPost]
        public ActionResult UploadMainImage(validationImage imageValidation, string alt)
        {
            if (IsValidSessions())
            {
                string filename = SaveImage(imageValidation.ImageBanner, alt);
                if (filename != "")
                {
                    TempData["result"] = "OK";
                    TempData["Message"] = "عملیات با موفقیت انجام شد.";
                    ViewData.ModelState.Clear();
                }
                else
                {
                    TempData["result"] = "Error";
                    TempData["Message"] = "فایل ارسالی مجاز نمی باشد";

                }

                return View("CompanyIntroductionImage");
            }
            else
                return RedirectToAction("Login", "Home");
        }

        //Azizi
        [HttpGet]
        public ActionResult SeoMng(int id)
        {
            if (IsValidSessions())
            {
                string description = string.Empty;
                var viewModel = new viewModelServiceSeo();

                var serviceGroup = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == id);
                var servicetabs = _RService.ServiceTabs.Where(_ => _.ServiceGroupId == id).ToList();

                if (!string.IsNullOrEmpty(serviceGroup.metaDescription))
                {
                    viewModel.metaDescription = serviceGroup.metaDescription;
                }
                else
                {
                    description = StripHTML(servicetabs[0].TabTypeText);
                    viewModel.metaDescription = description.Length > 1000 ? description.Substring(0, 1000) : description.Substring(0, description.Length);

                }

                if (!string.IsNullOrEmpty(serviceGroup.Title))
                {
                    viewModel.title = serviceGroup.Title;

                }
                else
                {
                    viewModel.title = serviceGroup.Name;
                }

                viewModel.id = id;

                if (string.IsNullOrEmpty(serviceGroup.keywords))
                {
                    foreach (var tab in servicetabs)
                    {
                        if (!string.IsNullOrEmpty(tab.Tags))
                            viewModel.keyWords += tab.Tags + ",";
                    }
                }
                else
                {
                    viewModel.keyWords = serviceGroup.keywords;
                }

                return View(viewModel);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        //Azizi
        [HttpPost]
        public ActionResult SeoMng(string title, string metaDescription, string keywords, int id)
        {
            if (IsValidSessions())
            {
                var item = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == id);

                item.Title = title;
                item.metaDescription = metaDescription;
                item.keywords = keywords;

                _RService.SaveServiceGroup(item);

                return Json(true);
            }

            return Json(false);
        }

        [HttpGet]
        public ActionResult AddGallery()
        {
            if (IsValidSessions())
            {
                Tabs Tab = (Tabs)TempData["Tabs"];
                if (Tab != null)
                {
                    TempData["Tabs"] = Tab;
                    return View();
                }
                else
                    return RedirectToAction("ProductList");
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult ProductList()
        {
            if (IsValidSessions())
            {
                Tabs Tab = (Tabs)TempData["Tabs"];
                if (Tab != null)
                    TempData["Tabs"] = null;
                int LanguageId = 1;
                if (Session["Language"] != null)
                {
                    LanguageId = Convert.ToInt32(Session["Language"].ToString());
                }

                var groups = _RService.ServiceGroups.Where(x => x.LanguageId == LanguageId);
                foreach (var grp in groups)
                {
                    var hasTab = _RService.ServiceTabs.Any(_ => _.ServiceGroupId == grp.Id);
                    grp.HasTab = hasTab;
                }
                ViewBag.MenuList = groups.ToList();
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddParentMenu(int? ParentId, string WhichOpen, string MenuTextHtml, string ParentMenuName, string Type, int Priority)
        {
            string fileName = string.Empty;
            string tempImagePath = string.Empty;

            // if (IsValidSessions())
            //{
            if (TempData["ImagePath"] != null)
            {
                tempImagePath = TempData["ImagePath"].ToString();
                fileName = Path.GetFileName(tempImagePath);
            }

            ServiceGroup ServiceGroup = new ServiceGroup();
            int languageId = Convert.ToInt32(Session["Language"]);
            if (Type == "add")
            {

                ServiceGroup.Name = ParentMenuName;
                ServiceGroup.ParentID = ParentId;
                ServiceGroup.Priority = Priority;
                ServiceGroup.ImagePath = fileName;
                ServiceGroup.LanguageId = languageId;
                _RService.SaveServiceGroup(ServiceGroup);

                #region AddImage

                var path = $"~/Images/Product/Categories/{ServiceGroup.Id}";
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }


                if (TempData["ImagePath"] != null)
                {
                    System.IO.File.Move(tempImagePath, Server.MapPath(Path.Combine(path, fileName)));
                    var dir = tempImagePath.Substring(0, tempImagePath.LastIndexOf("\\"));
                    Directory.Delete(dir, true);
                }

                #endregion

                ViewBag.MenuList = _RService.ServiceGroups.Where(x => x.LanguageId == languageId).ToList();
                return PartialView("_ServiceGroupTreeview");
            }
            else
            {
                ServiceGroup = _RService.DetailsServiceGroup(Convert.ToInt32(ParentId));

                ServiceGroup.Name = ParentMenuName;
                if (fileName != string.Empty)
                    ServiceGroup.ImagePath = fileName;
                _RService.SaveServiceGroup(ServiceGroup);

                #region AddImage

                var path = $"~/Images/Product/Categories/{ServiceGroup.Id}";
                if (!Directory.Exists(Server.MapPath(path)))
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                }
                else
                {
                    var files = Directory.GetFiles(Server.MapPath(path));
                    foreach (var file in files)
                    {
                        System.IO.File.Delete(file);
                    }
                }

                if (fileName != string.Empty)
                {
                    System.IO.File.Move(tempImagePath, Server.MapPath(Path.Combine(path, fileName)));
                    var dir = tempImagePath.Substring(0, tempImagePath.LastIndexOf("\\"));
                    Directory.Delete(dir, true);
                }

                #endregion

                //return Json(true);
                ViewBag.MenuList = _RService.ServiceGroups.Where(x => x.LanguageId == languageId).ToList();
                return PartialView("_ServiceGroupTreeview");
            }
            //}
            //else
            //    return JavaScript("window.location.href ='/Admin/Home/Login';");
        }
        [HttpPost]
        public ActionResult GetInfoServiceGroup(int id)
        {
            ServiceGroup ServiceGroup = _RService.DetailsServiceGroup(id);
            return Json(new { MenuNameTitle = ServiceGroup.Name }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult HasTab(int id)
        {
            int Count = _RService.ServiceTabs.Count(x => x.ServiceGroupId == id);


            return Json(new { Count = Count }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetPriority(int? id)
        {
            int LanguageId = Convert.ToInt32(Session["Language"].ToString());
            IQueryable<ServiceGroup> ServiceGroupList = (id == null) ? _RService.ServiceGroups.Where(p => p.ParentID == null && p.LanguageId == LanguageId) : _RService.ServiceGroups.Where(p => p.ParentID == id && p.LanguageId == LanguageId);
            int Priority = 1;
            if (ServiceGroupList.Any())
                Priority = ServiceGroupList.Max(p => p.Priority) + 1;
            return Json(new { Priority = Priority }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult DeleteServiceGroup(int Id)
        {
            try
            {
                 if (IsValidSessions())
                {
                    int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                    ServiceGroup ServiceGroup = _RService.DetailsServiceGroup(Id);
                    var childNumber = ServiceGroup.GrandsonsCount;
                    int Priority = ServiceGroup.Priority;
                    DeleteImagesWhenDeleteGroup(Id);


                    //update GrandsonNumbers of its ancestors
                    UpdateGrandSonNumbers(Id, false, childNumber);

                    _RService.DeleteServiceGroup(ServiceGroup);


                    ViewBag.MenuList = _RService.ServiceGroups.Where(x => x.LanguageId == LanguageId).ToList();
                    DeleteNodes(Id);
                    List<ServiceGroup> UserMenuList = ViewBag.MenuList;
                    UserMenuList =
                        UserMenuList.Where(p => p.ParentID == ServiceGroup.ParentID && p.Priority > Priority)
                            .OrderBy(x => x.Priority)
                            .ToList();
                    foreach (var item in UserMenuList)
                    {
                        item.Priority = Priority;
                        _RService.SaveServiceGroup(item);
                        Priority++;
                    }
                    return JavaScript("window.location.reload();");
                }
                 else
                    return JavaScript("window.location.href ='/Admin/Home/Login';");

            }
            catch (Exception ex)
            {
                var path = Server.MapPath("/test.txt");
                System.IO.File.WriteAllText(path, ex.ToString());
                return Json(false);
            }
            
        }
        [HttpPost]
        public ActionResult UpDownServiceGroup(int CurrentId, int PrevId)
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                List<ServiceGroup> UserMenuList = _RService.ServiceGroups.Where(p => (p.Id == CurrentId || p.Id == PrevId) && p.LanguageId == LanguageId).ToList();
                int Priority = UserMenuList[1].Priority;
                UserMenuList[1].Priority = UserMenuList[0].Priority;
                UserMenuList[0].Priority = Priority;
                foreach (var item in UserMenuList)
                {
                    _RService.SaveServiceGroup(item);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }

            return JavaScript("window.location.href ='/Admin/Home/Login';");

        }

        private void DeleteNodes(int? ParentID)
        {
            // string host = Request.Url.Host.ToLower();
            List<ServiceGroup> UserMenuList = ViewBag.MenuList;
            UserMenuList = UserMenuList.Where(p => p.ParentID == ParentID).ToList();
            int CountUserMenuList = UserMenuList.Count;
            if (CountUserMenuList > 0)
            {
                for (int i = 0; i < CountUserMenuList; i++)
                {
                    // updateSiteMap UpdateSiteMap = new updateSiteMap();
                    //UpdateSiteMap.UpdateSiteMap("http://www." + host + "/Menu/Detail/" + UserMenuList[i].Id + "/" + UserMenuList[i].Name, "delete");
                    DeleteImagesWhenDeleteGroup(UserMenuList[i].Id);
                    _RService.DeleteServiceGroup(UserMenuList[i]);
                    DeleteNodes(UserMenuList[i].Id);
                }
            }
        }
        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
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

        public bool ThumbnailCallback()
        {
            return false;
        }
        public static string StripHTML(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }
        
        private string SaveFlash(HttpPostedFileBase Picture, string Image)
        {
            string filename1 = "";
            GetFileExtension Ext = new GetFileExtension();
            if (Ext.GetExtension(Picture.FileName) == "swf")
            {
                if (Image != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Files/FlashService/" + Image));
                }
                DateTime Dt = DateTime.Now.Date;
                filename1 = DateTime.Now.Ticks + Dt.Year + Dt.Month + Dt.Day + "." + Ext.GetExtension(Picture.FileName);
                string filePath = Path.Combine(
                    Server.MapPath("~/Files/FlashService"),
                    Path.GetFileName(filename1)
                );
                Picture.SaveAs(filePath);

            }

            return filename1;
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
        //private void SelectedEditPeople(int id)
        //{
        //    var Item = _RService.ServiceTabFiles.Where(x => x.ServiceTabId == id).Select(x => x.CompanyUserId).ToList();
        //    List<Int32> Lists = Item.ConvertAll(s => Convert.ToInt32(s));
        //    TempData["SelectPeoples"] = Lists;
        //    int LanguageId = Convert.ToInt32(Session["Language"].ToString());
        //    TempData["Count"] = ECompanyUser.GetCountCompanyUserAdmin(LanguageId);
        //    ViewBag.CompanyUser = ECompanyUser.GetCompanyUserAdmin(0, 5, LanguageId).ToList();
        //}Product
        private void GetServiceTabList(int id, int Page)
        {
            int Start = (Page == 0) ? 1 : (Page - 1) * 5;
            ViewBag.TabList = EService.GetServiceTabAdmin(Start, 5, id).ToList();
            ViewBag.PageNumber = Page;
            ViewBag.ServiceGroupId = id;
            TempData["Count"] = EService.GetCountServiceTabAdmin(id);
        }
        private void DeleteImagesWhenDeleteGroup(int Id)
        {
            List<int> ServiceTabList = _RService.ServiceTabs.Where(x => x.ServiceGroupId == Id && x.TabType == 2).Select(x => x.Id).ToList();
            if (ServiceTabList != null && ServiceTabList.Count > 0)
            {
                List<ServiceTabFile> ServiceTabFiles = _RService.ServiceTabFiles.Where(x => ServiceTabList.Contains(x.ServiceTabId)).ToList();
                if (ServiceTabFiles != null && ServiceTabFiles.Count > 0)
                {
                    foreach (var item in ServiceTabFiles)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/TabGalleryService/" + item.File));
                        System.IO.File.Delete(Server.MapPath("~/Images/TabGalleryService/thum/" + item.File));
                    }
                }
            }
            List<int> ServiceTabList1 = _RService.ServiceTabs.Where(x => x.ServiceGroupId == Id && x.TabType == 1).Select(x => x.Id).ToList();
            if (ServiceTabList1 != null && ServiceTabList1.Count > 0)
            {
                List<ServiceTabFile> ServiceTabFiles = _RService.ServiceTabFiles.Where(x => ServiceTabList1.Contains(x.ServiceTabId)).ToList();
                if (ServiceTabFiles != null && ServiceTabFiles.Count > 0)
                {
                    foreach (var item in ServiceTabList1)
                    {
                        if (Directory.Exists(Server.MapPath("~/Files/ServiceGallery/" + item)))
                        {
                            DeleteDirectory(Server.MapPath("~/Files/ServiceGallery/" + item));
                        }
                    }
                }
            }
        }

        //writtenBy Azizi 95_03_12
        private string SaveImage(System.Web.HttpPostedFileBase Picture)
        {
            string filename = "";
            GetFileExtension Ext = new GetFileExtension();
            if (Ext.GetExtension(Picture.FileName) == "jpg" || Ext.GetExtension(Picture.FileName) == "png" || Ext.GetExtension(Picture.FileName) == "jpeg" || Ext.GetExtension(Picture.FileName) == "png")
            {
                filename = DateTime.Now.ToString("ddMMyyhhmmss") + Session["admin"].ToString() + "." + Ext.GetExtension(Picture.FileName);
                string filePath = Path.Combine(
                   Server.MapPath("~/Images/TabGalleryService"),
                   Path.GetFileName(filename)
               );
                Picture.SaveAs(filePath);
                WebImage img = new WebImage(Picture.InputStream);
                img.Resize(750, 464, false, false);
                img.Save(filePath);
                System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(Server.MapPath("~/Images/TabGalleryService/" + filename));
                System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = (ThumbnailCallback);
                int height = fullSizeImg.Height;
                int width = fullSizeImg.Width;
                if (height > width)
                {
                    height = (height * 190) / width;
                    width = 190;
                }
                else
                {
                    width = (width * 190) / height;
                    height = 190;
                }
                System.Drawing.Image thumbNailImg = fullSizeImg.GetThumbnailImage(width, height, dummyCallBack, IntPtr.Zero);
                thumbNailImg.Save(Server.MapPath("~/Images/TabGalleryService/thum/") + filename);
                ImageResizer.ImageBuilder.Current.Build(Server.MapPath("~/Images/TabGalleryService/thum/") + filename, Server.MapPath("~/Images/TabGalleryService/thum/") + filename, new ImageResizer.ResizeSettings(190, 190, ImageResizer.FitMode.Crop, ""));
                thumbNailImg.Dispose();
                fullSizeImg.Dispose();

            }

            return filename;
        }
        private string SaveImage(HttpPostedFileBase Picture, string alt)
        {

            try
            {
                if (Picture != null && Picture.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(Picture.FileName);
                    string fileName = (alt == string.Empty) ? "MainImage" : alt + fileExtension;
                    if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".jpeg" || fileExtension == ".gif")
                    {

                        var path = Server.MapPath("~/Images/HomePageCompanyIntroduction");
                        var files = Directory.GetFiles(path);

                        if (files.Length > 0)
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                System.IO.File.Delete(files[i]);
                            }
                        }

                        Picture.SaveAs(path + "/" + fileName);

                    }

                    return "/Images/HomePageCompanyIntroduction/" + fileName;
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
