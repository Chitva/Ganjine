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
    public class ContactUsConfig : EntityTypeConfiguration<ContactUs>
    {
        public ContactUsConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.ContactUs).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
