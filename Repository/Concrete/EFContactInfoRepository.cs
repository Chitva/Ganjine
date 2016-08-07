using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System.Security.Cryptography;
using Domain.ViewModel.Admin;
namespace RepositoryLayer.Concrete
{
    public class EFContactInfoRepository : IContactInfoRepository
    {
        IUnitOfWork _uow;
        
        IDbSet<ContactInfo> _RContactInfo;
        public EFContactInfoRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RContactInfo=_uow.Set<ContactInfo>();
        }
        public IQueryable<ContactInfo> ContactInfos
        {
            get { return _RContactInfo; }
        }
        public void SaveContactInfo(ContactInfo ContactInfo)
        {
            if (ContactInfo.Id == 0)
            {

                _RContactInfo.Add(ContactInfo);
            }
            else
            {
                _uow.Entry(ContactInfo).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public ContactInfo DetailsContactInfo(int Id)
        {
            return _RContactInfo.Find(Id);
        }
        public void DeleteContactInfo(ContactInfo ContactInfo)
        {
            _RContactInfo.Remove(ContactInfo);
            _uow.SaveChanges();
        }
    }
}
