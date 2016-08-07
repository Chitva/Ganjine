
namespace Domain.Entities
{
    public class NewsGallery
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public string alt { get; set; }
        public int NewsId { get; set; }
        public virtual News News { get; set; }

    }
    
}

