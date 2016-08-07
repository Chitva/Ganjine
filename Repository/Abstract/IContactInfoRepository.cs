using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IContactInfoRepository
    {
        IQueryable<ContactInfo> ContactInfos { get; }
        void SaveContactInfo(ContactInfo ContactInfo);
        ContactInfo DetailsContactInfo(int Id);
        void DeleteContactInfo(ContactInfo ContactInfo);
    }
}

