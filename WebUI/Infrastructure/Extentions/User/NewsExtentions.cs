using Domain.Entities;
using RepositoryLayer.Abstract;
using System.Linq;

namespace WebUI.Infrastructure.Extentions.User
{
    public class NewsExtentions
    {
        INewsRepository _RNews;
        public NewsExtentions(INewsRepository NewsRepository)
        {
            _RNews = NewsRepository;
        }
        public IQueryable<News> GetNews(int Start, int End,int LanguageId)
        {
            return _RNews.News.Where(x => x.LanguageId == LanguageId && x.IsShow == true).OrderByDescending(x => x.Id).Skip(Start).Take(End);
        }
        public int GetCountNews(int LanguageId)
        {
            return _RNews.News.Where(x => x.LanguageId == LanguageId && x.IsShow == true).Count();
        }
    }
}