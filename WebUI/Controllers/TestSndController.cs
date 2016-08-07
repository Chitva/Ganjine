using DataLayer.Context;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class TestSndController : Controller
    {
        IUnitOfWork _uow;
        public TestSndController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult Index()
        {
            InitializeData();

            return View();
        }


        [HttpPost]
        public ActionResult SendEmail()
        {
            string from = "info@tandistalaei.com";
            string[] recievers = new string[9] {
                "galaxygift@gmail.com",
                "s0.azizi@gmail.com",
                "dotnetdeveloperxx@gmail.com",
                "dotnetdeveloperes@gmail.com",
                "pooyanikbakht@gmail.com",
                "pooyanikbakht@yahoo.com",
                "hosseini114@hotmail.com",
                "farzadho68@gmail.com",
                "farzadho68@yahoo.com" };
            const string errorReturn = "ارسال ایمیل با خطامواجه شد";
           
            try
            {
                var fromAddress = new MailAddress(from);
                var smtp = new SmtpClient
                {
                    Host = "5.9.224.230",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "chitva123456")
                };

                MailMessage msg = new MailMessage();
                msg.From = fromAddress;
                //msg.To.Add("galaxygift@gmail.com");
                foreach (var str in recievers)
                {
                    try
                    {
                        msg.Bcc.Add(str);
                    }
                    catch { }
                }
                msg.Subject = "این پیام برای تست ارسال میشود";
                msg.Body = "متن این پیام برای تست ارسال شده است";

                smtp.Send(msg);

                return Json(new { result = "ارسال ایمیل با موفقیت انجام شد" });
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(Server.MapPath("~/error.txt"), ex.ToString());
                return Json(new { result = errorReturn });
            }
        }

        public void InitializeData()
        {
            ViewBag.FooterColName = _uow.Set<FooterColumnName>().Where(x => x.Name.Length > 0 && x.LanguageId == 1).OrderByDescending(x => x.Id).ToList();
            var x1 = _uow.Set<ServiceGroup>().Where(x => x.LanguageId == 1);
            if (x1.Any())
                ViewBag.ServiceGroup = x1.ToList();

            var x2 = _uow.Set<WorkExperienceGroup>().Where(x => x.LanguageId == 1);
            if (x2.Any())
                ViewBag.WorkExGroup = x2.ToList();

            var x7 = _uow.Set<ServiceTab>();
            if (x7.Any())
                ViewBag.ServiceTabs = x7.ToList();

        }
    }
}