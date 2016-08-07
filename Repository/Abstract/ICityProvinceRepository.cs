using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface ICityProvinceRepository
    {
        IQueryable<Province> Provinces { get; }
        IQueryable<City> Cities { get; }
        IQueryable<City> CitiesInProvince(int provinceId);
    }
}
