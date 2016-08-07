using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IUserAccountRepository
    {
        IQueryable<Menu> Menus { get; }
        IQueryable<UserAccount> UserAccounts { get; }
        bool SaveUserAccount(UserAccount userAccount);
        UserAccount UserAccountDetails(int id);
        UserAccount UserAccountDetails(string email);
        bool DeleteUserAccount(UserAccount userAccount);

        IQueryable<UserAddress> UserAddresses { get; }
        bool SaveUserAddress(UserAddress userAddress);
        IQueryable<UserAddress> UserAddressesList(int userId);
        bool DeleteUserAddress(UserAddress userAddress);
        UserAddress UserAddressDetails(int id);
    }
}
