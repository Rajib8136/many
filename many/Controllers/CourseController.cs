using many.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using many.Models;

namespace many.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Course = _context.Courses.ToList();

            return View(Course);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
  
    }
}
