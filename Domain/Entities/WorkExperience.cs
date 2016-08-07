using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    //Gallery
    public class WorkExperience
    {
        public int Id { get; set; }
        public bool IsShow { get; set; }
        public string LongDes { get; set; }
        public string ShortDes { get; set; }
        public string Tags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int WorkExperienceGroupId { get; set; }
        public virtual WorkExperienceGroup WorkExperienceGroup { get; set; }
        public string title { get; set; }
        public string metaDescription { get; set; }
        public string keyWords { get; set; }
        public virtual ICollection<WorkExperienceGallery> WorkExperiencesGallery { get; set; }
    }
    
}

