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
    public class EFNewsRepository : INewsRepository
    {
        IUnitOfWork _uow;
        IDbSet<News> _RNews;
        IDbSet<NewsGallery> _RNewsGallery;
        public EFNewsRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RNewsGallery = _uow.Set<NewsGallery>();
            _RNews = _uow.Set<News>();
        }
        public IQueryable<News> News
        {
            get { return _RNews; }
        }
        public void SaveNews(News News)
        {
            if (News.Id == 0)
            {

                _RNews.Add(News);
            }
            else
            {
                _uow.Entry(News).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public News DetailsNews(int? Id)
        {
            return _RNews.Find(Id);
        }
        public void DeleteNews(News News)
        {
            _RNews.Remove(News);
            _uow.SaveChanges();
        }




        public IQueryable<NewsGallery> NewsGalleries
        {
            get { return _RNewsGallery; }
        }
        public void SaveNewsGallery(NewsGallery NewsGallery)
        {
            if (NewsGallery.Id == 0)
            {
                _RNewsGallery.Add(NewsGallery);
            }
            else
            {
                _uow.Entry(NewsGallery).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public NewsGallery DetailsNewsGallery(int Id)
        {
            return _RNewsGallery.Find(Id);
        }
        public void DeleteNewsGallery(NewsGallery NewsGallery)
        {
            _RNewsGallery.Remove(NewsGallery);
            _uow.SaveChanges();
        }
    }
}
