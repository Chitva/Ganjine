using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.Admin
{
    public class validationContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string BusinessArea { get; set; }
        public string  CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string OfficeAddress { get; set; }
        public int StatusMSG { get; set; }
          [Required(ErrorMessage = "وارد کردن نام الزامی است .")]
        public string Reader { get; set; }
          [Required(ErrorMessage = "وارد کردن تاریخ الزامی است .")]
        public string ReadDate { get; set; }
          public DateTime SaveMessageDate { get; set; }
    }
}
