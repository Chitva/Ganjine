

namespace Domain.Entities
{
    public class ServiceTabFile
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string Alt { get; set; }
        public bool IsMainImage { get; set; }
        public int? CompanyUserId { get; set; }
        public int ServiceTabId { get; set; }
        public string Description { get; set; }
        public virtual ServiceTab ServiceTab { get; set; }

    }
    
}

