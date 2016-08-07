

namespace Domain.Entities
{
   public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public int? Action { get; set; }
        public string icon { get; set; }
        
    }
}
