using BooksManager.Data;
using BooksManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BooksManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookManagerDBContext _context;

        public HomeController(BookManagerDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.BooksCount = _context.books.Count();
            ViewBag.AuthorsCount = _context.author.Count();
            ViewBag.CategoriesCount = _context.categories.Count();
            ViewBag.PublishersCount = _context.publisher.Count();
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}