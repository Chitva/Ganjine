using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.DTO.Template
{
    //menubar or footer
    public class Details
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public List<Details>  Childs { get; set; }
    }
}