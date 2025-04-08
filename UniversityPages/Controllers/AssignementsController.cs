using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityPages;
using UniversityPages.Data;

namespace UniversityPages.Controllers
{
    public class AssignementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assignements
        public async Task<IActionResult> Index()
        {
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)
            var applicationDbContext = _context.Assignements.Include(s => s.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Assignements/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                //kods ņemts no lekcijas piemēra (StudentCourseController)
                
                return NotFound();
            }
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)

            var assignement = await _context.Assignements
             .Include(s => s.Course)
             .FirstOrDefaultAsync(m => m.Id == id);

            if (assignement == null)
            {
                return NotFound();
            }

            return View(assignement);
        }

        // GET: Assignements/Create
        public IActionResult Create()
        {
            //kods ņemts no lekcijas piemēra (StudentCourseController)
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: Assignements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Deadline,Description,CourseId")] Assignement assignement)
        {


            //if (ModelState.IsValid)
            //{
            _context.Add(assignement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            
            return View(assignement);
        }

        // GET: Assignements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            //kods ņemts no lekcijas piemēra (StudentCourseController)
            if (id == null)
            {
                return NotFound();
            }

            var assignement = await _context.Assignements.FindAsync(id);
            if (assignement == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", assignement.CourseId);

            return View(assignement);
        }

        // POST: Assignements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Deadline,Description,CourseId")] Assignement assignement)
        {
            if (id != assignement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignementExists(assignement.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "ID", "Name", assignement.CourseId);
            return View(assignement);
        }

        // GET: Assignements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignement = await _context.Assignements
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignement == null)
            {
                return NotFound();
            }

            return View(assignement);
        }

        // POST: Assignements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignement = await _context.Assignements.FindAsync(id);
            if (assignement != null)
            {
                //man neļāva izdzēst assignment, ja neizdzēsu saistītās submissions, tādēļ pievienoju kodu, kas izdzēš saistītās submissions
                var relatedSubmissions = _context.Submissions.Where(s => s.AssignementId == id);
                _context.Submissions.RemoveRange(relatedSubmissions);
                _context.Assignements.Remove(assignement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignementExists(int id)
        {
            return _context.Assignements.Any(e => e.Id == id);
        }
    }
}
