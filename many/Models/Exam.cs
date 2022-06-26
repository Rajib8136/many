using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Models
{
    public class Exam
    {
        public int Id { get; set; }
         public string ExamName { get; set;}
        public string GradeName { get; set; }
        public ICollection<ExamCourse> ExamCourses { get; set; }
    }
}
