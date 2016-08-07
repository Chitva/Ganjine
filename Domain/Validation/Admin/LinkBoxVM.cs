using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation.Admin
{
    public class LinkBoxVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsShown { get; set; }

    }
}
