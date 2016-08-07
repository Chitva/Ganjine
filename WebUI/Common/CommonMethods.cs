using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace WebUI.Common
{
    public class CommonMethods
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";


        //ChangeUnknownCharactersToDash
        public static string ChangeUnKnownCharacters(string inputstring)
        {
            return Regex.Replace(inputstring, @"[^A-Za-z0-9]+", "_");
        }

        public static string GetClientIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        public static string GetComputerName()
        {
            return HttpContext.Current.Request.UserHostName;
        }

        //written By Azizi 95_03_24
        public static string Encrypt(string userPassword)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(userPassword);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        //written By Azizi 95_03_24
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        //written By Azizi 95_03_26
        public static string TemporarySaveImage(HttpPostedFileBase picture , int userId ,int order )
        {
            
            var path = HttpContext.Current.Server.MapPath($"~/Images/{userId}");
         

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
         
            try
            {
               
                if (picture != null && picture.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(picture.FileName);

                    if (fileExtension == ".jpg" || fileExtension == ".png" ||
                        fileExtension == ".jpeg" || fileExtension == ".gif")
                    {
                        string fileName = order.ToString() + "_" + DateTime.Now.Ticks.ToString() + fileExtension;

                    if (File.Exists(Path.Combine(path, fileName)))
                        {
                            File.Delete(Path.Combine(path, fileName));
                        }

                        picture.SaveAs(Path.Combine(path, fileName));
                        return Path.Combine(path, fileName);
                    }
                 }
               
                   return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static bool WritingExternalEmail(string[] recieverEmails, string content, string subject)
        {

            var siteEmail = ConfigurationManager.AppSettings["SiteEmail"];
            var password = ConfigurationManager.AppSettings["EmailPassword"];
            var hostIp = ConfigurationManager.AppSettings["HostIP"];
            try
            {
                var fromAddress = new MailAddress(siteEmail);
                var smtp = new SmtpClient
                {
                    Host = hostIp,
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };

                MailMessage msg = new MailMessage();
                msg.From = fromAddress;
                if (recieverEmails.Length == 1)
                    msg.To.Add(recieverEmails[0]);
                else
                foreach (var str in recieverEmails)
                {
                    try
                    {
                        msg.Bcc.Add(str);
                    }
                    catch { }
                }
                msg.Subject = subject;
                msg.Body = content;

                smtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static DateTime ConvertToPersianDatetime(DateTime datetime)
        {
            var per = new PersianCalendar();

            var year = per.GetYear(datetime);
            var month = per.GetMonth(datetime);
            var day = per.GetDayOfMonth(datetime);
            var hour = per.GetHour(datetime);
            var minute = per.GetMinute(datetime);

            return new DateTime(year, month, day, hour, minute, 0);
        }

       
    }
}