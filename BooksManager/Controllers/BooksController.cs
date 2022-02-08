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

            if(books != null && books.Any())
            {
                return View(books);
            }

            return NotFound();
        }

        public IActionResult Create()
        {
            ViewBag.AuthorId = GetAuthors();
            ViewBag.PublisherId = GetPublishers();
            ViewBag.CategoryId = GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                //
                if(Convert.ToBoolean(spBook.create(book)))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book book = spBook.getBook(id);

            if(book == null)
            {
                return NotFound();
            }

            ViewBag.AuthorId = GetAuthors();
            ViewBag.PublisherId = GetPublishers();
            ViewBag.CategoryId = GetCategories();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Book book)
        {
            if (ModelState.IsValid)
            {
                //
                if (Convert.ToBoolean(spBook.update(book)))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(book);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book book = spBook.getBook(id);
            
            if(book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (Convert.ToBoolean(spBook.delete(id)))
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
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

       
    }
}
