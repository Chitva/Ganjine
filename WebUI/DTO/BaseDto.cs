using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.DTO
{
    public class BaseDto
    {
        public BaseDto()
        {
            Menubar = new List<Template.Details>();
            Footers = new List<Template.Details>();
        }
        public List<Template.Details> Menubar { get; set; }

        public List<Template.Details> Footers { get; set; }

        /// <summary>
        /// these below three title will be filled for seo features
        /// </summary>
        public string PageTitle { get; set; }

        public string PageDescription { get; set; }

        public string PageMetaTags { get; set; }
    }
}