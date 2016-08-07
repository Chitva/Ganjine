using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IContactRepository
    {
        IQueryable<ContactUs> ContactUs { get; }
        ContactUs DetailsContactUs(int Id);
        void SaveContactUs(ContactUs ContactUs);
        void DeleteContactUs(ContactUs ContactUs);

    }
}
