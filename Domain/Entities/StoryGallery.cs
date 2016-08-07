
namespace Domain.Entities
{
    //داستان های آموزنده 
    public class StoryGallery
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string alt { get; set; }
        public int StoryId { get; set; }
        public virtual Story Story { get; set; }

    }
    
}

