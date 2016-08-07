using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ViewModel.Admin;

namespace RepositoryLayer.Abstract
{
    public interface INewsRepository
    {
        IQueryable<News> News { get; }
        void SaveNews(News News);
        News DetailsNews(int? Id);
        void DeleteNews(News News);
        IQueryable<NewsGallery> NewsGalleries { get; }
        void SaveNewsGallery(NewsGallery NewsGallery);
        NewsGallery DetailsNewsGallery(int Id);
        void DeleteNewsGallery(NewsGallery NewsGallery);
    }
}

