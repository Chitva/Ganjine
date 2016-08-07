using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Abstract
{
    public interface IFooterRepository
    {
        void SaveFooter(Footer Footer);
        IQueryable<FooterColumnName> FooterColumnNames { get; }
        void SaveFooterColumnName(FooterColumnName FooterColumnName);
        FooterColumnName DetailsFooterColumnName(int Id);
        IQueryable<Footer> Footers { get; }
        Footer DetailsFooter(int Id);
        void DeleteFooter(Footer Footer);
    }
}
