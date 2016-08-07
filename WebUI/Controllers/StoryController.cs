using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Repository.Abstract;
using WebUI.Infrastructure.Extentions.User;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using Domain.Validation.User;
using WebUI.Infrastructure.Extentions.Admin;
using Domain.ViewModel.User;

namespace WebUI.Controllers
{
    public class StoryController : Controller
    {
        IUnitOfWork _uow;
        IStoryRepository _RStory;
        IServiceRepository _RService;
        IWorkExperienceRepository _RWorkExperience;
        IFooterRepository _RFooter;
        //IRssFeedRepository _rRssFeed;
        StoryExtentions EStory;
        IUserAccountRepository _rUserRepository;
        List<StoryViewModel> StoryRssList = new List<StoryViewModel>();

        public StoryController(
           IFooterRepository FooterRepository, IWorkExperienceRepository WorkExperienceRepository,
            IServiceRepository ServiceRepository, IStoryRepository StoryRepository, IUnitOfWork uow, IUserAccountRepository rUser)
        {
            _uow = uow;
            _RStory = StoryRepository;
            _RService = ServiceRepository;
            _RWorkExperience = WorkExperienceRepository;
            _RFooter = FooterRepository;
            _rUserRepository = rUser;

            EStory = new StoryExtentions(_RStory, _rUserRepository);
        }
        [HttpGet]
        public ActionResult Index(int Page = 1)
        {
            InitializeData();
            int Start = (Page - 1) * 10;

            ViewBag.PageNumber = Page;

            var StoryEntity = _RStory.Stories.Where(_=>_.LanguageId == 1);
            if (StoryEntity.Any())
            {
                foreach (var item in StoryEntity)
                {
                    StoryRssList.Add(new StoryViewModel
                    {
                        Id = item.Id,
                        CreationDate = item.CreationDate,
                        IsShow = item.IsShow,
                        LongDes = item.LongDes,
                        metaDescription = item.metaDescription,
                        ModifiedDate = item.ModifiedDate,
                        StoryGallery = item.StoryGallery,
                        ShortDes = item.ShortDes,
                        Tags = item.Tags,
                        Title = item.Title
                    });
                }
            }

            StoryRssList = StoryRssList.OrderByDescending(_ => _.CreationDate).ToList();
            TempData["Count"] = StoryRssList.Count;
            ViewBag.Story = StoryRssList.Skip(Start).Take(10).ToList();


            FillingRelatedViewBags();
            return View();
        }
        [HttpGet]
        public ActionResult Detail(int Id = 1, string Title = "")
        {
            InitializeData();
            FillingRelatedViewBags(Id);
            return View(_RStory.DetailsStory(Id));
        }
        public void InitializeData()
        {
            ViewBag.FooterColName = _RFooter.FooterColumnNames.Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
            var x1 = _RService.ServiceGroups.Where(x => x.LanguageId == 1);
            if (x1.Any())
                ViewBag.ServiceGroup = x1.ToList();

            var x2 = _RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == 1);
            if (x2.Any())
                ViewBag.WorkExGroup = x2.ToList();
            

            var x7 = _uow.Set<ServiceTab>();
            if (x7.Any())
                ViewBag.ServiceTabs = x7.ToList();
        }

        //written By Azizi 94_08_26
        private void FillingRelatedViewBags(int StoryId)
        {
            var StoryEntity = _RStory.Stories.FirstOrDefault(_ => _.Id == StoryId);
            if (!string.IsNullOrEmpty(StoryEntity.Title))
            {
                ViewBag.Title = StoryEntity.Title;
            }
            else
            {
                ViewBag.Title = "اخبار |  ";
            }

            if (!string.IsNullOrEmpty(StoryEntity.metaDescription))
            {
                ViewBag.MetaDescription = StoryEntity.metaDescription;
            }
            else
            {
                ViewBag.MetaDescription = " ";
            }

            if (!string.IsNullOrEmpty(StoryEntity.Tags))
            {
                ViewBag.MetaKeywords = StoryEntity.Tags;
            }
            else
            {
                ViewBag.MetaKeywords = " ";
            }

        }

        //written By Azizi 94_08_26
        private void FillingRelatedViewBags()
        {
            var Storylist = _RStory.Stories.Where(_ => _.LanguageId == 1).ToList();
            ViewBag.Title = "حکایات سایت";
            foreach (var Story in Storylist)
            {
                ViewBag.MetaKeywords = Story.Title + ",";
            }
            if (ViewBag.MetaKeywords != null)
            {
                if (ViewBag.MetaKeywords.EndsWith(","))
                {
                    ViewBag.MetaKeywords = ViewBag.MetaKeywords.Remove(ViewBag.MetaKeywords.Length - 1);
                }
            }

            if (Storylist.Any())
                ViewBag.MetaDescription = Storylist.Last().metaDescription ?? " ";
        }
        public static string StripHTML(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }


    }
}
