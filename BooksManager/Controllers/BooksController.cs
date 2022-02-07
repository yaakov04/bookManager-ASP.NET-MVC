using BooksManager.Data;
using BooksManager.Models;
using BooksManager.StoredProcedure;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookManagerDBContext _context;
        private spBook spBook;

        public BooksController(BookManagerDBContext context)
        {
            _context = context;
            spBook = new spBook(_context);
        }
        public IActionResult Index()
        {
            IEnumerable<BookQuery> books = spBook.getAll();
            return View(books);
        }
    }
}
