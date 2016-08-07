
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Domain.Entities;
namespace DataLayer.Mapping
{
    public class QuestionConfig : EntityTypeConfiguration<Question>
    {
        public QuestionConfig()
        {
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(x => x.Language).WithMany(x => x.Question).HasForeignKey(x => x.LanguageId).WillCascadeOnDelete(false);
        }
    }
}
