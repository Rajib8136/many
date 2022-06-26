using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.ViewModel
{
    public class StudentViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<CourseViewModel> courseViewModels { get; set; }
        public List<ExamViewModel> ExamViewModels { get; set; }
    }
}
