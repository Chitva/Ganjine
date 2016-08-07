//using System;
//using System.Configuration;
//using System.Web.Mvc;


//namespace WebUI.Areas.Admin.Controllers
//{
//    public class SMSController : Controller
//    {

//        [HttpGet]
//        public ActionResult SendSMS()
//        {
//            if (IsValidSessions())
//            {
//                return View();
//            }
//            else
//               return  RedirectToAction("Login", "Home");
//        }


//        //[HttpPost]
//        //public ActionResult SendSMS(string message ,string receipentNumbers )
//        //{
//        //    var userName = ConfigurationManager.AppSettings["SMSUserName"].ToString();
//        //    var password = ConfigurationManager.AppSettings["SMSPassword"].ToString();
//        //    var domain = ConfigurationManager.AppSettings["SMSDomain"].ToString();
//        //    var senderNumber = ConfigurationManager.AppSettings["SenderNumber"].ToString();
//        //    string[] receipents = receipentNumbers.Split(';');

//        //    if (IsValidSessions())
//        //    {
//        //        try
//        //        {
//        //            smsSendWebService obj = new smsSendWebService();
//        //             var credit = obj.getCredit(userName, password, domain);

//        //            if (Convert.ToInt32(credit) > 0)
//        //            {
//        //                long[] smsIds = obj.SendSms(userName, password, domain,
//        //                    new string[] { message },
//        //                    receipents, senderNumber, SendType.StaticText, SmsMode.SaveInPhone);

//        //                //int[] results = obj.GetAllDelivery(userName, password, domain, smsIds, senderNumber);
//        //                return Json(true);
//        //            }
//        //            else
//        //                return Json(false);
                    
//        //        }
//        //        catch (Exception )
//        //        {
//        //            return Json(false);
//        //        }
//        //    }
//        //    else
//        //        return RedirectToAction("Login", "Home");
            

//        //}


//        private bool IsValidSessions()
//        {
//            if (Session["admin"] != null)
//            {
//                return true;
//            }
//            else
//                return false;
//        }
//    }
//}