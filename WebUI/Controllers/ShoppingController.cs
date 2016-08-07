//using DataLayer.Context;
//using Domain.Entities;
//using Domain.ViewModel.User;
//using Repository.Abstract;
//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Transactions;
//using System.Web;
//using System.Web.Mvc;

//namespace WebUI.Controllers
//{
//    public class ShoppingController : Controller
//    {
//        IShoppingCartRepository _rShoppingCart;
//        IServiceRepository _rService;
//        IUserAccountRepository _rAccount;
//        ICityProvinceRepository _rCityProvince;
//        IUnitOfWork _uow;
//        public ShoppingController(IShoppingCartRepository shoppingRepository,IServiceRepository serviceRepository
//            ,IUnitOfWork uow ,IUserAccountRepository accountRepository ,ICityProvinceRepository cityProvinceRepository)
//        {
//            _rShoppingCart = shoppingRepository;
//            _uow = uow;
//            _rService = serviceRepository;
//            _rAccount = accountRepository;
//            _rCityProvince = cityProvinceRepository;
//        }


//        [HttpGet]
//        public ActionResult MyShoppingCart()
//        {
//            var model = new List<ShoppingCartViewModel>();

//            InitializeData();

//            //first check if the userId session is not null we retrieve its row from database
//            if (Session["UserId"] != null)
//            {
//                var userId = Convert.ToInt32(Session["UserId"].ToString());

//                var shoppingCartRows = _rShoppingCart.ShoppingCarts.Where(_ => _.UserId == userId);

//                foreach (var item in shoppingCartRows)
//                {
//                    var totalPrice = item.Product.Price != null ? (item.UnitCount * item.Product.Price.Value) : 0;
//                    var totalDiscount = item.Product.Discount != null ? (item.UnitCount * item.Product.Discount.Value) : 0;
//                    var obj = new ShoppingCartViewModel()
//                    {
//                       ProductId = item.ProductId ,
//                       ProductName = item.Product.Name ,
//                       UnitPrice = item.Product.Price.Value ,
//                       Count = item.UnitCount ,
//                       TotalDiscount = totalDiscount,
//                       TotalPrice = totalPrice,
//                       TotalPayment = (totalPrice - totalDiscount) ,
//                       Id = item.Id
//                    };

//                    model.Add(obj);
//                }
//            }
//            else
//            {
//                if(Request.Cookies["ShoppingCartIds"] != null)
//                {
//                    var dbIds = Request.Cookies["ShoppingCartIds"].ToString().Split('-');
//                    foreach (var str in dbIds)
//                    {;
//                        int id = Convert.ToInt32(str);
//                        var row = _rShoppingCart.ShoppingCarts.FirstOrDefault(_ => _.Id == id);
//                        var totalPrice = row.Product.Price != null ? (row.UnitCount * row.Product.Price.Value) : 0;
//                        var totalDiscount = row.Product.Discount != null ? (row.UnitCount * row.Product.Discount.Value) : 0;
//                        var obj = new ShoppingCartViewModel()
//                        {
//                            ProductId = row.ProductId,
//                            ProductName = row.Product.Name,
//                            UnitPrice = row.Product.Price.Value,
//                            Count = row.UnitCount,
//                            TotalDiscount = totalDiscount,
//                            TotalPrice = totalPrice,
//                            TotalPayment = (totalPrice - totalDiscount),
//                            Id = row.Id
//                        };

//                        model.Add(obj);
//                    }
//                }
//            }

//            return View(model);
//        }

//        [HttpPost]//تعداد اقلام هر سطر سبد خرید را  تغییر میدهد
//        public ActionResult EditShoppingCartDetails(int shoppingCartId ,int newCount)
//        {
//            var result = false;
//            var entity = _rShoppingCart.ShoppingCartDetails(shoppingCartId);
//            if(entity != null)
//            {
//                entity.UnitCount = newCount;
//                result = _rShoppingCart.SaveShoppingCart(entity);
//            }
           
//            return Json(result);
//        }

