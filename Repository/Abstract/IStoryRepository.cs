using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IStoryRepository
    {
        IQueryable<Story> Stories { get; }
        void SaveStory(Story story);
        Story DetailsStory(int? Id);
        void DeleteStory(Story story);
        IQueryable<StoryGallery> StoryGalleries { get; }
        void SaveStoryGallery(StoryGallery storyGallery);
        StoryGallery DetailsStoryGallery(int Id);
        void DeleteStoryGallery(StoryGallery storyGallery);
    }
}

