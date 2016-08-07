using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.RssReader
{
    public class Rss
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime publishedDate { get; set; }
    }
}