using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Abstract
{
    public interface IProductOrderRepository
    {
        IQueryable<ProductOrder> ProductOrder { get; }
        ProductOrder DetailsProductOrder(int Id);
        void SaveProductOrder(ProductOrder productOrder);
        void DeleteProductOrder(ProductOrder productOrder);
    }
}
