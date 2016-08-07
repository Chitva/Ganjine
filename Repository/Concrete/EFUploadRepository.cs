using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System.Security.Cryptography;
namespace RepositoryLayer.Concrete
{
    public class EFUploadRepository : IUploadRepository
    {
        IUnitOfWork _uow;
        
        IDbSet<Upload> _RUpload;
        public EFUploadRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RUpload = _uow.Set<Upload>();
        }
        public IQueryable<Upload> Uploads
        {
            get { return _RUpload; }
        }
        public void SaveUpload(Upload Upload)
        {
            if (Upload.Id == 0)
            {

                _RUpload.Add(Upload);
            }
            else
            {
                _uow.Entry(Upload).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public Upload DetailsUpload(int Id)
        {
            return _RUpload.Find(Id);
        }
        public void DeleteUpload(Upload Upload)
        {
            _RUpload.Remove(Upload);
            _uow.SaveChanges();
        }
    }
}
