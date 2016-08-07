using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public int TotalPrice { get; set; } // the price is calculated from sum of (Price - Discount) of invoiceDetails 

        public int SendingCost { get; set; }
        
        public DateTime RegisteredDate { get; set; }

        public int UserId { get; set; }
      
        public virtual UserAccount UserAccount { get; set; }

        public InvoiceStatus InvoiceStatus { get; set; }

        [MaxLength(32)]//شماره مرجع تراکنش
        public string ReferenceId { get; set; }

        [MaxLength(32)]//شماره پیگیری
        public string TrackingId { get; set; }

        [MaxLength(4)]//شماره کارت فرستنده روش کارت به کارت
        public string DestinationCardNumber { get; set; }

        //مبلغ واریزشده کارت به کارت یاواریز به حساب 
        public int? DepositedPrice { get; set; }

        //تاریخ دقیق واریز به حساب یا کارت به کارت
        public DateTime? DepositedDate { get; set; }

        public int InvoiceDiscount { get; set; }

        public ICollection<InvoiceDetail> Details { get; set; }

        public int ProvinceId { get; set; }
       
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual UserAddress Address { get; set; } 
        
        public bool SendingBillPaper { get; set; }  //ارسال فاکتور برای مشتری یا خیر  
        
        public ForwardingType? ForwardingType { get; set; }
    }

    public enum InvoiceStatus
    {
        PaymentAwaiting,//درانتظار پرداخت
        Paid, //پرداخت شده
        Forwarded //ارسال شده 
    }

    public enum ForwardingType // روش های ارسال
    {
       online ,//پرداخت اینترنتی
       PaymentInPlace ,//پرداخت در محل 
       CardToCard ,//کارت به کارت
       BankAccountDeposite //واریز به حساب بانکی
    }
}
