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
    public class EFSaleAgenciesRepository  : ISaleAgenciesRepository
    {
        IUnitOfWork _uow;
        IDbSet<SaleAgency> _rSaleAgency;

        public EFSaleAgenciesRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _rSaleAgency = _uow.Set<SaleAgency>();
        }
        public IQueryable<SaleAgency> SalesAgencies
        {
            get { return _rSaleAgency; }
        }
        public void SaveSalesAgency(SaleAgency saleAgency)
        {
            if (saleAgency.Id == 0)
            {

                _rSaleAgency.Add(saleAgency);
            }
            else
            {
                _uow.Entry(saleAgency).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public SaleAgency DetailsSalesAgency(int? Id)
        {
            return _rSaleAgency.Find(Id);
        }
        public void DeleteSalesAgency(SaleAgency saleAgency)
        {
            _rSaleAgency.Remove(saleAgency);
            _uow.SaveChanges();
        }

        public IQueryable<SaleAgency> GetSpecifiedSalesAgency(int start, int end, int languageId)
        {
            return SalesAgencies.Where(_ => _ .LanguageId== languageId).OrderByDescending(_ => _.Id).Skip(start).Take(end);
        }
    }
}
