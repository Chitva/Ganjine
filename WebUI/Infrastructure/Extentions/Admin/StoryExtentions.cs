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
    public class StoryExtentions
    {
        IStoryRepository _RStory;
        IUserAccountRepository _RDefineUser;
        UserAccountExtentions EDefineUser;
        public StoryExtentions(IStoryRepository StoryRepository, IUserAccountRepository DefineUserRepository)
        {
            _RStory = StoryRepository;
            _RDefineUser = DefineUserRepository;
            EDefineUser = new UserAccountExtentions(_RDefineUser);
        }
        public int GetCountStory( int LanguageId)
        {
            return _RStory.Stories.Where(x=>x.LanguageId==LanguageId).Count() ;
        }
        public IQueryable<Story> GetStory(int Start, int End, int LanguageId)
        {
            return _RStory.Stories.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
        }
        public IQueryable<StoryGallery> GetStoryGallery(int StoryId)
        {
            return _RStory.StoryGalleries.Where(x => x.StoryId == StoryId).OrderByDescending(x => x.Id);
        }
     
    }
}