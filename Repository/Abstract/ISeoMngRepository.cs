using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModel.Admin;



//written By Azizi 94_08_25
namespace RepositoryLayer.Abstract
{
    public interface ISeoMngRepository
    {
        int SaveSeoMng(SeoMng seomngEntity);

        SeoMng ReadSeoMang(string url);
    }
}
