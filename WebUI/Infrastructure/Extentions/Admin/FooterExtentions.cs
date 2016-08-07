using Domain.Entities;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace WebUI.Infrastructure.Extentions.Admin
{
    public class FooterExtentions
    {
        IFooterRepository _RFooter;
        public FooterExtentions(IFooterRepository FooterRepository)
        {
            _RFooter = FooterRepository;
        }
        public int GetCountFooter(int? FooterColumnNameId)
        {
            return _RFooter.Footers.Where(x => x.FooterColumnNameId == FooterColumnNameId).Count() ;
        }
        public IQueryable<Footer> GetFooter(int? FooterColumnNameId, int Start, int End)
        {
            return _RFooter.Footers.Where(x => x.FooterColumnNameId == FooterColumnNameId).OrderByDescending(x => x.Id).Skip(Start).Take(End) ;
        }
    }
}