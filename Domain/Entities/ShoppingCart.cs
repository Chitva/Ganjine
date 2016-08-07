using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ServiceTab Product { get; set; }

        public int UnitCount { get; set; }

        public DateTime AddedDate { get; set; }
       
        public int UserId { get; set; }

        public virtual UserAccount User { get; set; }
    }
}
