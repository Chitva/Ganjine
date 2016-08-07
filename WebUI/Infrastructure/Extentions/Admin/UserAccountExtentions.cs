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
    public class UserAccountExtentions
    {
        IUserAccountRepository _RUser;
        public UserAccountExtentions(IUserAccountRepository UserRepository)
        {
            _RUser = UserRepository;
        }
       
        public IQueryable<Menu> GetChildParent(int ParentID)
        {
            return _RUser.Menus.Where(p => p.ParentID == ParentID);
        }
        public IQueryable<UserAccount> UserList
        {
            get { return _RUser.UserAccounts.Where(p => p.Role.RoleName == "Admin"); }
        }
        public IQueryable<UserAccount> GetUser(int Start, int End)
        {
            return _RUser.UserAccounts.Where(p => p.Role.RoleName == "Admin" && p.Id != 1)
                .OrderByDescending(x => x.Id).Skip(Start).Take(End);
        }
        public IQueryable<UserAccount> AllUserList
        {
            get
            {
                return _RUser.UserAccounts;
            }
        }
        public IQueryable<UserAccount> AllGetUser(int Start, int End)
        {
            return _RUser.UserAccounts.Where(_=>_.Role.RoleName =="Admin" && _.Id != 1).OrderByDescending(x => x.Id).Skip(Start).Take(End);
        }
        
        public int GetCountUsers()
        {
            return _RUser.UserAccounts.Where(p => p.Role.RoleName == "Admin" ).Count();
        }
        
        public void DeleteUser(int Id)
        {
            var DefineUser = _RUser.UserAccountDetails(Id);
            DefineUser.IsActive = false;
            //DefineUser.Email = "-";
            _RUser.SaveUserAccount(DefineUser);
        }
        public Domain.Entities.UserAccount ValidationUser(string Username, string CurrentPass)
        {
            var pass = Common.CommonMethods.Encrypt(CurrentPass);
            var DefineUser = _RUser.UserAccounts.FirstOrDefault(p => p.Email == Username+"@admin.com" && p.EncrypedPass == pass );
            return DefineUser;
        }
        

        /// <summary>
        /// It check whether the pass and encryped pass match eachother or not
        /// if they are matched check whether there is any user in the database with this password or not
        /// modified by Azizi 95_03_25
        /// </summary>
        /// <param name="encrypedpass">the password which is encryped</param>
        /// <param name="password">the plain text of password without encryption</param>
        /// <returns></returns>
        public bool GetHashCodeValidation(string encrypedpass, string password)
        {
            var decryptedpass = Common.CommonMethods.Decrypt(encrypedpass);
            if (decryptedpass == password)
            {
                var DefineUser = _RUser.UserAccounts.FirstOrDefault(p => p.EncrypedPass == encrypedpass);
                return (DefineUser != null) ? true : false;
            }
            else
                return false;
        }


        /// <summary>
        /// return the user Id 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int GetUserCode(string username, string password)
        {
            var pass = Common.CommonMethods.Encrypt(password);
            var DefineUser = _RUser.UserAccounts.FirstOrDefault(p => p.Email == username+"@admin.com" && p.EncrypedPass == pass);
            return DefineUser.Id;
        }
        public bool IsExistUserName(string username)
        {
            var DefineUser = _RUser.UserAccounts.FirstOrDefault(l => l.Email == username+"@admin.com");
            return (DefineUser != null) ? false : true;
        }
        public bool IsTruePassword(int UserCode, string Password)
        {
            var pass = Common.CommonMethods.Encrypt(Password);
            var DefineUser = _RUser.UserAccounts.FirstOrDefault(p => p.Id == UserCode && p.EncrypedPass == pass);
            return (DefineUser != null) ? true : false;
        }
        public void ChangePassByAdmin(int UserCode, string Password)
        {
            var DefineUser = _RUser.UserAccountDetails(UserCode);
            DefineUser.EncrypedPass = Common.CommonMethods.Encrypt(Password);
            _RUser.SaveUserAccount(DefineUser);
        }
        public bool IsMasterAdmin(int UserCode)
        {
            return true;     
        }
        public int GetUserCodeByHashCode(string HashCode)
        {
            
            return _RUser.UserAccounts.FirstOrDefault(p => p.EncrypedPass == HashCode).Id;
        }

        public bool IsExistPassword(int Code, string OldPassword)
        {
            var pass = Common.CommonMethods.Encrypt(OldPassword);
            var s = _RUser.UserAccounts.FirstOrDefault(x => x.EncrypedPass ==pass  && x.Id == Code);
            return (s != null) ? true : false;
        }
        
        public UserAccount FillDefineUser(RegisterModel RegisterModel)
        {
            UserAccount DefineUser = new UserAccount();
            DefineUser.IsActive = true;
            DefineUser.Email = RegisterModel.UserName;
            DefineUser.LastName = RegisterModel.Family;
            DefineUser.Name = RegisterModel.Name;
            DefineUser.RoleId = 1;
            DefineUser.CreateDate = DateTime.Now.Date;
            DefineUser.EncrypedPass = Common.CommonMethods.Encrypt(RegisterModel.Password);
           
            return DefineUser;
        }
       
       
    }
}