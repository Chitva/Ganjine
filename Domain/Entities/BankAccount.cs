using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public bool IsOnline { get; set; }
        public bool IsDraft { get; set; }
        public bool IsKartNumber { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string TerminalId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
       
    }
}
