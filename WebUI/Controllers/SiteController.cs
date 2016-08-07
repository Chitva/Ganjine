using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using WebUI.Models;
using WebUI.Infrastructure;
using DataLayer.Context;
using Repository.Concrete;
using RepositoryLayer.Concrete;
using RepositoryLayer.Abstract;


namespace WebUI.Controllers
{
    [AllowAnonymous]
    
    public class SiteController : Controller
    {
        //
        // GET: /Site/

        
        public FileContentResult RobotsTxt()
        {
            var content = new StringBuilder("User-agent: *" + Environment.NewLine);

            if (string.Equals(ConfigurationManager.AppSettings["SiteStatus"], "live", StringComparison.InvariantCultureIgnoreCase))
            {

                content.Append("Disallow: ").Append("/Admin" + Environment.NewLine);
                content.Append("Sitemap :").Append(" http://www.TandisTalaei.com/sitemap.xml");

                ////it tells the crawlers to igonore site map for crawling
                //content.Append("Sitemap: ").Append("https://").Append(ConfigurationManager.AppSettings["HostName"]).Append("/sitemap.xml" + Environment.NewLine);
                //#region developing
                //content.Append("Disallow: ").Append("/" + Environment.NewLine);
                //#endregion
            }
            else
            {
                // disallow indexing for test and dev servers
                content.Append("Disallow: /" + Environment.NewLine);
            }


            return File(
                    Encoding.UTF8.GetBytes(content.ToString()),
                    "text/plain");
        }


        public ActionResult SitemapXML()
        {

            var sitemapNodes = GetSitemapNodes();


            return new XmlSitemapResult(sitemapNodes);
        }
        

        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            #region LocalVariable

            IUnitOfWork _uow = new EFDbContext();
            //work experience as galleries
            EFWorkExperienceRepository _RWorkExperience = new EFWorkExperienceRepository(_uow);
            EFServiceRepository _RService = new EFServiceRepository(_uow);
            EFNewsRepository _RNews = new EFNewsRepository(_uow);
            EFArticleRepository _rArticle = new EFArticleRepository(_uow);
            EFStoryRepository _rStory = new EFStoryRepository(_uow);
          
          

            List<SitemapNode> items = new List<SitemapNode>();

            #endregion

            #region Static Page

            items.Add(new SitemapNode("http://www.TandisTalaei.com"));
            items.Add(new SitemapNode("http://www.TandisTalaei.com/Home"));
            items.Add(new SitemapNode("http://www.TandisTalaei.com/EN/Home"));


            items.Add(new SitemapNode("http://www.TandisTalaei.com/AboutUs"));
            
            items.Add(new SitemapNode("http://www.TandisTalaei.com/EN/AboutUs"));

            items.Add(new SitemapNode("http://www.TandisTalaei.com/News"));
            items.Add(new SitemapNode("http://www.TandisTalaei.com/EN/News"));

            //items.Add(new SitemapNode("http://www.TandisTalaei.com/FAQ"));
            //items.Add(new SitemapNode("http://www.TandisTalaei.com/EN/FAQ"));
           

            #endregion

            #region Gallery

            //persian workExperience
            var persianWorkExperience = _RWorkExperience.WorkExperienceGroups.Where(_ => _.LanguageId == 1);

            foreach (var experience in persianWorkExperience)
            {
                //if it is the last node and it hasnt any child
                if (!_RWorkExperience.WorkExperienceGroups.Any(_ => _.ParentID != null && _.ParentID == experience.Id))
                {
                    string urlName = experience.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/Gallery/{0}/{1}", experience.Id, urlName)));
                }
            }


            //English WorkExperience 
            var englishWorkExperience = _RWorkExperience.WorkExperienceGroups.Where(_ => _.LanguageId == 2);

            foreach (var exp in englishWorkExperience)
            {
                //if it is the last node and it hasnt any child
                if (!_RWorkExperience.WorkExperienceGroups.Any(_ => _.ParentID != null && _.ParentID == exp.Id))
                {
                    string urlName = exp.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/EN/Gallery/{0}/{1}", exp.Id, urlName)));
                }
            }

            #endregion Gallery

            #region Product

            //persian services 
            var persianServiceGroup = _RService.ServiceGroups.Where(_ => _.LanguageId == 1);

            foreach (var service in persianServiceGroup)
            {
                if (!_RService.ServiceGroups.Any(_ => _.ParentID != null && _.ParentID == service.Id))
                {
                    string urlName = service.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/Product/{0}/{1}", service.Id, urlName)));
                }
            }

            //english Services
            var englishServiceGroup = _RService.ServiceGroups.Where(_ => _.LanguageId == 2);

            foreach (var engService in englishServiceGroup)
            {
                if (!_RService.ServiceGroups.Any(_ => _.ParentID != null && _.ParentID == engService.Id))
                {
                    string urlName = engService.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/EN/Product/{0}/{1}", engService.Id, urlName)));
                }
            }
            #endregion

            #region News

            var persianNews = _RNews.News.Where(_ => _.LanguageId == 1);
            foreach (var perNews in persianNews)
            {
                string urlName = perNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/News/{0}/{1}", perNews.Id, urlName)));
            }


            var englishNews = _RNews.News.Where(_ => _.LanguageId == 2);

            foreach (var engNews in englishNews)
            {
                string urlName = engNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/EN/News/{0}/{1}", engNews.Id, urlName)));
            }

            #endregion

            #region Article

            var persianArticle = _rArticle.Article.Where(_ => _.LanguageId == 1);
            foreach (var perNews in persianArticle)
            {
                string urlName = perNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/Article/{0}/{1}", perNews.Id, urlName)));
            }


            var englishArticle = _rArticle.Article.Where(_ => _.LanguageId == 2);

            foreach (var engNews in englishArticle)
            {
                string urlName = engNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/EN/Article/{0}/{1}", engNews.Id, urlName)));
            }

            #endregion

            #region Story

            var persianStory = _rArticle.Article.Where(_ => _.LanguageId == 1);
            foreach (var perNews in persianStory)
            {
                string urlName = perNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/Story/{0}/{1}", perNews.Id, urlName)));
            }


            var englishStory = _rArticle.Article.Where(_ => _.LanguageId == 2);

            foreach (var engNews in englishStory)
            {
                string urlName = engNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.TandisTalaei.com/EN/Story/{0}/{1}", engNews.Id, urlName)));
            }

            #endregion

            return items;

        }

        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }

    }
}
