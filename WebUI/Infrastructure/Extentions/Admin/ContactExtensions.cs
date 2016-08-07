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
//    public class ContactExtensions
//    {
//        IContactRepository _RContact;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        public ContactExtensions(IContactRepository ContactRepository, IUserAccountRepository DefineUserRepository)
//        {
//            _RContact = ContactRepository;
//            _RDefineUser = DefineUserRepository;
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }

//        //public IQueryable<ContactUs> ContactUsAdmin()
//        //{
//        //    return _RContact.ContactUs;
//        //}
//        public IQueryable<ContactUs> GetContactUsAdmin(int Start, int End,int LanguageId)
//        {
          
//            return _RContact.ContactUs.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
//        }

//        //Azizi
//        public IQueryable<ContactUs>  GetContactUsAdmin(int start,int end)
//        {
//            return _RContact.ContactUs.OrderByDescending(_ => _.Id).Skip(start).Take(end);
//        }
//        public int GetCountContactUsAdmin(int LanguageId)
//        {

//            return _RContact.ContactUs.Where(x => x.LanguageId == LanguageId).Count();
//        }

//        //Azizi
//        public int GetAllMessageCount()
//        {
//            return _RContact.ContactUs.Count();
//        }
        
//    }
//}