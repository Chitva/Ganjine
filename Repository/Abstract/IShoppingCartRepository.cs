using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IShoppingCartRepository
    {
        IQueryable<ShoppingCart> ShoppingCarts { get; }
        bool SaveShoppingCart(ShoppingCart shoppingCart);
        ShoppingCart ShoppingCartDetails(int id);
        bool DeleteShoppingCart(ShoppingCart shoppingCart);
    }
}
