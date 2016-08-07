using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.ViewModel.Admin
{
    public class viewmodelFaq
    {
        public bool IsShow { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Tags { get; set; }
        public int? Id { get; set; }
    }
}