//        [HttpPost]
//        public ActionResult DeleteShoppingCartDetails (int shoppingCartId)
//        {
//            var result = false;
//            var entity = _rShoppingCart.ShoppingCartDetails(shoppingCartId);
//            result = _rShoppingCart.DeleteShoppingCart(entity);

//            return Json(result);
//        }


//        [HttpGet]
//        //دکمه مرحله بعد در تب سبد خرید
//        //اطلاعات آدرس انتخاب شده و هزینه ی ارسال وارسال یاعدم ارسال فاکتور  در سه آبجکت تمپ دیتا ذخیره میشود
//        //و بسمت صفحه ی بعد که زمان صدور فاکتور است ارسال میشود
//        public ActionResult SubmitMyShoppingCart()
//        {
//            if (Session["UserId"] != null)
//            {
//                return RedirectToAction("Login", "Home");
//            }
//            else
//            {
//                var userId = Convert.ToInt32(Session["UserId"].ToString());
//                var model = new List<UserAddressVM>();
//                var addresses = _rAccount.UserAddresses.Where(_ => _.UserId == userId);

//                foreach (var address in addresses)
//                {
//                    var obj = new UserAddressVM()
//                    {
//                        Id = address.Id,
//                        CityId = address.CityId,
//                        CityName = address.City.CityName,
//                        ProvinceId = address.City.ProvinceId,
//                        ProvinceName = address.City.Province.ProvinceName,
//                        CompanyName = address.CompanyName,
//                        FirstName = address.FirstName,
//                        LastName = address.LastName,
//                        LeftAddress = address.LeftAddress,
//                        Mobile = address.Mobile,
//                        Phone = address.Phone,
//                        PostalCode = address.PostalCode,
//                        Title = address.Title
//                    };

//                    model.Add(obj);
//                }
               
//                return View(model);
//            }
//        }

//        [HttpPost]
//        public ActionResult AddNewAddress(UserAddressVM model)
//        {
            
//            var obj = new UserAddress()
//            {
//                Id = model.Id ,
//                CityId = model.CityId ,
//                CompanyName = model.CompanyName ,
//                FirstName = model.FirstName ,
//                LastName = model.LastName ,
//                Mobile = model.Mobile ,
//                Phone = model.Phone ,
//                PostalCode = model.PostalCode ,
//                LeftAddress = model.LeftAddress ,
//                Title = model.Title ,
//                UserId = model.UserId.Value
//            };

//           var result =  _rAccount.SaveUserAddress(obj);

//            return Json(result);

//        }

//        //[HttpPost]
//        //public ActionResult IsSendBill

//        //[HttpGet]//دکمه مرحله بعد در تب اطلاعات ارسال
//        //public ActionResult InvoicePublish()
//        //{
//        //    if (Session["UserId"] != null)
//        //    {
//        //        var userId = Convert.ToInt32(Session["UserId"].ToString());
//        //        var cartRows = _rShoppingCart.ShoppingCarts.Where(_ => _.UserId == userId);
//        //        var sendingCost = TempData["SendingCost"]
//        //        using (var tr = new TransactionScope())
//        //        {
//        //            var invoice = new Invoice()
//        //            {

//        //            }
//        //            foreach (var cart in cartRows)
//        //            {

//        //            }

//        //        }

//        //    }
//        //}


//        //[HttpPost]
//        //public ActionResult InvoicePublish()
//        //{

//        //}


//        private void InitializeData()
//        {
//            ViewBag.FooterColName = _uow.Set<FooterColumnName>()
//                .Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
//            var x1 = _rService.ServiceGroups.Where(x => x.LanguageId == 1);
//            if (x1.Any())
//                ViewBag.ServiceGroup = x1.ToList();

//            var x2 = _uow.Set<WorkExperienceGroup>().Where(x => x.LanguageId == 1);
//            if (x2.Any())
//                ViewBag.WorkExGroup = x2.ToList();

//            var x7 = _uow.Set<ServiceTab>();
//            if (x7.Any())
//                ViewBag.ServiceTabs = x7.ToList();
//        }
//    }
//}
