using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UniversityPages.Data;
using UniversityPages.Models;
using UniversityPages;

namespace UniversityPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void CreateTestData()
        {
            try
            {
                Student student01 = new("Amelija", "Saba", Gender.Other, "as11234");
                Student student02 = new("George", "SillyMan", Gender.Man, "gs12334");
                Student student03 = new("Dzons", "Burkans", Gender.Man, "jc12224");
                Student student04 = new("Kate", "Abele", Gender.Woman, "kj13334");

                _context.Students.AddRange(student01, student02, student03, student04);

                Teacher teacher01 = new("Kens", "Rabats", Gender.Man, "14.06.2020.");
                Teacher teacher02 = new("Roberts", "Ozolins", Gender.Man, "24.05.2018");

                _context.Teachers.AddRange(teacher01, teacher02);

                Course course01 = new("Fizika", teacher01); // Teacher assignment via constructor
                Course course02 = new("Sports", teacher02);

                _context.Courses.AddRange(course01, course02);

                Assignement assignement01 = new Assignement(new DateOnly(2024, 10, 31), course01, "Finish tasks 1 - 7 in the workbook.");
                Assignement assignement02 = new Assignement(new DateOnly(2024, 11, 20), course02, "Finish tasks 12 - 30 in the workbook.");

                _context.Assignements.AddRange(assignement01, assignement02);

                Submission submission01 = new Submission(assignement01, student01, DateTime.Now, 85);
                Submission submission02 = new Submission(assignement01, student02, DateTime.Now.AddDays(-1), 90);
                Submission submission03 = new Submission(assignement02, student03, DateTime.Now, 75);
                Submission submission04 = new Submission(assignement02, student04, DateTime.Now.AddDays(-2), 80);

                _context.Submissions.AddRange(submission01, submission02, submission03, submission04);

                _context.SaveChanges();

                Console.WriteLine("Test data created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating data: {ex.Message}");
            }

        }


        public void DeleteTestData()
        {
            try
            {
                if (_context.Submissions != null)
                    _context.Submissions.RemoveRange(_context.Submissions);

                if (_context.Assignements != null)
                    _context.Assignements.RemoveRange(_context.Assignements);

                if (_context.Courses != null)
                    _context.Courses.RemoveRange(_context.Courses);

                if (_context.Students != null)
                    _context.Students.RemoveRange(_context.Students);

                if (_context.Teachers != null)
                    _context.Teachers.RemoveRange(_context.Teachers);

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deliting data: {ex.Message}");
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
