using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class BannerConfig : EntityTypeConfiguration<Banner>
    {
        public BannerConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.Banner).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
