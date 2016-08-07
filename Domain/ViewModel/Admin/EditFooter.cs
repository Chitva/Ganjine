using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Admin
{
   public class EditFooter
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public string FooterLink { get; set; }
        //public string FooterText2 { get; set; }
        public int? FooterColumnNameId { get; set; }
    }
}
