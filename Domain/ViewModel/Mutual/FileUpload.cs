using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Mutual
{
    public class FileUpload
    {
        public int? id { get; set; }

        public string fileName { get; set; }

        public string url { get; set; }

        public string thumbnail { get; set; }

        public string title { get; set; }

        public string description { get; set; }
    }
}
