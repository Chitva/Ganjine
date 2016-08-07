using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Domain.Entities;

namespace Domain.Validation.Admin
{
    public class OrderValidation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است .")]
        public string Family { get; set; }

        [Required(ErrorMessage = "واردکردن جنسیت الزامیست")]
        public bool IsMale { get; set; }

        [Required(ErrorMessage = "واردکردن شماره تماس الزامیست")]
        public string Tell { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage = "لطفاایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string OrderText { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int ServiceGroupId { get; set; }
        public string ServiceGroupName { get; set; }
        public int LanguageId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
