using System;

namespace Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int StatusMSG { get; set; }//1=jadid ,2==khande shode ,3=paokh dade shode
        public string  Reader { get; set; }
        public string ReadDate { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public DateTime SaveMessageDate { get; set; }
    }
}
