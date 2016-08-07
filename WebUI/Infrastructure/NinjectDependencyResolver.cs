using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using RepositoryLayer.Abstract;
using RepositoryLayer.Concrete;
using DataLayer.Context;
using Repository.Abstract;
using Repository.Concrete;


namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
           // put additional bindings here
            kernel.Bind<IUnitOfWork>().To<EFDbContext>();
            kernel.Bind<ISettingRepository>().To<EFSettingRepository>();
            kernel.Bind<IBannerRepository>().To<EFBannerRepository>();
            kernel.Bind<INewsRepository>().To<EFNewsRepository>();
            kernel.Bind<IUploadRepository>().To<EFUploadRepository>();
            kernel.Bind<IContactInfoRepository>().To<EFContactInfoRepository>();
            kernel.Bind<IContactRepository>().To<EFContactRepository>();
            kernel.Bind<IServiceRepository>().To<EFServiceRepository>();
            kernel.Bind<IQuestionRepository>().To<EFQuestionRepository>();
            kernel.Bind<IWorkExperienceRepository>().To<EFWorkExperienceRepository>();
            kernel.Bind<IFooterRepository>().To<EFFooterRepository>();
            kernel.Bind<IProductOrderRepository>().To<EFProductOrderRepository>();
            kernel.Bind<ISeoMngRepository>().To<EFSeoMngRepository>();
            kernel.Bind<IInvoiceRepository>().To<EFInvoiceRepository>();
            kernel.Bind<IUserAccountRepository>().To<EFUserAccountRepository>();
            kernel.Bind<IShoppingCartRepository>().To<EFShoppingCartRepository>();
            kernel.Bind<ISaleAgenciesRepository>().To<EFSaleAgenciesRepository>();
            kernel.Bind<ICityProvinceRepository>().To<EFCityProvinceRepository>();
            kernel.Bind<IArticleRepository>().To<EFArticleRepository>();
            kernel.Bind<IStoryRepository>().To<EFStoryRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}
