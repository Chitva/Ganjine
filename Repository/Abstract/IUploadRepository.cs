using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModel.Admin;

namespace RepositoryLayer.Abstract
{
    public interface IUploadRepository
    {
        IQueryable<Upload> Uploads { get; }
        void SaveUpload(Upload Upload);
        Upload DetailsUpload(int Id);
        void DeleteUpload(Upload Upload);
    }
}

