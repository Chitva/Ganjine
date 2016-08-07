using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bank
    {
        public Int16 Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
