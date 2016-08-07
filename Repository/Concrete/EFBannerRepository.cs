
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;

namespace RepositoryLayer.Concrete
{
    public class EFBannerRepository : IBannerRepository
    {
        IUnitOfWork _uow;
        IDbSet<Banner> _RBanner;
        public EFBannerRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RBanner = _uow.Set<Banner>();
        }
        public IQueryable<Banner> Banners
        {
            get { return _RBanner; }
        }
        public void SaveBanner(Banner Banner)
        {
            if (Banner.Id == 0)
            {

                _RBanner.Add(Banner);
            }
            else
            {
                _uow.Entry(Banner).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public Banner DetailsBanner(int Id)
        {
            return _RBanner.Find(Id);
        }
        public void DeleteBanner(Banner Banner)
        {
            _RBanner.Remove(Banner);
            _uow.SaveChanges();
        }
    }
}
