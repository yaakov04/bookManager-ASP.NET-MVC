using BooksManager.Data;
using BooksManager.Models;
using BooksManager.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Controllers
{
    public class PublishersController : Controller
    {
        private readonly BookManagerDBContext _context;

        public PublishersController(BookManagerDBContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = _context.publisher;
            return View(publishers);
        }

        // GET: Publishers/Details
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Publisher publisher = _context.publisher.Find(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);

        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                _context.SaveChanges();
                TempData["success"] = Success("fue creada");
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Publisher publisher = _context.publisher.Find(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);

        }

        //POST: Publishers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExist(publisher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["success"] = Success("fue editada");
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        // GET: Publishers/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Publisher publisher = _context.publisher.Find(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);

        }

        // POST: Publishers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Publisher publisher = _context.publisher.Find(id);
            _context.publisher.Remove(publisher);
            _context.SaveChanges();
            TempData["success"] = Success("se elimino");
            return RedirectToAction(nameof(Index));
        }


        private bool PublisherExist(int id)
        {
            return _context.publisher.Any(c => c.Id == id);
        }

        private string Success(string action)
        {
            return Notification.Success("La editorial", action);
        }
    }
}
