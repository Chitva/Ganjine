using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebUI.Models
{
   
    public class SitemapItem : ISitemapItem
    {
        public SitemapItem(string url)
        {
            this.url = url;
        }
 
        private string url;
        
 
        public string Url
        {
            get { return this.url; }
        }
 
        //public DateTime? LastModified
        //{
        //    get { return this.lastModified; }
        //}
 
        //public ChangeFrequency? ChangeFrequency
        //{
        //    get { return this.changeFrequency; }
        //}
 
        //public float? Priority
        //{
        //    get { return this.priority; }
        //}
    }


  
}