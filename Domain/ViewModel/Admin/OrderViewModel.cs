using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Admin
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public bool IsMale { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string OrderText { get; set; }
        public DateTime OrderDate { get; set; }
        public string ReadDate { get; set; }
        public string Reader { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int ServiceGroupId { get; set; }
        public string ServiceGroupName { get; set; }
        public int LanguageId { get; set; }

       
    }
}
