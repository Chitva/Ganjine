using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IArticleRepository
    {
        IQueryable<Article> Article { get; }
        void SaveArticle(Article Article);
        Article DetailsArticle(int? Id);
        void DeleteArticle(Article Article);
        IQueryable<ArticleGallery> ArticleGalleries { get; }
        void SaveArticleGallery(ArticleGallery ArticleGallery);
        ArticleGallery DetailsArticleGallery(int Id);
        void DeleteArticleGallery(ArticleGallery ArticleGallery);
    }
}

