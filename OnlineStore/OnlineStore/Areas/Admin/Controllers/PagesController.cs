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

        // GET RREQUEST FOR / Admin / Pages 
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        // GET RREQUEST FOR / Admin / Pages / Details / ID: 5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // ADMIN / PAGES / CREATE 
        public IActionResult Create() => View();

    }
}
