using System.Collections.Generic;


namespace Domain.Entities
{
    //Gallery
    public class WorkExperienceGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public int? ParentID { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<WorkExperience> WorkExperience { get; set; }
    }
    
}

