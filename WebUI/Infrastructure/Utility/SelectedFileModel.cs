using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileUpload.Web.Models
    {
    public class SelectedFileModel
        {
        public string fileName { get; set; }
        public long fileSize { get; set; }
        public string fileContent { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public int ProductId { get; set; }

        }
    }