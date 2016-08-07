using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPaymentProvider
    {
        PaymentConnectResponse Connect(long invoiceId, long amount, string callback);

        PaymentVerifyResponse Verify(JObject providerRequest);
    }

    /// <summary>
    /// 0-19    Success
    /// 20-149  Errors
    /// 150-199 Verify
    /// 200-255 Undefined errors
    /// 255:    Unkown
    /// </summary>
    public enum PaymentStatus
    {
        #region Success

        Success = 0,
        VerifySuccess = 1,

        #endregion

        #region Errors

        //شماره کارت نامعتبر است
        InvalidCardNumber = 20,

        //موجودی کافی نیست 
        InadequateCredibility = 21,
        // 
        IncorrectPassword = 22,

        //تعداد دفعات وارد کردن رمز بیش از حد مجاز است
        ExceedAllowablePasswordImport = 23,

        //کارت نامعتبر است
        InvalidCard = 24,

        //كاربر از انجام تراكنش منصرف شده است
        TransactionDiscardedByUser = 25,

        //تاریخ انقضای کارت گذشته است
        ExpiryDateIsPassed = 26,

        //صادرکننده کارت نامعتبر
        InvalidCardExporter = 27,

        //پاسخی از صادر کننده کارت دریافت نشد
        NoRespondFromCardExporter = 28,

        //پذیرنده نامعتبر است 
        InvalidReciever = 29,

        //خطای امنیتی رخ داده است
        SecurityError = 30,

        //اطلاعات كاربري پذيرنده نامعتبر است
        InvalidRecieverAccountInfo = 31,

        //مبلغ نامعتبر است
        InvalidAmount = 32,

        //پاسخ نامعتبر است
        InvalidRespond = 33,

        //فرمت اطلاعات وارد شده صحيح نمي باشد
        IncorrectInputDataFormat = 34,

        //حساب نامعتبر است
        InvalidBankAccount = 35,

        //خطاي سيستمي
        SystemError = 36,

        //تاريخ نامعتبر است
        InvalidDateTime = 37,

        //شماره درخواست تكراري است
        DuplicateRequestNumber = 38,

        //شده استReverseتراکنش
        TransactionIsReversed = 39,

        //یافت نشدRefundتراکنش
        RefundTransactionNoyFound = 40,

        //
        SessionTimeOut = 41,

        //خطا در ثبت اطلاعات
        SaveInfoError = 42,

        //اشكال در تعريف اطلاعات مشتري
        DefiningUserInfoBug = 43,

        //اشكال در تعريف اطلاعات مشتري
        ExceedAllowableInformationInput = 44,

        //IPنامعتبراست
        InvalidIP = 45,

        //تراكنش تكراري است
        DuplicateTransaction = 46,

        //تراكنش مرجع موجود نيست
        ReferenceTransactionNotExist = 47,

        //تراكنش نامعتبر است
        InvalidTransaction = 48,

        //خطا در واريز
        SettlementError = 49,

        #endregion

        #region Payment Verification

        // داده شده استverifyقبلا درخواست  
        VerificationRequestSentBefore = 150,

        //یافت نشدverify درخواست 
        VerifyRequestNotFound = 151,

        #endregion

        #region Undefined Errors

        UnSuccess = 254,

        #endregion

        Unkonwn = 255
    }

    public class PaymentConnectResponse
    {
        public PaymentProvider Provider { get; set; }

        public PaymentStatus Status { get; set; }

        public string GatewayUrl { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }

    public class PaymentVerifyResponse
    {
        public PaymentStatus Status { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }

    public enum PaymentProvider
    {
        Mellat,
        Saman,
        CafeBazaar
    }
}
