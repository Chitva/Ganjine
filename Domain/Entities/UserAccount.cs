using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserAccount
    {
        public int Id { get; set; }
        public bool? IsMale { get; set; }

        public bool IsActive { get; set; }

        [Index(IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(300)]
        [Required]
        [DataType(DataType.Password)]
        public string EncrypedPass { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(20)]
        public string CellPhone { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? ProvinceId { get; set; }

        public virtual Province Province { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public DateTime CreateDate { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        //public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts  { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
