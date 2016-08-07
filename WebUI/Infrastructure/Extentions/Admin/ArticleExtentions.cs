using Domain.Entities;
using Domain.Validation.Admin;
using Domain.ViewModel.Admin;
using Repository.Abstract;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace WebUI.Infrastructure.Extentions.Admin
{
    public class ArticleExtentions
    {
        IArticleRepository _RArticle;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
        public ArticleExtentions(IArticleRepository ArticleRepository, IUserAccountRepository DefineUserRepository)
        {
            _RArticle = ArticleRepository;
            _RDefineUser = DefineUserRepository;
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }
        public int GetCountArticle( int LanguageId)
        {
            return _RArticle.Article.Where(x=>x.LanguageId==LanguageId).Count() ;
        }
        public IQueryable<Article> GetArticle(int Start, int End, int LanguageId)
        {
            return _RArticle.Article.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
        }
        public IQueryable<ArticleGallery> GetArticleGallery(int ArticleId)
        {
            return _RArticle.ArticleGalleries.Where(x => x.ArticleId == ArticleId).OrderByDescending(x => x.Id);
        }

    }
}