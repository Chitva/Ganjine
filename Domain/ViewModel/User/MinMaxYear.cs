using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Domain.ViewModel.User
{
    public class MinMaxYear
    {
        public long MinYear { get; set; }
        public long MaxYear { get; set; }
    }
}
