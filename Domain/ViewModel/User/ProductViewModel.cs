using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.User
{
    public class ProductViewModel
    {

        public int? ProductId { get; set; }

        public int? GroupId { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public int Price { get; set; }

        public int Discount { get; set; }

        public bool IsExist { get; set; }
    }
}
