using Domain.Entities;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Repository.Abstract;
using Domain.ViewModel.Admin;

namespace WebUI.Infrastructure.Extentions.Admin
{
    public class OrderExtensions
    {
        IProductOrderRepository _ROrder;
        IServiceRepository _RProduct;
        IUserAccountRepository _RDefineUser;
       
        public OrderExtensions(IProductOrderRepository orderRepository, IUserAccountRepository DefineUserRepository,IServiceRepository serviceRepository)
        {
            _ROrder = orderRepository;
            _RDefineUser = DefineUserRepository;
            _RProduct = serviceRepository;
            //EDefineUser = new DefineUserExtentions(_RDefineUser);
        }

        //public IQueryable<ContactUs> ContactUsAdmin()
        //{
        //    return _RContact.ContactUs;
        //}
        public IQueryable<OrderViewModel> GetOrdersAdmin(int Start, int End, int LanguageId)
        {
            return _ROrder.ProductOrder.Where(x => x.LanguageId == LanguageId)
                .OrderByDescending(x => x.Id).Skip(Start).Take(End)
                .Select(p => new OrderViewModel()
            {
               Id = p.Id,Address = p.Address ,Email = p.Email ,Family = p.Family ,
               IsMale =  p.IsMale ,LanguageId =  p.LanguageId ,Name =  p.Name,
               OrderDate = p.OrderDate ,OrderStatus = p.OrderStatus ,OrderText = p.OrderText,
               ReadDate = p.ReadDate,Reader = p.Reader ,ServiceGroupId = p.ServiceGroupId,
               ServiceGroupName = _RProduct.ServiceGroups.FirstOrDefault(_ => _.Id == p.ServiceGroupId).Name,
               Tell = p.Tell
               
            });
        }
        public int GetCountOrdersAdmin(int LanguageId)
        {

            return _ROrder.ProductOrder.Count(x => x.LanguageId == LanguageId);
        }
        
    }
}