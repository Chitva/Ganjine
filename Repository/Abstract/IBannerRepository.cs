using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModel.Admin;

namespace RepositoryLayer.Abstract
{
    public interface IBannerRepository
    {
        IQueryable<Banner> Banners { get; }
        void SaveBanner(Banner Banner);
        Banner DetailsBanner(int Id);
        void DeleteBanner(Banner Banner);
    }
}

