
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class StoryConfig : EntityTypeConfiguration<Story>
    {
        public StoryConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.Story).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
