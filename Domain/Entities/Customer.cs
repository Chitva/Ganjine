using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
     public class Customer
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public int Title { get; set; }


        [MaxLength(200)]
        public string Url { get; set; }


        [MaxLength(200)]
        public string ImagePath { get; set; }


        [MaxLength(100)]
        public string Alt { get; set; }

        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual  Language Language { get; set; }
    }
}
