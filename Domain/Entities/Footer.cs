using System;

namespace Domain.Entities
{
    public class Footer
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public string FooterLink { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int FooterColumnNameId { get; set; }
        public virtual FooterColumnName FooterColumnName { get; set; }
    }
}
