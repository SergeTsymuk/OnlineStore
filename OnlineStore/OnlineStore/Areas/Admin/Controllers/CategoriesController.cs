using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;

namespace OnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly StoreContext context;
        public CategoriesController(StoreContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
