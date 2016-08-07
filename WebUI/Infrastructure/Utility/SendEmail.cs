using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
namespace WebUI.Infrastructure.Utility
{
    public class SendEmail
    {
        //تابع ارسال ایمیل
        public void SendMail(string Body, string Email, string Title)
        {
            MailMessage message = new MailMessage("info@com", Email);
            //end
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Subject = Title;
            message.Body = Body;
            message.IsBodyHtml = true;
            //email server ip
            SmtpClient smtpClient = new SmtpClient("mail.com");
            smtpClient.Credentials = new System.Net.NetworkCredential("Support@com", "Aria2010@)!)");
            //end
            smtpClient.Send(message);
        }
        //تابع ارسال ایمیل
        public void SendMails(string Body, string Email, string Title, string Name, string EmailSite, string Password, string SmtpClient)
        {
            MailMessage message = new MailMessage(Email, EmailSite);
            //end
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Subject = Title;
            message.Body = "<b>" + Name + "</b><br/>" + Body;
            message.IsBodyHtml = true;
            //email server ip
            SmtpClient smtpClient = new SmtpClient(SmtpClient);
            smtpClient.Credentials = new System.Net.NetworkCredential(EmailSite, Password);
            //end
            smtpClient.Send(message);
        }
    }
}