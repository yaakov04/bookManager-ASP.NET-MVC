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


        //PUT: Authors/Edit
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

           

            if (!AuthorExist(id))
            {
                return Json(new {
                    result = "Error",
                    message = "No existe el autor",
                });
            }

            if (id != author.Id)
            {
                return Json(new {
                    result = "Error",
                    message = failed("actualizar")
                });
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

                return Json(new{
                    result = "ok",
                    message =  Success("fue actualizado")
                });
            }
            
           return Json(new {
               result = "Error",
               message = failed("actualizar")
           });
        }

 

        // POST: Authors/Delete
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

            Author author = _context.author.Find(id);

            if (author == null)
            {
                return Json(new {
                    result = "Error",
                    message = failed("eliminar")
                });
            }
            
            _context.author.Remove(author);
            _context.SaveChanges();
            return Json(new{
                result = "ok",
                message =  Success("fue eliminado")
            });
        }


        private bool AuthorExist(int id)
        {
            return _context.author.Any(c => c.Id == id);
        }

        private string Success(string action)
        {
            return Notification.Success("El autor", action);
        }

        private string failed(string action)
        {
            return Notification.Failed("el autor", action);
        }
    }
}
