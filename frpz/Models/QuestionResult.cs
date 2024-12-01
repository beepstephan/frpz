using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.Models
{
    public class QuestionResult
    {
        public string QuestionText { get; set; }
        public string SelectedAnswerText { get; set; }
        public string CorrectAnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
