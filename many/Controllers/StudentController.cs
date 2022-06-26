using many.Data;
using many.Models;
using many.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var student = await _context.Students.Include(c => c.StudentCourses).ThenInclude(c => c.Course).ToListAsync();
            return View(student);
        }
        public async Task<IActionResult> Create()
        {
            var model = new StudentViewModel();
            var Courses = await _context.Courses.ToListAsync();
            var CourseName = new List<CourseViewModel>();
            foreach (var item in Courses)
            {
                CourseName.Add(new CourseViewModel { Id = item.Id, Name = item.Name });
            }
            model.courseViewModels = CourseName;
            //var examss = _context.Exams.ToList();
            //var exam = new List<ExamViewModel>();

            //foreach (var item in examss)
            //{
            //    exam.Add(new ExamViewModel { Id = item.Id, ExamName = item.ExamName, GradeName = item.GradeName });
            //}
            //model.ExamViewModels = exam;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            var Student = new Student() { Name = studentViewModel.Name, Address = studentViewModel.Address };
            var StudentData = _context.Add(Student);
            await _context.SaveChangesAsync();
            foreach (var item in studentViewModel.courseViewModels)
            {
                if (item.IsChecked)
                {
                    var StudentCourse = new StudentCourse() { CourseId = item.Id, StudentId = StudentData.Entity.Id };
                     _context.Add(StudentCourse);
                    await _context.SaveChangesAsync();
                }
            }

            //var exam = new ExamViewModel();
            
            //foreach(var item in studentViewModel.ExamViewModels)
            //{
            //    if(item != null)
            //    {

            //        var examcourses = new ExamCourse() {  ExamId = item.Id, StudentId = StudentData.Entity.Id };
            //        _context.Add(examcourses);
            //        await _context.SaveChangesAsync();
            //    }
            //}

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.Include(x => x.StudentCourses).FirstOrDefaultAsync(c => c.Id == id);
            var model = new StudentViewModel();
            model.Name = student.Name;
            model.Address = student.Address;
            model.Id = student.Id;

            var Courses = _context.Courses.ToList();
            var CourseName = new List<CourseViewModel>();
            foreach (var item in Courses)
            {
                if (student.StudentCourses.Any(c => c.CourseId == item.Id))
                {

                    CourseName.Add(new CourseViewModel() { Id = item.Id, Name = item.Name, IsChecked = true });

                }
                else
                {
                    CourseName.Add(new CourseViewModel() { Id = item.Id, Name = item.Name });
                }
            }
            model.courseViewModels = CourseName;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(StudentViewModel student)
        {
            var Student = _context.Students.Include(c => c.StudentCourses).FirstOrDefault(c => c.Id == student.Id);
            if (Student != null)
            {
                Student.Name = student.Name;
                Student.Address = student.Address;
                _context.Update(Student);
                foreach (var item in student.courseViewModels)
                {
                    if (item.IsChecked)
                    {
                        var StudentCourse = Student.StudentCourses.FirstOrDefault(c => c.CourseId == item.Id);
                        if (StudentCourse == null)
                        {
                            var SutudentCourse = new StudentCourse() { CourseId = item.Id, StudentId = student.Id.Value };
                            _context.Add(SutudentCourse);
                        }
                    }

                    else
                    {
                        var studentCourse = _context.StudentCourses.FirstOrDefault(c => c.CourseId == item.Id);
                        if (studentCourse != null)
                            _context.Remove(studentCourse);

                    }

                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.Include(x => x.StudentCourses).FirstOrDefaultAsync(c => c.Id == id);
            var model = new StudentViewModel();
            model.Name = student.Name;
            model.Address = student.Address;
            //model.Id = student.Id; 
            if (student == null)
            {
                return NotFound();
            }
            var Courses = _context.Courses.ToList();
            var CourseName = new List<CourseViewModel>();
            model.courseViewModels = CourseName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StudentViewModel student)
        {
            var Student = _context.Students.Include(c => c.StudentCourses).FirstOrDefault(c => c.Id == student.Id);
            if (Student != null)

                Student.Name = student.Name;
            Student.Address = student.Address;

            _context.Remove(Student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> GetCourse()
        {
            var Course = await _context.Courses.Include(c => c.StudentCourses).ThenInclude(c => c.Student).ToListAsync();

            return View(Course);
        }

       
        public IActionResult GetStudentList()
        {
            var studentss = new List<StudentViewModel>();
            var student = _context.Students.Include(c => c.StudentCourses).ToList();
            if (student.Count() > 0 )
            {
                foreach (var item in student)
                {
                    var dadd = new StudentViewModel();
                    dadd.courseViewModels = new List<CourseViewModel>();

                    dadd.Name = item.Name;
                    dadd.Address = item.Address;


                    var coursess = _context.Courses.ToList();
                    foreach (var item1 in coursess)
                    {
                       
                        
                            var StudentCourse = item.StudentCourses.FirstOrDefault(c => c.CourseId == item1.Id);
                         if (StudentCourse != null)
                            {
                            
                                var dhh = new CourseViewModel() { Id = item1.Id, Name = item1.Name, IsChecked = true  };
                            dadd.courseViewModels.Add(dhh);
                            }
                        else
                        {
                            var fgh = new CourseViewModel() { Id = item1.Id, Name = item1.Name };
                             dadd.courseViewModels.Add(fgh);


                        }

                    }
                    studentss.Add(dadd);
                    
                }
            }
            return View(studentss);
            
        }

        public async Task<IActionResult> Courseget()
        {
            var students = new List<Student>();
            var stud = _context.Students.Include(c => c.ExamCourses).ToList();
            if (stud.Count() > 0)
            {
                foreach(var item in stud)
                {
                    var dd = new StudentViewModel();
                    dd.Name = item.Name;
                    dd.Address = item.Address;
                    dd.courseViewModels = new List<CourseViewModel>();
                    var courses =  _context.Courses.ToList();
                    foreach(var item2 in courses)
                    {
                        var ExamCoursess = item.ExamCourses.FirstOrDefault(c => c.CourseId == item2.Id);
                        if(ExamCoursess != null)
                        {
                            var ddd = new CourseViewModel() { Id = item2.Id };
                        }

                    }
                }

            }
            var exam = new ExamViewModel();
            await _context.SaveChangesAsync();

            return View();
        }
    }
}
        
                                                            