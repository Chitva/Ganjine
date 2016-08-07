using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.DTO.Customer
{
    public class Details
    {
        public int Id { get; set; }

        public Logo.Details Image { get; set; }

        public string Link { get; set; }
    }
}