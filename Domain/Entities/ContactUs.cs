using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string BusinessAreas { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Mobile { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
        public int StatusMSG { get; set; }//1=jadid ,2==khande shode ,3=paokh dade shode
        public string  Reader { get; set; }
        public string ReadDate { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public DateTime SaveMessageDate { get; set; }
        public string OfficeAddress { get; set; }
        public string CompanyAddress { get; set; }
    }
}
