using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly StoreContext _context;
        public CategoriesController(StoreContext context)
        {
            _context = context;
        }

        // GET Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderBy(c => c.Sorting).ToListAsync());
        }

        // Create GET
        public IActionResult Create() => View();

        // Create POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                var slug = await _context.Categories.FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "This category already exist.");
                    return View(category);
                }
                _context.Add(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "New Page has been Successfully added!";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Pages.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The Category already exist.");
                    return View(category);
                }
                _context.Update(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "New Category has been edited Successfully!";

                return RedirectToAction("Edit", new { id });
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                TempData["Error"] = "The page doesn't exist";
            }
            else
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Category has been Successfully Delited!";
            }
            return RedirectToAction("Index");
        }

        // POST reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var item in id)
            {
                Category category = await _context.Categories.FindAsync(item);
                category.Sorting = count;
                _context.Update(category);
                _context.SaveChanges();
                count++;
            }
            return Ok();
        }
    }
}
