using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using Domain.Entities;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class EFProductOrderRepository : IProductOrderRepository
    {
        IUnitOfWork _uow;
        IDbSet<ProductOrder> _RProductOrder;
        public EFProductOrderRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RProductOrder = _uow.Set<ProductOrder>();
        }
        public IQueryable<ProductOrder> ProductOrder
        {
            get { return _RProductOrder; }
        }
        public ProductOrder DetailsProductOrder(int Id)
        {
            return _RProductOrder.Find(Id);
        }
        public void SaveProductOrder(ProductOrder ProductOrder)
        {
            if (ProductOrder.Id == 0)
            {
                _RProductOrder.Add(ProductOrder);
            }
            else
            {
                _uow.Entry(ProductOrder).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public void DeleteProductOrder(ProductOrder ProductOrder)
        {
            _RProductOrder.Remove(ProductOrder);
            _uow.SaveChanges();
        }
    }
}
