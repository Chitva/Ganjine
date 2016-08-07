using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.Admin
{
    public class validationImageGroup
    {
        [Required(ErrorMessage = "وارد کردن نام منو لزامی است")]
        public string ParentMenuName { get; set; }
    }
}
