using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<ExamCourse> ExamCourses { get; set; }
    }
}
