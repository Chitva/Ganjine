using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Article
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
        public virtual ICollection<ArticleGallery> ArticleGallery { get; set; }
    }
    
}

