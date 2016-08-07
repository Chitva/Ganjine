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
    public class EFUserAccountRepository : IUserAccountRepository
    {
        IUnitOfWork _uow;
        IDbSet<Menu> _RMenu;
        IDbSet<UserAccount> _rAccount;
        IDbSet<UserAddress> _rAddress;

        public IQueryable<Menu> Menus
        {
            get { return _RMenu; }
        }
        public EFUserAccountRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _rAccount = _uow.Set<UserAccount>();
            _rAddress = _uow.Set<UserAddress>();
        }

        public IQueryable<UserAccount> UserAccounts
        {
            get { return _rAccount; }
        }

        public bool SaveUserAccount(UserAccount userAccount)
        {
            try
            {
                if (userAccount.Id == 0)
                {
                    _rAccount.Add(userAccount);
                }
                else
                {
                    _uow.Entry(userAccount).State = EntityState.Modified;
                }
                var result = _uow.SaveChanges();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public UserAccount UserAccountDetails(int id)
        {
            return _rAccount.Find(id);
        }

        public UserAccount UserAccountDetails(string email)
        {
            return _rAccount.FirstOrDefault(_ => _.Email == email);
        }
        public bool DeleteUserAccount(UserAccount userAccount)
        {
            _rAccount.Remove(userAccount);
            var result = _uow.SaveChanges();

            if (result > 0)
                return true;
            else
                return false;
        }

        public IQueryable<UserAddress> UserAddresses
        {
            get { return _rAddress; }
        }
        public bool SaveUserAddress(UserAddress userAddress)
        {
            try
            {
                if (userAddress.Id == 0)
                {
                    _rAddress.Add(userAddress);
                }
                else
                {
                    _uow.Entry(userAddress).State = EntityState.Modified;
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
        public IQueryable<UserAddress> UserAddressesList(int userId)
        {
            return _rAddress.Where(_ => _.UserId == userId);
        }
        public bool DeleteUserAddress(UserAddress userAddress)
        {
            _rAddress.Remove(userAddress);
            var result = _uow.SaveChanges();

            if (result > 0)
                return true;
            else
                return false;
        }
        public UserAddress UserAddressDetails(int id)
        {
            return _rAddress.Find(id);
        }
    }
}
