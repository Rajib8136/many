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
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ResultController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var student = _context.Students.Include(c => c.ExamCourses).ThenInclude(c=> c.Exam).ToList(); 
            return View(student);
        }
         public async Task<IActionResult> Create()
        {
            var stu = new StudentViewModel();
            var examss =await _context.Exams.ToListAsync();
            var exams = new List<ExamViewModel>();
            foreach(var item in examss)
            {
                exams.Add(new ExamViewModel { Id = item.Id, ExamName = item.ExamName, GradeName = item.GradeName });

            }
            stu.ExamViewModels = exams;
            return View(stu);

        }

        [HttpPost]

        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            var student = new Student() { Name = studentViewModel.Name, Address = studentViewModel.Address };
            var stud = _context.Add(student);
            await _context.SaveChangesAsync();
            var exam = new ExamViewModel();
            foreach(var item in studentViewModel.ExamViewModels)
            {
                if (item != null)
                {
                    var ExamCo = new ExamCourse() { Id = item.Id, StudentId = stud.Entity.Id };
                    _context.Add(ExamCo); 
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
