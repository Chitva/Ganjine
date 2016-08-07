using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstract
{
    public interface IInvoiceRepository
    {
        IQueryable<Invoice> Invoices { get; }
        bool SaveInvoice(Invoice invoice);
        Invoice DetailsInvoice(int Id);
        bool DeleteInvoice(Invoice invoice);

        IQueryable<InvoiceDetail> InvoiceDetails { get; }
        bool SaveInvoiceDetail(InvoiceDetail invoiceDetail);
        InvoiceDetail DetailsInvoiceDetail(int Id);
        bool DeleteInvoiceDetail(InvoiceDetail invoiceDetail);
        IQueryable<InvoiceDetail> DetailsOfInvoice(int invoiceId);


    }
}
