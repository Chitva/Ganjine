using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class NewsGalleryConfig : EntityTypeConfiguration<NewsGallery>
    {
        public NewsGalleryConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         this.HasRequired(x => x.News).WithMany(x => x.NewsGallery).HasForeignKey(x => x.NewsId).WillCascadeOnDelete(true);
        }
    }
}
