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
//    public class UploadExtentions
//    {
//        IUploadRepository _RUpload;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        public UploadExtentions(IUploadRepository UploadRepository, IUserAccountRepository DefineUserRepository)
//        {
//            _RUpload = UploadRepository;
//            _RDefineUser = DefineUserRepository;
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }
//        public int GetCountUpload(int LanguageId)
//        {
//            return _RUpload.Uploads.Where(x=>x.LanguageId==LanguageId).Count() ;
//        }
//        public IQueryable<Upload> GetUpload(int Start, int End, int LanguageId)
//        {
//            return _RUpload.Uploads.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
//        }
//        public void DeleteUpload(Upload Upload)
//        {
//            string FileName = Upload.FileName;
//                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Files/Upload/" + FileName));
//            _RUpload.DeleteUpload(Upload);
//        }
//    }
//}