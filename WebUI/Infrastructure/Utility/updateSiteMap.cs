using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;
using System.IO;
using System.Web.Hosting;
namespace WebUI.Infrastructure.Utility
{
    public class updateSiteMap
    {
        public void UpdateSiteMap(string Addr,string NewOpr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            bool IsExistNode = false;
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/sitemap.xml"));
            XmlElement xmlElement = xmlDoc.DocumentElement;
            if ( xmlElement.ChildNodes != null)
            {
                    foreach (XmlElement myElement in xmlDoc.DocumentElement)
                    {
                        if (myElement.ChildNodes[0].InnerText == Addr)
                        {
                            myElement.ChildNodes[1].InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                            if (NewOpr != null)
                            {
                                if (NewOpr != "delete")
                                    myElement.ChildNodes[0].InnerText = NewOpr;
                                else
                                    myElement.ParentNode.RemoveChild(myElement);
                            }
                            IsExistNode = true;
                            break;
                        }
                    }
            }
            if (!IsExistNode)
            {
                string ns="http://www.sitemaps.org/schemas/sitemap/0.9";
                XmlNode url = xmlDoc.CreateNode(XmlNodeType.Element, "url",ns );
                XmlNode loc = xmlDoc.CreateNode(XmlNodeType.Element, "loc", ns);
                XmlNode lastmod = xmlDoc.CreateNode(XmlNodeType.Element, "lastmod", ns);
                XmlNode changefreq = xmlDoc.CreateNode(XmlNodeType.Element, "changefreq", ns);
                XmlNode priority = xmlDoc.CreateNode(XmlNodeType.Element, "priority", ns);
                loc.InnerText = Addr;
                lastmod.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                changefreq.InnerText = "always";
                priority.InnerText = "1";
                url.AppendChild(loc);
                url.AppendChild(lastmod);
                url.AppendChild(changefreq);
                url.AppendChild(priority);
                xmlDoc.DocumentElement.AppendChild(url);
            }
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/sitemap.xml"));
        }
    }
}