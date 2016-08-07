using Domain.Entities;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using Repository.Abstract;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace WebUI.Infrastructure.Extentions.Admin
{
    public class BannerExtentions
    {
        IBannerRepository _RBanner;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
        public BannerExtentions(IBannerRepository BannerRepository, IUserAccountRepository DefineUserRepository)
        {
            _RBanner=BannerRepository;
            _RDefineUser = DefineUserRepository;
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }
        public int GetCountBanner(int UserCode,int LanguageId)
        {
            return _RBanner.Banners.Where(x=>x.LanguageId==LanguageId).Count() ;
        }
        public IQueryable<Banner> GetBanner(int Start, int End,int LanguageId)
        {
            return _RBanner.Banners.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End) ;
        }
        public void DeleteBanner(Banner Banner)
        {
            string FileName = Banner.Image;
            if (FileName != "default.ico")
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Images/Banner/Large/" + FileName));
                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Images/Banner/Small/" + FileName));
            }
            _RBanner.DeleteBanner(Banner);
        }
    }
}