

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Setting
    {
        public int Id { get; set; }
        public string AboutUs { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
       
        public string ContactUs { get; set; }
        public string CompanyIntroduce { get; set; }
       

        /// <summary>
        /// معرفی شرکت
        /// </summary>
        public string CompanyHistory { get; set; }

        /// <summary>
        /// بیانیه ماموریت
        /// </summary>
        public string MissionStatement { get; set; }

        ///// <summary>
        ///// چشم انداز
        ///// </summary>
        //public string Perspective { get; set; }

        ///// <summary>
        ///// اساسنامه شرکت
        ///// </summary>
        //public string Provisions { get; set; }

        /// <summary>
        /// گواهینامه ها
        /// </summary>
        public string Certificates { get; set; }

        /// <summary>
        /// تندیس ها
        /// </summary>
        public string Awards { get; set; }

        /// <summary>
        /// خواص گز راجی
        /// </summary>
        public string RajiGazSpecifications { get; set; }

        /// <summary>
        /// حکایت گز راجی
        /// </summary>
        public string RajiStory { get; set; }

        //public string BestSpeechTitle { get; set; }

        public string CompanyIntroductionTitle { get; set; }

        //[MaxLength(64)]
        //public string Menu1Link { get; set; }

        //[MaxLength(64)]
        //public string Menu2Link { get; set; }

        //[MaxLength(64)]
        //public string Menu3Link { get; set; }

        //[MaxLength(64)]
        //public string Menu4Link { get; set; }

        //[MaxLength(64)]
        //public string Menu5Link { get; set; }
    }
}
