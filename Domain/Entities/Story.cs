 using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsShow { get; set; }
        public string LongDes { get; set; }
        public string ShortDes { get; set; }
        public string Tags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public string metaDescription { get; set; }
        public virtual ICollection<StoryGallery> StoryGallery { get; set; }
    }
    
}

