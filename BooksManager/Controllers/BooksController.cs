using BooksManager.Data;
using BooksManager.Models;
using BooksManager.StoredProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Create()
        {
            ViewBag.AuthorId = GetAuthors();
            ViewBag.PublisherId = GetPublishers();
            ViewBag.CategoryId = GetCategories();
            return View();
        }

        private List<SelectListItem> GetCategories()
        {
            List<SelectListItem> listCategories = new List<SelectListItem>();
            IEnumerable<Category> categories = _context.categories;
            foreach (var category in categories)
            {
                listCategories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }

            return listCategories;
        }

        private List<SelectListItem> GetPublishers()
        {
            List<SelectListItem> listPublisher = new List<SelectListItem>();
            IEnumerable<Publisher> publishers = _context.publisher;
            foreach (Publisher publisher in publishers)
            {
                listPublisher.Add(new SelectListItem
                {
                    Value = publisher.Id.ToString(),
                    Text = publisher.Name,
                });
            }

            return listPublisher;
        }

        private List<SelectListItem> GetAuthors()
        {
            List<SelectListItem> listAuthors = new List<SelectListItem>();
            IEnumerable<Author> authors = _context.author;
            foreach (Author author in authors)
            {
                listAuthors.Add(new SelectListItem
                {
                    Value = author.Id.ToString(),
                    Text = $"{author.Name} {author.LastName}",
                });
            }

            return listAuthors;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if(ModelState.IsValid)
            {
                //
                spBook.create(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
    }
}
