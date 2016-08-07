
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace API.Services
{
    public class MellatProvider : IPaymentProvider
    {
        private long TerminalId = 782728;
        private string UserName = "anamehr";
        private string Password = "36497438";
        private string ServiceUrl = "https://bpm.shaparak.ir/pgwchannel/services/pgw?wsdl";
        private string GatewayURL = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";

        //private IInvoiceProvider InvoiceProvider { get; set; }

        //public MellatProvider(IInvoiceProvider invoiceProvider)
        //{
        //    #region new providers

        //    // new invoice provider
        //    InvoiceProvider = invoiceProvider;

        //    #endregion
        //}

        private string DefaultDate()
        {
            return DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString().PadLeft(2, '0') +
                DateTime.Now.Day.ToString().PadLeft(2, '0');

        }

        private string DefaultTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                DateTime.Now.Second.ToString().PadLeft(2, '0');
        }

        private void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender1, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
        }

        private PaymentStatus PaymentStatusFromString(string status)
        {
            switch (Convert.ToInt32(status))
            {
                case 000: return PaymentStatus.Success;
                case 011: return PaymentStatus.InvalidCardNumber;
                case 012: return PaymentStatus.InadequateCredibility;
                case 013: return PaymentStatus.IncorrectPassword;
                case 014: return PaymentStatus.ExceedAllowablePasswordImport;
                case 015: return PaymentStatus.InvalidCard;
                case 017: return PaymentStatus.TransactionDiscardedByUser;
                case 018: return PaymentStatus.ExpiryDateIsPassed;
                case 021: return PaymentStatus.InvalidReciever;
                case 023: return PaymentStatus.SecurityError;
                case 024: return PaymentStatus.InvalidRecieverAccountInfo;
                case 025: return PaymentStatus.InvalidAmount;
                case 031: return PaymentStatus.InvalidRespond;
                case 032: return PaymentStatus.IncorrectInputDataFormat;
                case 033: return PaymentStatus.InvalidBankAccount;
                case 034: return PaymentStatus.SystemError;
                case 035: return PaymentStatus.InvalidDateTime;
                case 041: return PaymentStatus.DuplicateRequestNumber;
                case 043: return PaymentStatus.VerificationRequestSentBefore;
                case 044: return PaymentStatus.VerifyRequestNotFound;
                case 048: return PaymentStatus.TransactionIsReversed;
                case 049: return PaymentStatus.RefundTransactionNoyFound;
                case 051: return PaymentStatus.DuplicateTransaction;
                case 054: return PaymentStatus.ReferenceTransactionNotExist;
                case 055: return PaymentStatus.InvalidTransaction;
                case 061: return PaymentStatus.SettlementError;
                case 111: return PaymentStatus.InvalidCardExporter;
                case 113: return PaymentStatus.NoRespondFromCardExporter;
                case 415: return PaymentStatus.SessionTimeOut;
                case 416: return PaymentStatus.SaveInfoError;
                case 418: return PaymentStatus.DefiningUserInfoBug;
                case 421: return PaymentStatus.InvalidIP;
                default: return PaymentStatus.Unkonwn;
            }
        }

        private string PaymentStatusToString(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.Success: return "000";
                case PaymentStatus.InvalidCardNumber: return "011";
                case PaymentStatus.InadequateCredibility: return "012";
                case PaymentStatus.IncorrectPassword: return "013";
                case PaymentStatus.ExceedAllowablePasswordImport: return "014";
                case PaymentStatus.InvalidCard: return "015";
                case PaymentStatus.TransactionDiscardedByUser: return "017";
                case PaymentStatus.ExpiryDateIsPassed: return "018";
                case PaymentStatus.InvalidReciever: return "021";
                case PaymentStatus.SecurityError: return "023";
                case PaymentStatus.InvalidRecieverAccountInfo: return "024";
                case PaymentStatus.InvalidAmount: return "025";
                case PaymentStatus.InvalidRespond: return "031";
                case PaymentStatus.IncorrectInputDataFormat: return "032";
                case PaymentStatus.InvalidBankAccount: return "033";
                case PaymentStatus.SystemError: return "034";
                case PaymentStatus.InvalidDateTime: return "035";
                case PaymentStatus.DuplicateRequestNumber: return "041";
                case PaymentStatus.VerificationRequestSentBefore: return "043";
                case PaymentStatus.VerifyRequestNotFound: return "044";
                case PaymentStatus.TransactionIsReversed: return "048";
                case PaymentStatus.RefundTransactionNoyFound: return "049";
                case PaymentStatus.DuplicateTransaction: return "051";
                case PaymentStatus.ReferenceTransactionNotExist: return "054";
                case PaymentStatus.InvalidTransaction: return "055";
                case PaymentStatus.SettlementError: return "061";
                case PaymentStatus.InvalidCardExporter: return "111";
                case PaymentStatus.NoRespondFromCardExporter: return "113";
                case PaymentStatus.SessionTimeOut: return "415";
                case PaymentStatus.SaveInfoError: return "416";
                case PaymentStatus.DefiningUserInfoBug: return "418";
                case PaymentStatus.InvalidIP: return "421";
                default: return "255";
            }
        }

        public PaymentConnectResponse Connect(long invoiceId, long amount, string callback)
        {
            var result = new PaymentConnectResponse()
            {
                GatewayUrl = GatewayURL,
                Status = PaymentStatus.Unkonwn,
                Provider = PaymentProvider.Mellat,
                Parameters = new Dictionary<string, string>()
            };

            try
            {
                BypassCertificateError();
                WebUI.MellatBank.PaymentGatewayImplService pg = new WebUI.MellatBank.PaymentGatewayImplService();
               
                //MellatBank.PaymentGatewayClient bp = new WebUI.MellatBank.PaymentGatewayClient();

                var bankResult = pg.bpPayRequest(TerminalId, UserName, Password,
                    Convert.ToInt64(invoiceId), amount, DefaultDate(), DefaultTime(), "", callback, 0).Split(',');

                result.Status = PaymentStatusFromString(bankResult[0]);

                if (result.Status == PaymentStatus.Success)
                {
                    result.Parameters.Add("RefId", bankResult[1]);

                    result.Parameters.Add("HTML",
                        File.ReadAllText(System.Web.HttpContext.Current.Request.MapPath(@"~\Services\Payment\connect.html"))
                        .Replace("%Result%", JsonConvert.SerializeObject(result))
                    );
                }
                else
                {
                    result.Parameters.Add("Code", PaymentStatusToString(result.Status));

                    result.Parameters.Add("Description", result.Status.ToString());

                    result.Parameters.Add("HTML",
                        File.ReadAllText(System.Web.HttpContext.Current.Request.MapPath(@"~\Services\Payment\error.html"))
                        .Replace("%Result%", JsonConvert.SerializeObject(result))
                    );
                }
            }
            catch (Exception)
            {

            }

            return result;
        }

        public PaymentVerifyResponse Verify(JObject providerRequest)
        {
            var result = new PaymentVerifyResponse()
            {
                Status = PaymentStatus.Unkonwn,
                Parameters = new Dictionary<string, string>() { }
            };

            var refId = ((JValue)(providerRequest["RefId"])).Value.ToString();
            var resCode = ((JValue)(providerRequest["ResCode"])).Value.ToString();
            var saleOrderId = long.Parse(((JValue)(providerRequest["SaleOrderId"])).Value.ToString());
            var saleReferenceId = long.Parse(((JValue)(providerRequest["SaleReferenceId"])).Value.ToString());

            if (PaymentStatusFromString(resCode) == PaymentStatus.Success)
            {
                try
                {
                    BypassCertificateError();

                    WebUI.MellatBank.PaymentGatewayImplService bp = new WebUI.MellatBank.PaymentGatewayImplService();

                    var bankResult = bp.bpVerifyRequest(TerminalId, UserName, Password, saleOrderId, saleOrderId, saleReferenceId);

                    if (!string.IsNullOrEmpty(bankResult))
                    {
                        result.Status = PaymentStatusFromString(bankResult);
                    }
                    else
                    {
                        bankResult = bp.bpInquiryRequest(TerminalId, UserName, Password, saleOrderId, saleOrderId, saleReferenceId);

                        if (!string.IsNullOrEmpty(bankResult))
                        {
                            result.Status = PaymentStatusFromString(bankResult);
                        }
                        else
                        {
                            bankResult = bp.bpReversalRequest(TerminalId, UserName, Password, saleOrderId, saleOrderId, saleReferenceId);

                            result.Status = !string.IsNullOrEmpty(bankResult) ? PaymentStatusFromString(bankResult) : PaymentStatus.UnSuccess;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

            result.Parameters.Add("Code", PaymentStatusToString(result.Status));

            result.Parameters.Add("Description", result.Status.ToString());

            result.Parameters.Add("RefId", refId);

            result.Parameters.Add("ResCode", resCode);

            result.Parameters.Add("SaleOrderId", saleOrderId.ToString());

            result.Parameters.Add("SaleReferenceId", saleReferenceId.ToString());

            result.Parameters.Add("HTML",
                File.ReadAllText(System.Web.HttpContext.Current.Request.MapPath(@"~\Payment\callback.html"))
                .Replace("%Result%", JsonConvert.SerializeObject(result))
            );

            if (false)
            {
                // ??? InvoiceProvider.Finalize(saleOrderId, "Mellat", refId, saleReferenceId.ToString());
            }

            return result;
        }
    }
}