using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IWorkExperienceRepository
    {
        IQueryable<WorkExperienceGroup> WorkExperienceGroups { get; }
        void SaveWorkExperienceGroup(WorkExperienceGroup WorkExperienceGroup);
        WorkExperienceGroup DetailsWorkExperienceGroup(int Id);
        void DeleteWorkExperienceGroup(WorkExperienceGroup WorkExperienceGroup);

        IQueryable<WorkExperience> WorkExperiences { get; }
        void SaveWorkExperience(WorkExperience WorkExperience);
        WorkExperience DetailsWorkExperience(int Id);
        void DeleteWorkExperience(WorkExperience WorkExperience);

        IQueryable<WorkExperienceGallery> WorkExperienceGalleries { get; }
        void SaveWorkExperienceGallery(WorkExperienceGallery WorkExperienceGallery);
        WorkExperienceGallery DetailsWorkExperienceGallery(int Id);
        void DeleteWorkExperienceGallery(WorkExperienceGallery WorkExperienceGallery);


    }
}

