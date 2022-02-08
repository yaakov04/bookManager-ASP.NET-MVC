using BooksManager.Data;
using BooksManager.Models;
using BooksManager.StoredProcedure;
using BooksManager.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookManagerDBContext _context;
        private spBook spBook;
        private List<SelectListItem> _Autores;
        private List<SelectListItem> _Publishers;
        private List<SelectListItem> _Categories;

        public BooksController(BookManagerDBContext context)
        {
            _context = context;
            spBook = new spBook(_context);
            _Autores = GetAuthors();
            _Publishers = GetPublishers();
            _Categories = GetCategories();
        }
        
        public IActionResult Index()
        {
            IEnumerable<BookQuery> books = spBook.getAll();

            if(books != null && books.Any())
            {
                ViewBag.AuthorId = _Autores;
                ViewBag.PublisherId = _Publishers;
                ViewBag.CategoryId = _Categories;
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
                if(Convert.ToBoolean(spBook.create(book)))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(book);
        }



        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Book book)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    result = "Error",
                    message = "No existe el autor",
                });
            }

            Book oldBook = spBook.getBook(id);

            if (oldBook == null)
            {
                return Json(new
                {
                    result = "Error",
                    message = failed("eliminar")
                });
            }

            if(id != book.Id)
            {
                return Json(new
                {
                    result = "Error",
                    message = failed("actualizar")
                });
            }

            if (ModelState.IsValid)
            {
                if (Convert.ToBoolean(spBook.update(book)))
                {
                    return Json(new
                    {
                        result = "ok",
                        message = Success("fue actualizado")
                    });
                }
            }

            return Json(new
            {
                result = "Error",
                message = failed("actualizar")
            });
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    result = "Error",
                    message = failed("eliminar")
                });
            }

            Book book = spBook.getBook(id);

            if (book == null)
            {
                return Json(new
                {
                    result = "Error",
                    message = failed("eliminar")
                });
            }

            if (Convert.ToBoolean(spBook.delete(id)))
            {
                return Json(new
                {
                    result = "ok",
                    message = Success("fue eliminado")
                });
            }

            return Json(new
            {
                result = "Error",
                message = failed("eliminar")
            });
        }


        public IActionResult GetAuthorNameOfABook(int id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    result = "Error",
                    message = "No se pudo obtener el autor"
                });
            }

            BookQuery book = spBook.getById(id);

            if(book == null)
            {
                return Json(new
                {
                    result = "Error",
                    message = "No se pudo obtener el autor"
                });
            }

            return Json(new
            {
                result = "ok",
                data = book.author,
                message = "Consulta correcta"
            });
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

        private string Success(string action)
        {
            return Notification.Success("El libro", action);
        }

        private string failed(string action)
        {
            return Notification.Failed("el libro", action);
        }


    }
}
