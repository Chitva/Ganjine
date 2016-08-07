using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        public string CityName { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<SaleAgency>  SalesAgencies { get; set; }
        
    }
}
