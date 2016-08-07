using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class StoryGalleryConfig : EntityTypeConfiguration<StoryGallery>
    {
        public StoryGalleryConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Story).WithMany(x => x.StoryGallery).HasForeignKey(x => x.StoryId).WillCascadeOnDelete(true);
        }
    }
}
