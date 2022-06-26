using many.Data;
using many.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var exam = _context.Exams.ToList();
            return View(exam);
        }
         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));

            }
            return View(exam);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Exam exam = await _context.Exams.FindAsync(id);
            if(exam == null)
            {
                return NotFound();
            }
            return View(exam);


        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
