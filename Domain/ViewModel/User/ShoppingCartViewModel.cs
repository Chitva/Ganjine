using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.User
{
    public class ShoppingCartViewModel
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string  ProductName { get; set; }

        public int Count { get; set; }

        public int UnitPrice { get; set; }

        public int TotalPrice { get; set; }

        public int TotalDiscount { get; set; }

        public int TotalPayment { get; set; }

    }
}
