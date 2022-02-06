using BooksManager.Data;
using BooksManager.Models;
using BooksManager.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksManager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BookManagerDBContext _context;

        public CategoriesController(BookManagerDBContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.categories;
            return View(categories);
        }

       

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
                TempData["success"] = Success("fue creada");
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _context.categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);

        }

        //POST: Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExist(category.Id))
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

        // POST: Categories/Delete
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

            Category category = _context.categories.Find(id);

            if (category == null)
            {
                return Json(new {
                    result = "Error",
                    message = failed("eliminar")
                });
            }
            
            _context.categories.Remove(category);
            _context.SaveChanges();
            return Json(new{
                result = "ok",
                message =  Success("fue eliminada")
            });
        }


        private bool CategoryExist(int id)
        {
            return _context.categories.Any(c => c.Id == id);
        }

        private string Success(string action)
        {
            return Notification.Success("La categoría", action);
        }

        private string failed(string action)
        {
            return Notification.Failed("la categoría", action);
        }


    }
}
