using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public List<ExamViewModel> ExamViewModels { get; set; }
    }
}
