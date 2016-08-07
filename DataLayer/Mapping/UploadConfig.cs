
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class UploadConfig : EntityTypeConfiguration<Upload>
    {
        public UploadConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.Upload).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
