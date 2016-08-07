//using Domain.Entities;
//using Domain.Validation.Admin;

//using Repository.Abstract;
//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;
//namespace WebUI.Infrastructure.Extentions.Admin
//{
//    public class ServiceExtentions
//    {
//        IServiceRepository _RService;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        public ServiceExtentions(IServiceRepository ServiceRepository, IUserAccountRepository DefineUserRepository)
//        {
//            _RService =ServiceRepository;
//            _RDefineUser = DefineUserRepository;
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }

//        public IQueryable<ServiceGroup> ServiceGroupsAdmin()
//        {
//            return _RService.ServiceGroups;
//        }
//        public IQueryable<ServiceTab> GetServiceTabAdmin(int Start, int End, int ServiceGroupId)
//        {
//            return _RService.ServiceTabs.Where(x => x.ServiceGroupId == ServiceGroupId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
//        }
//        public int GetCountServiceTabAdmin(int ServiceGroupId)
//        {
//            return _RService.ServiceTabs.Where(x => x.ServiceGroupId == ServiceGroupId).Count();
//        }
//        public IQueryable<ServiceTabFile> GetServiceTabFileAdmin(int Start, int End, int ServiceTabId)
//        {
//            return _RService.ServiceTabFiles.Where(x => x.ServiceTabId == ServiceTabId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
//        }
//        public int GetCountServiceTabFileAdmin(int ServiceTabId)
//        {
//            return _RService.ServiceTabFiles.Where(x => x.ServiceTabId == ServiceTabId).Count();
//        }
//        public void DeleteImageGallery(ServiceTabFile ServiceTabFile)
//        {

//            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Images/TabGalleryService/" + ServiceTabFile.File));
//            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Images/TabGalleryService/thum/" + ServiceTabFile.File));
//            _RService.DeleteServiceTabFile(ServiceTabFile);
//        }

//    }
//}