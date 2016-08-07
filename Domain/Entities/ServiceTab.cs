using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ServiceTab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TabType { get; set; }
        public string TabTypeText { get; set; }
        public string ShortText { get; set; }
        public string Title { get; set; }

        [MaxLength(100)]
        public string ProductUnit { get; set; }
        public int UnitWeight { get; set; }
        public bool IsShow { get; set; }
        public string Tags { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public bool IsExist { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ServiceGroupId { get; set; }
        public virtual ServiceGroup ServiceGroup { get; set; }
        public virtual ICollection<ServiceTabFile> ServiceTabFile { get; set; }
        public virtual ICollection<InvoiceDetail>  InvoiceDetails { get; set; }
    }
    
}

