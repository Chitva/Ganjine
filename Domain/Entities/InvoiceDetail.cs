using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InvoiceDetail
    {
        public int Id { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        public int InvoiceId { get; set; }
       
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual ServiceTab Product { get; set; }

        public int Price { get; set; }

        public int ProductDiscount { get; set; }

    }
}
