using System.Collections.Generic;


namespace Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public virtual ICollection<Banner> Banner { get; set; }
        public virtual ICollection<Setting> Setting { get; set; }
        public virtual ICollection<ContactInfo> ContactInfo { get; set; }
        public virtual ICollection<ContactUs> ContactUs { get; set; }
        public virtual ICollection<Question> Question { get; set; }
        public virtual ICollection<Upload> Upload { get; set; }
      
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<WorkExperienceGroup> WorkExperienceGroup { get; set; }
        public virtual ICollection<ServiceGroup> ServiceGroup { get; set; }
        public virtual ICollection<FooterColumnName> FooterColumnName { get; set; }
      
        public virtual ICollection<SaleAgency>  SaleAgency { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Story>  Story { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
    
}

