using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Admin
{
    public class BestSpeechVM
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public string SpeechText { get; set; }
    }
}
