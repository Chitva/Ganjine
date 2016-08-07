using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.Admin
{
    public class validationCompanyUser
    {
  
        public int Id { get; set; }
             [Required(ErrorMessage = "وارد کردن نام الزامی است")]
        public string Name { get; set; }
           [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است")]
        public string LastName { get; set; }
           public string BirthDate { get; set; }
           public string Education { get; set; }
           public string Specialty { get; set; }
           public string Job { get; set; }
           public string ShortDes { get; set; }
           public string PlaceOfEmployment { get; set; }
        public string Role { get; set; }
          [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$")]
        public string Email { get; set; }
        public string Tell { get; set; }
        public string UserImageFile { get; set; }
        public string VisitImageFile { get; set; }
        public string WebSite { get; set; }
        public HttpPostedFileBase UserImage { get; set; }
        public HttpPostedFileBase VisitImage { get; set; }
    }
}
