using System;

namespace Domain.Entities
{
    public class Upload
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public DateTime? CreationDate { get; set; }
    }
    
}

