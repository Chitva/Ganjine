using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Domain.ViewModel.Admin
{
   public class viewModelServiceSeo
    {

        public int id { get; set; }

        public string title { get; set; }

        public string metaDescription { get; set; }

        public string keyWords { get; set; }
    }
}
