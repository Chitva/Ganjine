using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Domain.Validation.Admin
{
   public class validationAdminName
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام مدیر الزامی است .")]
        public string AdminName { get; set; }
        public string Comment { get; set; }
    }
}
