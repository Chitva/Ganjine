using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.Admin
{
    public class validationEditGalleryTab
    {
        public int id { get; set; }
        public HttpPostedFileBase Image { get; set; }
        [Required(ErrorMessage = "وارد کردن نام تب لزامی است")]
        public string TabName { get; set; }
    }
}
