using BooksManager.Data;
using BooksManager.Models;
using BooksManager.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookManagerDBContext _context;

        public AuthorsController(BookManagerDBContext context)
        {
            _context = context;
        }

        // GET: Authors
        public IActionResult Index()
        {
            IEnumerable<Author> authors = _context.author;
            return View(authors);
        }

        // GET: Authors/Details
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Author author = _context.author.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);

        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                _context.SaveChanges();
                TempData["success"] = Success("fue agregado");
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Author author = _context.author.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);

        }

        //POST: Authors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExist(author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["success"] = Success("fue editado");
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        // GET: Authors/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Author author = _context.author.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);

        }

        // POST: Authors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Author author = _context.author.Find(id);
            _context.author.Remove(author);
            _context.SaveChanges();
            TempData["success"] = Success("se elimino");
            return RedirectToAction(nameof(Index));
        }


        private bool AuthorExist(int id)
        {
            return _context.author.Any(c => c.Id == id);
        }

        private string Success(string action)
        {
            return Notification.Success("El autor", action);
        }
    }
}
