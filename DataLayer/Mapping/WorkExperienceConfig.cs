
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class WorkExperienceConfig : EntityTypeConfiguration<WorkExperience>
    {
        public WorkExperienceConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
            this.HasRequired(x => x.WorkExperienceGroup).WithMany(x => x.WorkExperience).HasForeignKey(x => x.WorkExperienceGroupId).WillCascadeOnDelete(true);
        }
    }
}
