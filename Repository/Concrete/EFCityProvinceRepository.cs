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
    public class EFCityProvinceRepository :ICityProvinceRepository
    {
        IUnitOfWork _uow;
        IDbSet<Province> _rProvince;
        IDbSet<City> _rCity;
        public EFCityProvinceRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _rProvince = _uow.Set<Province>();
            _rCity = _uow.Set<City>();
        }
        public IQueryable<Province> Provinces
        {
            get { return _rProvince; }
        }

        public IQueryable<City>  Cities
        {
            get { return _rCity; }
        }
        public IQueryable<City> CitiesInProvince(int provinceId)
        {
            return _rCity.Where(_ => _.ProvinceId == provinceId);
        }
    }
}
