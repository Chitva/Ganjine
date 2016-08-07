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
    public class EFFooterRepository : IFooterRepository
    {
        IUnitOfWork _uow;
        IDbSet<Footer> _RFooter;
        IDbSet<FooterColumnName> _RFooterColumnName;
        public EFFooterRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RFooter = _uow.Set<Footer>();
            _RFooterColumnName = _uow.Set<FooterColumnName>();
        }

        public void SaveFooter(Footer Footer)
        {
            if (Footer.Id == 0)
            {
                _RFooter.Add(Footer);
            }
            else
            {
                _uow.Entry(Footer).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public IQueryable<Footer> Footers
        {
            get { return _RFooter; }
        }
        public Footer DetailsFooter(int Id)
        {
            return _RFooter.Find(Id);
        }
        public void DeleteFooter(Footer Footer)
        {
            _RFooter.Remove(Footer);
            _uow.SaveChanges();
        }

        public IQueryable<FooterColumnName> FooterColumnNames
        {
            get { return _RFooterColumnName; }
        }
        public void SaveFooterColumnName(FooterColumnName FooterColumnName)
        {
            if (FooterColumnName.Id == 0)
            {

                _RFooterColumnName.Add(FooterColumnName);
            }
            else
            {
                _uow.Entry(FooterColumnName).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public FooterColumnName DetailsFooterColumnName(int Id)
        {
            return _RFooterColumnName.Find(Id);
        }
    }
}
