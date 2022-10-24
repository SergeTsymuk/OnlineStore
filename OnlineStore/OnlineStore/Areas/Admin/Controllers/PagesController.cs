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
        private readonly StoreContext context;
        public PagesController(StoreContext context)
        {
            this.context = context;
        }

        // GET  /Admin/Pages 
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        // GET   Admin/Pages/Details / ID: 5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // GET    -- ADMIN/PAGES/CREATE 
        public IActionResult Create() => View();

        // POST RREQUEST FOR:  Admin/Pages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title already exist.");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "New Page has been Successfully added!";

                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET   Admin/Pages/Edit / ID: 5
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "main page" : page.Title.ToLower().Replace(" ", "-");

                var slug = await context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The Page already exist.");
                    return View(page);
                }
                context.Update(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "New Page has been Successfully edited!";

                return RedirectToAction("Edit", new { id = page.Id });
            }
            return View(page);
        }

    }
}
