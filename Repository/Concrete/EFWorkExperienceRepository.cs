using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;




namespace RepositoryLayer.Concrete
{
    public class EFWorkExperienceRepository : IWorkExperienceRepository
    {
        IUnitOfWork _uow;
        IDbSet<WorkExperienceGroup> _RWorkExperienceGroup;
        IDbSet<WorkExperience> _RWorkExperience;
        IDbSet<WorkExperienceGallery> _RWorkExperienceGallery;
        public EFWorkExperienceRepository(IUnitOfWork uow)
        {
            _uow = uow;

            _RWorkExperienceGroup = _uow.Set<WorkExperienceGroup>();
            _RWorkExperience = _uow.Set<WorkExperience>();
            _RWorkExperienceGallery = _uow.Set<WorkExperienceGallery>();
        }


        public IQueryable<WorkExperienceGroup> WorkExperienceGroups
        {
            get { return _RWorkExperienceGroup; }
        }
        public void SaveWorkExperienceGroup(WorkExperienceGroup WorkExperienceGroup)
        {
            if (WorkExperienceGroup.Id == 0)
            {
                _RWorkExperienceGroup.Add(WorkExperienceGroup);
            }
            else
            {
                _uow.Entry(WorkExperienceGroup).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public WorkExperienceGroup DetailsWorkExperienceGroup(int Id)
        {
            return _RWorkExperienceGroup.Find(Id);
        }
        public void DeleteWorkExperienceGroup(WorkExperienceGroup WorkExperienceGroup)
        {
            _RWorkExperienceGroup.Remove(WorkExperienceGroup);
            _uow.SaveChanges();
        }

        public IQueryable<WorkExperience> WorkExperiences
        {
            get { return _RWorkExperience; }
        }
        public void SaveWorkExperience(WorkExperience WorkExperience)
        {
            if (WorkExperience.Id == 0)
            {
                _RWorkExperience.Add(WorkExperience);
            }
            else
            {
                _uow.Entry(WorkExperience).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public WorkExperience DetailsWorkExperience(int Id)
        {
            return _RWorkExperience.Find(Id);
        }
        public void DeleteWorkExperience(WorkExperience WorkExperience)
        {
            _RWorkExperience.Remove(WorkExperience);
            _uow.SaveChanges();
        }


        public IQueryable<WorkExperienceGallery> WorkExperienceGalleries
        {
            get { return _RWorkExperienceGallery; }
        }
        public void SaveWorkExperienceGallery(WorkExperienceGallery WorkExperienceGallery)
        {
            if (WorkExperienceGallery.Id == 0)
            {
                _RWorkExperienceGallery.Add(WorkExperienceGallery);
            }
            else
            {
                _uow.Entry(WorkExperienceGallery).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public WorkExperienceGallery DetailsWorkExperienceGallery(int Id)
        {
            return _RWorkExperienceGallery.Find(Id);
        }
        public void DeleteWorkExperienceGallery(WorkExperienceGallery WorkExperienceGallery)
        {
            _RWorkExperienceGallery.Remove(WorkExperienceGallery);
            _uow.SaveChanges();
        }
    }
}
