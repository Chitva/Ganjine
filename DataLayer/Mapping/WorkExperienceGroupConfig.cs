using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class WorkExperienceGroupConfig : EntityTypeConfiguration<WorkExperienceGroup>
    {
        public WorkExperienceGroupConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         
            this.HasRequired(x => x.Language).WithMany(x => x.WorkExperienceGroup).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
