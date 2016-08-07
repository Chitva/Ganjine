using DataLayer.Context;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebUI.Models;

namespace WebUI.Infrastructure
{
    public class XmlSitemapResult : ActionResult
    {
        private static readonly XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        private IEnumerable<SitemapNode> _items;

        public XmlSitemapResult(IEnumerable<SitemapNode> items)
        {
            _items = items;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            string encoding = context.HttpContext.Response.ContentEncoding.WebName;
            XDocument sitemap = new XDocument(new XDeclaration("1.0", encoding, "yes"),
             new XElement(xmlns + "urlset",
                  from item in _items
                  select CreateItemElement(item)
                  )
             );

            context.HttpContext.Response.ContentType = "text/xml";
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.Write(sitemap.Declaration + sitemap.ToString());
        }

        private XElement CreateItemElement(SitemapNode item)
        {
            XElement itemElement = new XElement(xmlns + "url", new XElement(xmlns + "loc", item.Url.ToLower()));
            return itemElement;
        }
    }
}