namespace Domain.Entities
{
    //Gallery
    public class WorkExperienceGallery
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string alt { get; set; }
        public int WorkExperiencesId { get; set; }
        public virtual WorkExperience WorkExperience { get; set; }

    }
    
}

