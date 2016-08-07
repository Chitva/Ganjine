using System.Collections.Generic;

namespace Domain.Entities
{
    public class ServiceGroup
    {
        public int Id { get; set; }

        public int Priority { get; set; }
        public int? ParentID { get; set; }
        public bool IsHome { get; set; }
        
        public string ImagePath { get; set; }
        public string Alt { get; set; }
        public bool? HasTab { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public int GrandsonsCount { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
        public string metaDescription { get; set; }
        public string keywords { get; set; }

        public virtual ICollection<ServiceTab> ServiceTab { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
    
}

