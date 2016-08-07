using DataLayer.Context;
using Domain.Entities;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class EFShoppingCartRepository : IShoppingCartRepository
    {
        IUnitOfWork _uow;
        IDbSet<ShoppingCart> _rShoppingCart;
        public IQueryable<ShoppingCart> ShoppingCarts
        {
            get { return _rShoppingCart; }
        }
        public bool SaveShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart.Id == 0)
                {
                    _rShoppingCart.Add(shoppingCart);
                }
                else
                {
                    _uow.Entry(shoppingCart).State = EntityState.Modified;
                }
                var result = _uow.SaveChanges();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ShoppingCart ShoppingCartDetails(int id)
        {
            return _rShoppingCart.Find(id);
        }
        public bool DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            _rShoppingCart.Remove(shoppingCart);
            var result = _uow.SaveChanges();

            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
