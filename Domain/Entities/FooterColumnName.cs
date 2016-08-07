using System.Collections.Generic;


namespace Domain.Entities
{
    public class FooterColumnName
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public int LanguageId { get; set; }
       public virtual Language Language { get; set; }
       public virtual ICollection<Footer> Footer { get; set; }
    }
}
