using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.User
{
    public class ContactUsValidation
    {
        [Required(ErrorMessage = "وارد کردن عنوان لزامی است")]
        public string Subject { get; set; }
        //[Required(ErrorMessage = "وارد کردن نام لزامی است")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "وارد کردن نام خانوادگی لزامی است")]
        public string Family { get; set; }
        [Required(ErrorMessage = "وارد کردن پیام لزامی است")]

        public string Message { get; set; }
          [RegularExpression( @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$")]
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Mobile { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string OfficeAddress { get; set; }

        public string BusinessAreas { get; set; }
    }
}
