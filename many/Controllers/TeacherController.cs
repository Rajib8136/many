using many.Data;
using many.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace many.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext  _Context ;
        public TeacherController (ApplicationDbContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            var TeacherData = _Context.Teachers.ToList();
            return View(TeacherData);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _Context.Add(teacher);
                await _Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);

        }
    }
}
