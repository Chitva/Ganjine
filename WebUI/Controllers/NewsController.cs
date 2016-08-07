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
//using WebUI.Infrastructure.Extentions.User;
//using System.Xml;
//using System.ServiceModel.Syndication;
//using System.Xml.Linq;
//using Domain.Validation.User;

//namespace WebUI.Controllers
//{
//    public class NewsController : Controller
//    {

//        IUnitOfWork _uow;
//        INewsRepository _RNews;
//        IServiceRepository _RService;
//        IWorkExperienceRepository _RWorkExperience;
//        NewsExtentions ENews;
//        List<NewsRssViewModel> newsRssList = new List<NewsRssViewModel>();

//        public NewsController(
//           IFooterRepository FooterRepository,IWorkExperienceRepository WorkExperienceRepository,
//            IServiceRepository ServiceRepository, INewsRepository NewsRepository,IUnitOfWork uow )
//        {
//            _uow = uow;
//            _RNews = NewsRepository;
//            _RService = ServiceRepository;
//            _RWorkExperience = WorkExperienceRepository;
          
//            ENews = new NewsExtentions(_RNews);
//        }
//        [HttpGet]
//        public ActionResult Index(int Page = 1)
//        {
//            InitializeData();
//            int Start = (Page - 1) * 10;
           
//            ViewBag.PageNumber = Page;
           
//            var newsEntity = _RNews.News.Where(_=>_.LanguageId == 1);
//            if(newsEntity.Any())
//            {
//                foreach (var item in newsEntity)
//                {
//                    newsRssList.Add(new NewsRssViewModel
//                    {
//                      Id = item.Id ,
//                      CreationDate = item.CreationDate,
//                      IsShow = item.IsShow ,
//                      LongDes = item.LongDes ,
//                      metaDescription = item.metaDescription ,
//                      ModifiedDate = item.ModifiedDate ,
//                      NewsGallery = item.NewsGallery ,
//                      ShortDes = item.ShortDes ,
//                      Tags = item.Tags ,
//                      Title = item.Title 
                    
//                    });
//                }
//            }
//            //RssFeeds();
//            newsRssList = newsRssList.OrderByDescending(_ => _.CreationDate).ToList();
//            TempData["Count"] = newsRssList.Count;
//            ViewBag.News = newsRssList.Skip(Start).Take(10).ToList();
           
         
//            FillingRelatedViewBags();
//            return View();
//        }
//        [HttpGet]
//        public ActionResult Detail(int Id = 1,string Title="")
//        {
//            InitializeData();
//            FillingRelatedViewBags(Id);
//            return View(_RNews.DetailsNews(Id));
//        }
//        public void InitializeData()
//        {
//            ViewBag.FooterColName = _uow.Set<FooterColumnName>().Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
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
//        private void FillingRelatedViewBags(int newsId)
//        {
//            var newsEntity = _RNews.News.FirstOrDefault(_ => _.Id == newsId);
//           if(!string.IsNullOrEmpty(newsEntity.Title))
//           {
//              ViewBag.Title = newsEntity.Title;
//           }
//           else
//           {
//               ViewBag.Title = "اخبار |  ";
//           }
           
//            if(!string.IsNullOrEmpty(newsEntity.metaDescription))
//            {
//               ViewBag.MetaDescription = newsEntity.metaDescription ;
//            }
//            else
//            {
//                ViewBag.MetaDescription =" ";
//            }

//          if(!string.IsNullOrEmpty(newsEntity.Tags))
//          {
//              ViewBag.MetaKeywords = newsEntity.Tags;
//          }
//          else
//          {
//              ViewBag.MetaKeywords = " ";
//          }
            
//        }

//        //written By Azizi 94_08_26
//        private void FillingRelatedViewBags()
//        {
//            var newslist = _RNews.News.Where(_=>_.LanguageId == 1).ToList();
//            ViewBag.Title ="اخبار سایت" ;
//            foreach (var news in newslist)
//	        {
//                ViewBag.MetaKeywords = news.Title + ",";   
//	        }
//            if (ViewBag.MetaKeywords != null)
//            {
//                if (ViewBag.MetaKeywords.EndsWith(","))
//                {
//                    ViewBag.MetaKeywords = ViewBag.MetaKeywords.Remove(ViewBag.MetaKeywords.Length - 1);
//                } 
//            }

//            if(newslist.Any())
//            ViewBag.MetaDescription = newslist.Last().metaDescription ?? " ";
//        }

//        //private void RssFeeds()
//        //{
//        //    var Urls = _rRssFeed.RssFeedURLs.Where(_=>_.IsShown);
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
//        //                newsRssList.Add(new NewsRssViewModel
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
