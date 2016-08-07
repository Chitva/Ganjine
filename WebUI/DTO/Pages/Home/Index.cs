using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.DTO.Pages.Home
{
    public class Index : BaseDto
    {
        public Index()
        {
            Sliders = new List<Logo.Details>();
            Images = new List<Logo.Details>();
            Customers = new List<Customer.Details>();
        }

        public string Introduction { get; set; }
        public List<Logo.Details> Sliders { get; set; }

        public List<Logo.Details> Images { get; set; }

        public List<Customer.Details> Customers { get; set; }

        
    }
}