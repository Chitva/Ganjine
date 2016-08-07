

namespace Domain.Entities
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string FormTopText { get; set; }
        public string Address { get; set; }
        public string Map { get; set; }
        public string Tell { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
    
}

