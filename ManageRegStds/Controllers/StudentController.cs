using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageRegStds.Data;
using ManageRegStds.Models;

namespace ManageRegStds.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                        View(await _context.Students.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Student/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Student/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(Student student)
        {
            if (ModelState.IsValid)
            {
                var students = _context.Students
                    .Where(s => s.FullName.Contains(student.FullName) && s.GradeName.Contains(student.GradeName))
                    .ToList();

                if (students.Count == 0)
                {
                    ViewBag.NotFound = true;
                }

                ViewBag.SearchResults = students; // Pass the results to the view

                return View();
            }

            return View();
        }





        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,StudentNumber,GradeName")] Student student)
        {
            if (ModelState.IsValid)
            {
                // Check if the FullName or StudentNumber already exists in the database
                if (_context.Students.Any(s => s.FullName == student.FullName))
                {
                    ModelState.AddModelError("FullName", "The name is already used.");
                }

                if (_context.Students.Any(s => s.StudentNumber == student.StudentNumber))
                {
                    ModelState.AddModelError("StudentNumber", "The number is already used.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,StudentNumber,GradeName")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if the FullName or StudentNumber already exists in the database, excluding the current student being edited
                if (_context.Students.Any(s => s.FullName == student.FullName && s.Id != student.Id))
                {
                    ModelState.AddModelError("FullName", "The name is already used.");
                }

                if (_context.Students.Any(s => s.StudentNumber == student.StudentNumber && s.Id != student.Id))
                {
                    ModelState.AddModelError("StudentNumber", "The number is already used.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(student);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(student.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(student);
        }


        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
