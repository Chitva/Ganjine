using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.Admin
{
    public class validationImage
    {
        [Required(ErrorMessage = "عکس الزامی است.")]
        public HttpPostedFileBase ImageBanner { get; set; }
    }
}
