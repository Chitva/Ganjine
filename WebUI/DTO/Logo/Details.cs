using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.DTO.Logo
{
    public class Details
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Alt { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }
    }
}