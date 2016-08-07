using DataLayer.Context;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using System.Xml.Linq;
using WebUI.Models;

namespace WebUI.Infrastructure.Utility
{
    public class SiteMapRelated
    {
        public  IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
             #region LocalVariable

            IUnitOfWork _uow = new EFDbContext();
            EFWorkExperienceRepository _RWorkExperience = new EFWorkExperienceRepository(_uow);
            EFServiceRepository _RService = new EFServiceRepository(_uow);
            EFNewsRepository _RNews = new EFNewsRepository(_uow);
         

            List<SitemapNode> items = new List<SitemapNode>();

            #endregion

            #region Static Page

            items.Add(new SitemapNode("http://www.paa-lawyers.com"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/Home"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/Home"));


            items.Add(new SitemapNode("http://www.paa-lawyers.com/AboutUs/Introduce"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/AboutUs/Staff"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/AboutUs/Contact"));

            // Uncomment this Line if we have just one AboutUs Page
            //items.Add(new SitemapItem("http://www.paa-lawyers.com/AboutUs", ChangeFrequency.Weekly, 1f));

            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/AboutUs/Introduce"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/AboutUs/Staff"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/AboutUs/Contact"));

            // Uncomment this Line if we have just one AboutUs Page
            //items.Add(new SitemapItem("http://www.paa-lawyers.com/EN/AboutUs", ChangeFrequency.Weekly, 1f));

