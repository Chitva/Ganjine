using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;

namespace Domain.Validation.Admin
{
   public class ChangePasswordModel
    {
        [Required(ErrorMessage = "گذر واژه ی جاری الزامی است")]
        [System.Web.Mvc.Remote(action: "CheckPassword",
                             controller: "UserAdmin",
                             AdditionalFields = "OldPassword",
                             HttpMethod = "POST",
                             ErrorMessage = "گذر واژه جاری اشتباه است.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
      
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "گذر واژه و تکرار گذر واژه تطابق ندارد.")]
        public string ConfirmPassword { get; set; }
    }
}
