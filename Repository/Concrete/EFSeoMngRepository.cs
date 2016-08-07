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
using System.Security.Cryptography;
using RepositoryLayer.Abstract;

namespace RepositoryLayer.Concrete
{


    //written By Azizi 94_08_25
    public  class EFSeoMngRepository : ISeoMngRepository
    {
        IUnitOfWork _uow;
        IDbSet<SeoMng> _RSeoMng;
        public EFSeoMngRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RSeoMng = _uow.Set<SeoMng>();
        }
      
        public int SaveSeoMng(SeoMng entitySeo)
        {
            if (entitySeo.Id == 0)
            {

                _RSeoMng.Add(entitySeo);
            }
            else
            {
                _uow.Entry(entitySeo).State = EntityState.Modified;
            }
            return  _uow.SaveChanges();
        }


        public SeoMng ReadSeoMang(string url)
        {
            var seoEntity = _RSeoMng.FirstOrDefault(_ => _.urlTillActions == url);

            return seoEntity;
        }
    }
}
