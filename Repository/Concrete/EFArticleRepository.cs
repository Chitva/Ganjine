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
    public class EFArticleRepository : IArticleRepository
    {
        IUnitOfWork _uow;
        IDbSet<Article> _RArticle;
        IDbSet<ArticleGallery> _RArticleGallery;
        public EFArticleRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RArticleGallery = _uow.Set<ArticleGallery>();
            _RArticle = _uow.Set<Article>();
        }
        public IQueryable<Article> Article
        {
            get { return _RArticle; }
        }
        public void SaveArticle(Article Article)
        {
            if (Article.Id == 0)
            {

                _RArticle.Add(Article);
            }
            else
            {
                _uow.Entry(Article).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public Article DetailsArticle(int? Id)
        {
            return _RArticle.Find(Id);
        }
        public void DeleteArticle(Article Article)
        {
            _RArticle.Remove(Article);
            _uow.SaveChanges();
        }




        public IQueryable<ArticleGallery> ArticleGalleries
        {
            get { return _RArticleGallery; }
        }
        public void SaveArticleGallery(ArticleGallery ArticleGallery)
        {
            if (ArticleGallery.Id == 0)
            {
                _RArticleGallery.Add(ArticleGallery);
            }
            else
            {
                _uow.Entry(ArticleGallery).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public ArticleGallery DetailsArticleGallery(int Id)
        {
            return _RArticleGallery.Find(Id);
        }
        public void DeleteArticleGallery(ArticleGallery ArticleGallery)
        {
            _RArticleGallery.Remove(ArticleGallery);
            _uow.SaveChanges();
        }
    }
}
