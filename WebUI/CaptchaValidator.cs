using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;

/// <summary>
/// Captcha validator using google API
/// 
/// Do it :
///     1- go to "https://www.google.com/recaptcha/admin" and register domains, get "SecretKey" and "SiteKey"
///     
///     2- add SecretKey & SiteKey in web.config :
///     
///         <configuration>
///             <appSettings>
///                 <add key="SecretKey" value="6LdtARMTAAAAAC6hsakNsJvUBxbPLmHaYeiky6lS"/>
///                 <add key="SiteKey" value="6LdtARMTAAAAAOeK32qn0KDj2xZLFn-PKRU7w3nK"/>
///             </appSettings>
///         </configuration>
///         
///     3- use in controller :
///     
///        [HttpPost]
///        [CaptchaValidator]
///        public ActionResult Index(bool captchaIsValid)
///        {
///             if (captchaIsValid)
///             {
///                 // do something
///             }
///             else
///             {
///                 // do something
///             }
///             
///             ...
///         }
///         
///     4- add this code in HTML :
///         if persian :
///         <script src='https://www.google.com/recaptcha/api.js?hl=fa' async defer></script>
///         if english :
///         <script src='https://www.google.com/recaptcha/api.js' async defer></script>
///         and then add :
///         <div class="g-recaptcha" data-sitekey="6LdtARMTAAAAAOeK32qn0KDj2xZLFn-PKRU7w3nK"></div>
/// 
/// Created by Masood Abdolian
/// </summary>
public class CaptchaValidatorAttribute : ActionFilterAttribute
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        try
        {
            var response = filterContext.HttpContext.Request.Form["g-recaptcha-response"];

            var secretKey = ConfigurationManager.AppSettings["SecretKey"];

            var client = new WebClient();

            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            filterContext.ActionParameters["captchaIsValid"] = captchaResponse.Success;

            base.OnActionExecuting(filterContext);
        }
        catch (Exception ex)
        {
            filterContext.ActionParameters["captchaIsValid"] = false;
        }
    }
}