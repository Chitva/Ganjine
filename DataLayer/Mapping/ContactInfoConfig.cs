
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DataLayer.Mapping
{
    public class ContactInfoConfig : EntityTypeConfiguration<ContactInfo>
    {
        public ContactInfoConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.ContactInfo).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