            items.Add(new SitemapNode("http://www.paa-lawyers.com/Article"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/Article"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/News"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/News"));
            //items.Add(new SitemapNode("http://www.paa-lawyers.com/FAQ"));
            //items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/FAQ"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/Lawyer"));
            items.Add(new SitemapNode("http://www.paa-lawyers.com/EN/Lawyer"));

            #endregion

            #region workExperience

            //persian workExperience
            var persianWorkExperience = _RWorkExperience.WorkExperienceGroups.Where(_ => _.LanguageId == 1);

            foreach (var experience in persianWorkExperience)
            {
                //if it is the last node and it hasnt any child
                if (!_RWorkExperience.WorkExperienceGroups.Where(_ =>_.ParentID != null && _.ParentID == experience.Id).Any())
                {
                    string urlName = experience.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/Gallery/{0}/{1}", experience.Id, urlName)));
                }
            }


            //English WorkExperience 
            var englishWorkExperience = _RWorkExperience.WorkExperienceGroups.Where(_ => _.LanguageId == 2);

            foreach (var exp in englishWorkExperience)
            {
                //if it is the last node and it hasnt any child
                if (!_RWorkExperience.WorkExperienceGroups.Where(_ =>_.ParentID !=null && _.ParentID == exp.Id).Any())
                {
                    string urlName = exp.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/EN/WorkExperiences/{0}/{1}", exp.Id, urlName)));
                }
            }

            #endregion workExperience


            #region Services

            //persian services 
            var persianServiceGroup = _RService.ServiceGroups.Where(_ => _.LanguageId == 1);

            foreach (var service in persianServiceGroup)
            {
                if (!_RService.ServiceGroups.Where(_ =>_.ParentID != null && _.ParentID == service.Id).Any())
                {
                    string urlName = service.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/Product/{0}/{1}", service.Id, urlName)));
                }
            }

            //english Services
            var englishServiceGroup = _RService.ServiceGroups.Where(_ => _.LanguageId == 2);

            foreach (var engService in englishServiceGroup)
            {
                if (!_RService.ServiceGroups.Where(_ => _.ParentID != null &&_.ParentID == engService.Id).Any())
                {
                    string urlName = engService.Name.Replace(" ", "-");
                    items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/EN/Product/{0}/{1}", engService.Id, urlName)));
                }
            }
            #endregion

            //#region Profiles

            //var UserProfiles = new Dictionary<int, string>();

            //var perLawyersCompany = _RLawyerCompany.LawyerCompanies.Where(_ => _.LanguageId == 1);

            //foreach (var lawyer in perLawyersCompany)
            //{
            //    var urlName = lawyer.CompanyUser.Name.Replace(" ", "-");
            //    var urlFamily = lawyer.CompanyUser.LastName.Replace(" ", "-");
            //    var urlFinal = urlName + "-" + urlFamily;
            //    UserProfiles.Add(lawyer.CompanyUserId, urlFinal);
            //}


            //var perSomeLawyer = _RSomeLawyerCompany.SomeLawyerCompanies.Where(_ => _.LanguageId == 1);

            //foreach (var someLawyer in perSomeLawyer)
            //{
            //    if (!UserProfiles.ContainsKey(someLawyer.CompanyUserId))
            //    {
            //        var urlName = someLawyer.CompanyUser.Name.Replace(" ", "-");
            //        var urlFamily = someLawyer.CompanyUser.LastName.Replace(" ", "-");
            //        var urlFinal = urlName + "-" + urlFamily;
            //        UserProfiles.Add(someLawyer.CompanyUserId, urlFinal);
            //    }
            //}

            //var perStaff = _RStaffCompany.StaffCompanies.Where(_ => _.LanguageId == 1);

            //foreach (var staff in perStaff)
            //{
            //    if (!UserProfiles.ContainsKey(staff.CompanyUserId))
            //    {
            //        var urlName = staff.CompanyUser.Name.Replace(" ", "-");
            //        var urlFamily = staff.CompanyUser.LastName.Replace(" ", "-");
            //        var urlFinal = urlName + "-" + urlFamily;
            //        UserProfiles.Add(staff.CompanyUserId, urlFinal);
            //    }
            //}

            //foreach (var dic in UserProfiles)
            //{
            //    items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/Profile/Details/{0}/{1}", dic.Key, dic.Value)));
            //}

            ////EnglishUserProfiles
            //UserProfiles = new Dictionary<int, string>();

            //var engLawyersCompany = _RLawyerCompany.LawyerCompanies.Where(_ => _.LanguageId == 2);

            //foreach (var englawyer in engLawyersCompany)
            //{
            //    var urlName = englawyer.CompanyUser.Name.Replace(" ", "-");
            //    var urlFamily = englawyer.CompanyUser.LastName.Replace(" ", "-");
            //    var urlFinal = urlName + "-" + urlFamily;
            //    UserProfiles.Add(englawyer.CompanyUserId, urlFinal);
            //}


            //var engSomeLawyers = _RSomeLawyerCompany.SomeLawyerCompanies.Where(_ => _.LanguageId == 2);

            //foreach (var SomeLawyer in engSomeLawyers)
            //{
            //    if (!UserProfiles.ContainsKey(SomeLawyer.CompanyUserId))
            //    {
            //        var urlName = SomeLawyer.CompanyUser.Name.Replace(" ", "-");
            //        var urlFamily = SomeLawyer.CompanyUser.LastName.Replace(" ", "-");
            //        var urlFinal = urlName + "-" + urlFamily;
            //        UserProfiles.Add(SomeLawyer.CompanyUserId, urlFinal);
            //    }
            //}

            //var engStaff = _RStaffCompany.StaffCompanies.Where(_ => _.LanguageId == 2);

            //foreach (var engstaff in engStaff)
            //{
            //    if (!UserProfiles.ContainsKey(engstaff.CompanyUserId))
            //    {
            //        var urlName = engstaff.CompanyUser.Name.Replace(" ", "-");
            //        var urlFamily = engstaff.CompanyUser.LastName.Replace(" ", "-");
            //        var urlFinal = urlName + "-" + urlFamily;
            //        UserProfiles.Add(engstaff.CompanyUserId, urlFinal);
            //    }
            //}

            //foreach (var dic in UserProfiles)
            //{
            //    items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/EN/Profile/Details/{0}/{1}", dic.Key, dic.Value)));
            //}

            //#endregion

          

            #region News

            var persianNews = _RNews.News.Where(_ => _.LanguageId == 1);
            foreach (var perNews in persianNews)
            {
                string urlName = perNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/News/{0}/{1}", perNews.Id, urlName)));
            }


            var englishNews = _RNews.News.Where(_ => _.LanguageId == 2);

            foreach (var engNews in englishNews)
            {
                string urlName = engNews.Title.Replace(" ", "-");
                items.Add(new SitemapNode(string.Format("http://www.paa-lawyers.com/EN/News/{0}/{1}", engNews.Id, urlName)));
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
    
