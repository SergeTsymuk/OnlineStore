using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly StoreContext _context;
        public PagesController(StoreContext context)
        {
            _context = context;
        }

        // GET  /Admin/Pages 
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in _context.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        // GET   Admin/Pages/Details / ID: 5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await _context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // Create GET
        public IActionResult Create() => View();

        // Create POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title already exist.");
                    return View(page);
                }
                _context.Add(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "New Page has been Successfully added!";

                return RedirectToAction("Index");
            }
            return View(page);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await _context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The Page already exist.");
                    return View(page);
                }
                _context.Update(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "New Page has been edited Successfully!";

                return RedirectToAction("Edit", new { id = page.Id });
            }
            return View(page);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Page page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                TempData["Error"] = "The page doesn't exist";
            }
            else
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The Page has been Successfully Delited!";
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
                Page page = await _context.Pages.FindAsync(item);
                page.Sorting = count;
                _context.Update(page);
                _context.SaveChanges();
                count++;
            }
            return Ok();
        }

    }
}
