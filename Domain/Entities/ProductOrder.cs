using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Family { get; set; }

        [Required]
        public bool IsMale { get; set; }

        [Required]
        [MaxLength(15)]
        public string Tell { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(1000)]
        public string OrderText { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string ReadDate { get; set; }

        public string Reader { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int  ServiceGroupId { get; set; }

        public virtual ServiceGroup ServiceGroup { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }

    public enum OrderStatus
    {
        New, //جدید
        Pending, //درحال بررسی
        Checked //بررسی شده
    }
}
