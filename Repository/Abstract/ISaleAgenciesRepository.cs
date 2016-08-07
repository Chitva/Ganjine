using Domain.Entities;
using System.Linq;


namespace Repository.Abstract
{
    public interface ISaleAgenciesRepository
    {
        IQueryable<SaleAgency> SalesAgencies { get; }
        void SaveSalesAgency(SaleAgency salesAgency);
        SaleAgency DetailsSalesAgency(int? Id);
        void DeleteSalesAgency(SaleAgency salesAgency);
        IQueryable<SaleAgency> GetSpecifiedSalesAgency(int start, int end, int languageId);
    }
}
