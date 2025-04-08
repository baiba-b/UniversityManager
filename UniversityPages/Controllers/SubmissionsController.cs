using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityPages;
using UniversityPages.Data;
using Microsoft.AspNetCore.Authorization;

namespace UniversityPages.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Submissions
        public async Task<IActionResult> Index()
        {
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)
            var applicationDbContext = _context.Submissions.Include(s => s.Assignement).Include(s => s.Student);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Submissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)

            var submission = await _context.Submissions
                .Include(s => s.Assignement)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // GET: Submissions/Create
        [Authorize]

        public IActionResult Create()
        {
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)

            ViewData["AssignementId"] = new SelectList(_context.Assignements, "Id", "Description");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,SubmissionTime,Score,StudentId,AssignementId")] Submission submission)
        {

            _context.Add(submission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(submission);
        }

        // GET: Submissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)

            ViewData["AssignementId"] = new SelectList(_context.Assignements, "Id", "Description", submission.AssignementId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", submission.StudentId);

            return View(submission);
        }

        // POST: Submissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubmissionTime,Score,StudentId,AssignementId")] Submission submission)
        {
            if (id != submission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.Id))
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
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)

            ViewData["AssignmentId"] = new SelectList(_context.Courses, "Id", "Description", submission.AssignementId);
            ViewData["StudedntId"] = new SelectList(_context.Students, "Id", "FullName", submission.StudentId);
            return View(submission);
        }

        // GET: Submissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //kods ņemt no pasniedzējas lekcijas piemēra (StudentCourse)

            var submission = await _context.Submissions
                 .Include(s => s.Assignement)
                 .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission != null)
            {
                _context.Submissions.Remove(submission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submissions.Any(e => e.Id == id);
        }
    }
}
