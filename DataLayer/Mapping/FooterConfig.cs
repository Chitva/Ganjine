using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class FooterConfig : EntityTypeConfiguration<Footer>
    {
        public FooterConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.FooterColumnName).WithMany(x => x.Footer)
                .HasForeignKey(x => x.FooterColumnNameId).WillCascadeOnDelete(true);
        }
    }
}
