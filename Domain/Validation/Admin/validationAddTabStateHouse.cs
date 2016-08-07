using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.Admin
{
    public class validationAddTabStateHouse
    {
        [Required(ErrorMessage = "وارد کردن نام تب لزامی است")]
        public string TabName { get; set; }
    }
}
