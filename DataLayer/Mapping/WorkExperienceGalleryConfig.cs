using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class WorkExperienceGalleryConfig : EntityTypeConfiguration<WorkExperienceGallery>
    {
        public WorkExperienceGalleryConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.WorkExperience).WithMany(x => x.WorkExperiencesGallery).HasForeignKey(x => x.WorkExperiencesId).WillCascadeOnDelete(true);
        }
    }
}
