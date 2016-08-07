using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
   public  interface ISitemapItem
    {
       
            string Url { get; }
            //DateTime? LastModified { get; }
            //ChangeFrequency? ChangeFrequency { get; }
            //float? Priority { get; }
    }

   public enum ChangeFrequency
   {
       Never,
       Yearly,
       Monthly,
       Weekly,
       Daily,
       Hourly,
       Always
   }
}
