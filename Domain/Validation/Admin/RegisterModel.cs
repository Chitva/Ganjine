using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Domain.Validation.Admin
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string TellNumber { get; set; }
        
      
        public string UserName { get; set; }
   
        public string Email { get; set; }
      
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
    }
}
