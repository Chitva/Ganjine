using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ArticleGallery
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ArticleId { get; set; }
        public string alt { get; set; }
        public virtual Article Article { get; set; }

    }
    
}

