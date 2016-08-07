using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation.User
{
    public class UserLoginViewmodel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
