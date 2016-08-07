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

namespace RepositoryLayer.Concrete
{
    public class EFContactRepository : IContactRepository
    {
        IUnitOfWork _uow;
        IDbSet<ContactUs> _RContactUs;
        public EFContactRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RContactUs = _uow.Set<ContactUs>();
        }

        public IQueryable<ContactUs> ContactUs
        {
            get { return _RContactUs; }
        }
        public ContactUs DetailsContactUs(int Id)
        {
            return _RContactUs.Find(Id);
        }
        public void SaveContactUs(ContactUs ContactUs)
        {
            if (ContactUs.Id == 0)
            {
                _RContactUs.Add(ContactUs);
            }
            else
            {
                _uow.Entry(ContactUs).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public void DeleteContactUs(ContactUs ContactUs)
        {
            _RContactUs.Remove(ContactUs);
            _uow.SaveChanges();
        }
    }
}
