using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class UserAddress
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; } 
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [MaxLength(500)]
        public string LeftAddress { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserAccount UserAccount { get; set; }
      
    }
}
