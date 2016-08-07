using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.Validation.User
{
    public class FaqValidation
    {
        [Required(ErrorMessage = "نام الزامی است .")]
        public string Name { get; set; }
        [Required(ErrorMessage = "عنوان الزامی است .")]
        public string Subject { get; set; }
        
        [Required(ErrorMessage = "پرسش الزامی است .")]

        public string Question { get; set; }
          [RegularExpression( @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$")]
        public string Email { get; set; }
    }
}
