using DataLayer.Context;
using Domain.Entities;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class EFInvoiceRepository : IInvoiceRepository
    {
        IUnitOfWork _uow;
        IDbSet<Invoice> _rInvoice;
        IDbSet<InvoiceDetail> _rInvoiceDetails;

        public EFInvoiceRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _rInvoice = _uow.Set<Invoice>();
            _rInvoiceDetails = _uow.Set<InvoiceDetail>();
        }

        public IQueryable<Invoice> Invoices
        {
            get { return _rInvoice; }
        }
        public bool SaveInvoice(Invoice invoice)
        {
            try
            {
                if (invoice.Id == 0)
                {
                    _rInvoice.Add(invoice);
                }
                else
                {
                    _uow.Entry(invoice).State = EntityState.Modified;
                }
              var result = _uow.SaveChanges();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Invoice DetailsInvoice(int id)
        {
            return _rInvoice.Find(id);
        }
        public bool DeleteInvoice(Invoice invoice)
        {
            _rInvoice.Remove(invoice);
             var result = _uow.SaveChanges();

            if (result > 0)
                return true;
            else
                return false;
        }

        public IQueryable<InvoiceDetail> InvoiceDetails
        {
            get { return _rInvoiceDetails; }
        }
        public bool SaveInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            try
            {
                if (invoiceDetail.Id == 0)
                {
                    _rInvoiceDetails.Add(invoiceDetail);
                }
                else
                {
                    _uow.Entry(invoiceDetail).State = EntityState.Modified;
                }
                var result = _uow.SaveChanges();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public InvoiceDetail DetailsInvoiceDetail(int id)
        {
            return _rInvoiceDetails.Find(id);
        }
        public bool DeleteInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            _rInvoiceDetails.Remove(invoiceDetail);
            var result = _uow.SaveChanges();

            if (result > 0)
                return true;
            else
                return false;
        }
        public IQueryable<InvoiceDetail> DetailsOfInvoice(int invoiceId)
        {
            return _rInvoiceDetails.Where(_ => _.InvoiceId == invoiceId);
        }

    }
}
