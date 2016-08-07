//using Domain.Entities;
//using Repository.Abstract;
//using RepositoryLayer.Abstract;
//using System.Linq;

//namespace WebUI.Infrastructure.Extentions.Admin
//{
//    public class NewsExtentions
//    {
//        INewsRepository _RNews;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        public NewsExtentions(INewsRepository NewsRepository, IUserAccountRepository DefineUserRepository)
//        {
//            _RNews = NewsRepository;
//            _RDefineUser = DefineUserRepository;
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }
//        public int GetCountNews( int LanguageId)
//        {
//            return _RNews.News.Where(x=>x.LanguageId==LanguageId).Count() ;
//        }
//        public IQueryable<News> GetNews(int Start, int End, int LanguageId)
//        {
//            return _RNews.News.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
//        }
//        public IQueryable<NewsGallery> GetNewsGallery(int NewsId)
//        {
//            return _RNews.NewsGalleries.Where(x => x.NewsId == NewsId).OrderByDescending(x => x.Id);
//        }
     
//    }
//}