using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NewsLetterEmails
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        [MaxLength(32)]
        public string Email { get; set; }
    }
}
