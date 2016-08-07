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
    public class EFStoryRepository : IStoryRepository
    {
        IUnitOfWork _uow;
        IDbSet<Story> _RStory;
        IDbSet<StoryGallery> _RStoryGallery;
        public EFStoryRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RStoryGallery = _uow.Set<StoryGallery>();
            _RStory = _uow.Set<Story>();
        }
        public IQueryable<Story> Stories
        {
            get { return _RStory; }
        }
        public void SaveStory(Story Story)
        {
            if (Story.Id == 0)
            {

                _RStory.Add(Story);
            }
            else
            {
                _uow.Entry(Story).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public Story DetailsStory(int? Id)
        {
            return _RStory.Find(Id);
        }
        public void DeleteStory(Story Story)
        {
            _RStory.Remove(Story);
            _uow.SaveChanges();
        }

        public IQueryable<StoryGallery> StoryGalleries
        {
            get { return _RStoryGallery; }
        }
        public void SaveStoryGallery(StoryGallery StoryGallery)
        {
            if (StoryGallery.Id == 0)
            {
                _RStoryGallery.Add(StoryGallery);
            }
            else
            {
                _uow.Entry(StoryGallery).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public StoryGallery DetailsStoryGallery(int Id)
        {
            return _RStoryGallery.Find(Id);
        }
        public void DeleteStoryGallery(StoryGallery StoryGallery)
        {
            _RStoryGallery.Remove(StoryGallery);
            _uow.SaveChanges();
        }
    }
}
