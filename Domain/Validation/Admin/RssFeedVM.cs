using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation.Admin
{
    public class RssFeedVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string RssUrl { get; set; }

        public bool IsShown { get; set; }
    }
}
