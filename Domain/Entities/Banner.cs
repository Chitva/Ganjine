using System;


namespace Domain.Entities
{
    public class Banner
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string alt { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
    
}

