using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using Domain.Entities;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using Repository.Abstract;
using RepositoryLayer.Abstract;
using WebUI.Infrastructure.Extentions.Admin;

namespace WebUI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
       
        IProductOrderRepository _ROrder;
        IServiceRepository _RService;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
       
        OrderExtensions EOrder;
        public OrderController(IProductOrderRepository OrderRepository, IUserAccountRepository DefineUserRepository,
            IServiceRepository serviceRepository)
        {
            _ROrder = OrderRepository;
            _RDefineUser = DefineUserRepository;
            _RService = serviceRepository;
            EDefineUser = new UserAccountExtentions(_RDefineUser);
            EOrder = new OrderExtensions(_ROrder ,_RDefineUser,_RService);
        }
        [HttpGet]
        public ActionResult OrderList(int Page = 1)
        {
            
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                int Start = (Page - 1) * 10;
                int End = 10;
                TempData["Count"] =EOrder.GetCountOrdersAdmin(LanguageId);
                var orderList = _ROrder.ProductOrder.Where(x => x.LanguageId == LanguageId)
               .OrderByDescending(x => x.Id).Skip(Start).Take(End)
               .Select(p => new OrderViewModel()
               {
                   Id = p.Id,
                   Address = p.Address,
                   Email = p.Email,
                   Family = p.Family,
                   IsMale = p.IsMale,
                   LanguageId = p.LanguageId,
                   Name = p.Name,
                   OrderDate = p.OrderDate,
                   OrderStatus = p.OrderStatus,
                   OrderText = p.OrderText,
                   ReadDate = p.ReadDate,
                   Reader = p.Reader,
                   ServiceGroupId = p.ServiceGroupId,
                   Tell = p.Tell

               }).ToList();
                foreach (var order in orderList)
                {
                    order.ServiceGroupName =
                        _RService.ServiceGroups.FirstOrDefault(_ => _.Id == order.ServiceGroupId).Name;
                }
                ViewBag.ProductOrderList = orderList;
                //ViewBag.ProductOrderList = EOrder.GetOrdersAdmin(Start, End, LanguageId).ToList();
                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return View();
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult RefreshOrder(int Page = 1)
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                if (Page == 0) Page = 1;
                int Start = (Page - 1) * 10;
                int End = 10;
                var orderList = _ROrder.ProductOrder.Where(x => x.LanguageId == LanguageId)
               .OrderByDescending(x => x.Id).Skip(Start).Take(End)
               .Select(p => new OrderViewModel()
               {
                   Id = p.Id,
                   Address = p.Address,
                   Email = p.Email,
                   Family = p.Family,
                   IsMale = p.IsMale,
                   LanguageId = p.LanguageId,
                   Name = p.Name,
                   OrderDate = p.OrderDate,
                   OrderStatus = p.OrderStatus,
                   OrderText = p.OrderText,
                   ReadDate = p.ReadDate,
                   Reader = p.Reader,
                   ServiceGroupId = p.ServiceGroupId,
                   //ServiceGroupName = _RService.ServiceGroups.FirstOrDefault(_ => _.Id == p.ServiceGroupId).Name,
                   Tell = p.Tell

               }).ToList();

                foreach (var order in orderList)
                {
                    order.ServiceGroupName =
                        _RService.ServiceGroups.FirstOrDefault(_ => _.Id == order.ServiceGroupId).Name;
                }
                ViewBag.ProductOrderList = orderList;
                //ViewBag.ProductOrderList = EOrder.GetOrdersAdmin(Start, End, LanguageId).ToList();

                ViewBag.IsAdmin = EDefineUser.IsMasterAdmin(Convert.ToInt32(Session["admin"].ToString()));
                return PartialView("_OrderList");
            }
            else
            {
                return JavaScript("window.location.href ='/Admin/Home/Login';");
            }
        }
        [HttpPost]
        public ActionResult DeleteOrder(int Id, int Page)
        {
            if (IsValidSessions())
            {
                int LanguageId = Convert.ToInt32(Session["Language"].ToString());
                ProductOrder productOrder =_ROrder.DetailsProductOrder(Id);
                _ROrder.DeleteProductOrder(productOrder);
                int count =EOrder.GetCountOrdersAdmin(LanguageId);
                TempData["Count"] = count;
                if (count % 10 == 0)
                {
                    Page = Page - 1;
                }
                TempData["result"] = "OK";
                TempData["Message"] = "عملیات با موفقیت انجام شد.";
                return RedirectToAction("RefreshOrder", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult DetailProductOrder(int Id, int Extparam)
        {
            if (IsValidSessions())
            {
                ProductOrder productOrder =_ROrder.DetailsProductOrder(Id);
                OrderValidation validationOrder = new OrderValidation()
                {
                    Id = productOrder.Id,
                    Name = productOrder.Name,
                    Family = productOrder.Family,
                    IsMale = productOrder.IsMale,
                    Tell = productOrder.Tell,
                    Email = productOrder.Email,
                    Address =  productOrder.Address,
                    OrderText = productOrder.OrderText,
                    ServiceGroupId = productOrder.ServiceGroupId,
                    LanguageId = productOrder.LanguageId,
                    OrderDate = productOrder.OrderDate,
                    OrderStatus = productOrder.OrderStatus
                };
                validationOrder.ServiceGroupName =
                    _RService.ServiceGroups.FirstOrDefault(_ => _.Id == productOrder.ServiceGroupId).Name;
              
                ViewBag.DpStatus1 = new SelectList(new Dictionary<string, string> { { "1", "جدید" }, { "2", "درحال بررسی" }, { "3", "بررسی شده" } }, "Key", "Value", productOrder.OrderStatus);
                ViewBag.Page = Extparam;
                return View(validationOrder);
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public ActionResult BackListOrders(int Page)
        {
            return RedirectToAction("OrderList", new { Page = Page });
        }
        public ActionResult SaveReadInfo(int Page, int dpStatus, string Reader, string ReadDate, int orderId)
        {
            if (IsValidSessions())
            {
                ProductOrder productOrder = _ROrder.DetailsProductOrder(orderId);
                productOrder.OrderStatus = (OrderStatus)(dpStatus);
                productOrder.ReadDate = ReadDate;
                productOrder.Reader = Reader;
                _ROrder.SaveProductOrder(productOrder);
                return RedirectToAction("OrderList", new { Page = Page });
            }
            else
                return RedirectToAction("Login", "Home");
        }
       
        private bool IsValidSessions()
        {
            if (Session["admin"] != null)
            {
                return true;
            }
            else
                return false;
        }

    }
}
