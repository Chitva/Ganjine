
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class ServiceTabFileConfig : EntityTypeConfiguration<ServiceTabFile>
    {
        public ServiceTabFileConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.ServiceTab).WithMany(x => x.ServiceTabFile).HasForeignKey(x => x.ServiceTabId).WillCascadeOnDelete(true);
            
        }
    }
}
