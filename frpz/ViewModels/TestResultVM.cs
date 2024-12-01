using frpz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frpz.ViewModels
{
    public class TestResultVM
    {
        public ObservableCollection<QuestionResult> QuestionResults { get; set; }

        public TestResultVM()
        {
            QuestionResults = new ObservableCollection<QuestionResult>();
        }

        public TestResultVM(IEnumerable<QuestionResult> results)
        {
            QuestionResults = new ObservableCollection<QuestionResult>(results);
        }
    }
}
