
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class ServiceGroupConfig : EntityTypeConfiguration<ServiceGroup>
    {
        public ServiceGroupConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.ServiceGroup).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
