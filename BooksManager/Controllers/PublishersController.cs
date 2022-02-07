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

        //PUT: Publishers/Edit
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Publisher publisher)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }



            if (!PublisherExist(id))
            {
                return Json(new
                {
                    result = "Error",
                    message = "No existe el autor",
                });
            }

            if (id != publisher.Id)
            {
                return Json(new
                {
                    result = "Error",
                    message = failed("actualizar")
                });
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

                return Json(new
                {
                    result = "ok",
                    message = Success("fue actualizado")
                });
            }

            return Json(new
            {
                result = "Error",
                message = failed("actualizar")
            });
        }

        // POST: Publishers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == null || id == 0)
            {
                return Json(new {
                    result = "Error",
                    message = failed("eliminar")
                });
            }

            Publisher publisher = _context.publisher.Find(id);

            if (publisher == null)
            {
                return Json(new {
                    result = "Error",
                    message = failed("eliminar")
                });
            }
            
            _context.publisher.Remove(publisher);
            _context.SaveChanges();
            return Json(new{
                result = "ok",
                message =  Success("fue eliminado")
            });
        }


        private bool PublisherExist(int id)
        {
            return _context.publisher.Any(c => c.Id == id);
        }

        private string Success(string action)
        {
            return Notification.Success("La editorial", action);
        }

        private string failed(string action)
        {
            return Notification.Failed("la editorial", action);
        }
    }
}
