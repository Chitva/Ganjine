using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleAgency
    {
        public int Id { get; set; }

        [MaxLength(300)]
        public string  Address { get; set; }

        [MaxLength(64)]
        public string ManagerName { get; set; }

        [MaxLength(20)]
        public string Telephone { get; set; }

        public int CityId { get; set; }

        public virtual City  City { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
