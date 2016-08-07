using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validation.Admin
{
    public class ValidationIdAndText
    {
      public string Text { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "وارد کردن نام تب لزامی است")]
        public string TabName { get; set; }
        public string ckEditor { get; set; }
    }
}
