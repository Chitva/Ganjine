
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class ServiceTabConfig : EntityTypeConfiguration<ServiceTab>
    {
        public ServiceTabConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.ServiceGroup).WithMany(x => x.ServiceTab).HasForeignKey(x => x.ServiceGroupId).WillCascadeOnDelete(true);
        }
    }
}
