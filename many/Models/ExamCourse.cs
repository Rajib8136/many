using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Models
{
    public class ExamCourse
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
