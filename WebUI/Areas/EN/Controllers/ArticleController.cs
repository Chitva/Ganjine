//using DataLayer.Context;
//using Domain.Entities;
//using Domain.ViewModel.User;
//using Repository.Abstract;
//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using WebUI.Infrastructure.Extentions.Admin;
//using WebUI.Infrastructure.Extentions.User;

//namespace WebUI.Areas.EN.Controllers
//{
//    public class ArticleController : Controller
//    {
//        IUnitOfWork _uow;
//        IArticleRepository _RArticle;
//        IServiceRepository _RService;
//        IWorkExperienceRepository _RWorkExperience;
//        IFooterRepository _RFooter;
//        //IRssFeedRepository _rRssFeed;
//        ArticleExtentions EArticle;
//        IUserAccountRepository _rUserRepository;
//        List<ArticleViewModel> ArticleRssList = new List<ArticleViewModel>();

//        public ArticleController(
//           IFooterRepository FooterRepository, IWorkExperienceRepository WorkExperienceRepository,
//            IServiceRepository ServiceRepository, IArticleRepository ArticleRepository, IUnitOfWork uow ,IUserAccountRepository rUser)
//        {
//            _uow = uow;
//            _RArticle = ArticleRepository;
//            _RService = ServiceRepository;
//            _RWorkExperience = WorkExperienceRepository;
//            _RFooter = FooterRepository;
//            _rUserRepository = rUser;
          
//            EArticle = new ArticleExtentions(_RArticle ,_rUserRepository);
//        }
//        [HttpGet]
//        public ActionResult Index(int Page = 1)
//        {
//            InitializeData();
//            int Start = (Page - 1) * 10;

//            ViewBag.PageNumber = Page;

//            var ArticleEntity = _RArticle.Article.Where(_=>_.LanguageId == 2);
//            if (ArticleEntity.Any())
//            {
//                foreach (var item in ArticleEntity)
//                {
//                    ArticleRssList.Add(new ArticleViewModel
//                    {
//                        Id = item.Id,
//                        CreationDate = item.CreationDate,
//                        IsShow = item.IsShow,
//                        LongDes = item.LongDes,
//                        metaDescription = item.metaDescription,
//                        ModifiedDate = item.ModifiedDate,
//                        ArticleGallery = item.ArticleGallery,
//                        ShortDes = item.ShortDes,
//                        Tags = item.Tags,
//                        Title = item.Title
//                    });
//                }
//            }
           
//            ArticleRssList = ArticleRssList.OrderByDescending(_ => _.CreationDate).ToList();
//            TempData["Count"] = ArticleRssList.Count;
//            ViewBag.Article = ArticleRssList.Skip(Start).Take(10).ToList();


//            FillingRelatedViewBags();
//            return View();
//        }
//        [HttpGet]
//        public ActionResult Detail(int Id = 1, string Title = "")
//        {
//            InitializeData();
//            FillingRelatedViewBags(Id);
//            return View(_RArticle.DetailsArticle(Id));
//        }
//        public void InitializeData()
//        {
//            ViewBag.FooterColName = _RFooter.FooterColumnNames.Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
//            var x1 = _RService.ServiceGroups.Where(x => x.LanguageId == 1);
//            if (x1.Any())
//                ViewBag.ServiceGroup = x1.ToList();

//            var x2 = _RWorkExperience.WorkExperienceGroups.Where(x => x.LanguageId == 1);
//            if (x2.Any())
//                ViewBag.WorkExGroup = x2.ToList();

//            var x7 = _uow.Set<ServiceTab>();
//            if (x7.Any())
//                ViewBag.ServiceTabs = x7.ToList();
//        }

//        //written By Azizi 94_08_26
//        private void FillingRelatedViewBags(int ArticleId)
//        {
//            var ArticleEntity = _RArticle.Article.FirstOrDefault(_ => _.Id == ArticleId);
//            if (!string.IsNullOrEmpty(ArticleEntity.Title))
//            {
//                ViewBag.Title = ArticleEntity.Title;
//            }
//            else
//            {
//                ViewBag.Title = "اخبار |  ";
//            }

//            if (!string.IsNullOrEmpty(ArticleEntity.metaDescription))
//            {
//                ViewBag.MetaDescription = ArticleEntity.metaDescription;
//            }
//            else
//            {
//                ViewBag.MetaDescription = " ";
//            }

//            if (!string.IsNullOrEmpty(ArticleEntity.Tags))
//            {
//                ViewBag.MetaKeywords = ArticleEntity.Tags;
//            }
//            else
//            {
//                ViewBag.MetaKeywords = " ";
//            }

//        }

//        //written By Azizi 94_08_26
//        private void FillingRelatedViewBags()
//        {
//            var Articlelist = _RArticle.Article.Where(_ => _.LanguageId == 1).ToList();
//            ViewBag.Title = "اخبار سایت";
//            foreach (var Article in Articlelist)
//            {
//                ViewBag.MetaKeywords = Article.Title + ",";
//            }
//            if (ViewBag.MetaKeywords != null)
//            {
//                if (ViewBag.MetaKeywords.EndsWith(","))
//                {
//                    ViewBag.MetaKeywords = ViewBag.MetaKeywords.Remove(ViewBag.MetaKeywords.Length - 1);
//                }
//            }

//            if (Articlelist.Any())
//                ViewBag.MetaDescription = Articlelist.Last().metaDescription ?? " ";
//        }

//        //private void RssFeeds()
//        //{
//        //    var Urls = _rRssFeed.RssFeedURLs.Where(_ => _.IsShown);
//        //    try
//        //    {
//        //        foreach (var url in Urls)
//        //        {
//        //            var xml = XmlReader.Create(url.FeedURL);
//        //            var feeds = SyndicationFeed.Load(xml);
//        //            xml.Close();

//        //            foreach (SyndicationItem feed in feeds.Items)
//        //            {
//        //                if (feed.Links[0].Uri == null)
//        //                    continue;

//        //                string content = "";
//        //                foreach (SyndicationElementExtension ext in feed.ElementExtensions)
//        //                {
//        //                    if (ext.GetObject<XElement>().Name.LocalName == "encoded")
//        //                    {
//        //                        content = ext.GetObject<XElement>().Value;
//        //                        break;
//        //                    }

//        //                }
//        //                var shortDesc = content.Length >= 500 ? StripHTML(content).Substring(0, 500) : content;
//        //                ArticleRssList.Add(new ArticleRssViewModel
//        //                {
//        //                    CreationDate = feed.PublishDate.Date,
//        //                    ShortDes = shortDesc,
//        //                    Title = feed.Title.Text,
//        //                    LongDes = StripHTML(content),
//        //                    Id = 0,
//        //                    BaseUri = feed.Links[0].Uri.ToString()
//        //                });
//        //            }
//        //        }
//        //    }
//        //    catch (Exception)
//        //    {
//        //        return;
//        //    }

//        //}

//        public static string StripHTML(string HTMLText, bool decode = true)
//        {
//            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
//            var stripped = reg.Replace(HTMLText, "");
//            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
//        }
//    }
//}
