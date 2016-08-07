using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.User
{
    public class ProductLinksViewModel
    {
        public int ProductGroupId { get; set; }

        public string ProductName { get; set; }

        public string ImagePath { get; set; }

        public string Alt { get; set; }

        public int? Price { get; set; }

        public bool IsExist { get; set; }

        public string ShortDescription { get; set; }

        public int? Discount { get; set; }
    }
}
