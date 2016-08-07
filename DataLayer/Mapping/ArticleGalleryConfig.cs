
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class ArticleGalleryConfig : EntityTypeConfiguration<ArticleGallery>
    {
        public ArticleGalleryConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         this.HasRequired(x => x.Article).WithMany(x => x.ArticleGallery).HasForeignKey(x => x.ArticleId).WillCascadeOnDelete(true);
        }
    }
}
