﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.ViewModel.Admin
{
    public class viewmodelStory
    {

        public int? Id { get; set; }
        public string Title { get; set; }
        public bool IsShow { get; set; }
        public string LongDes { get; set; }
        public string ShortDes { get; set; }
        public string Tags { get; set; }
        public string metaDescription { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        
    }
}
