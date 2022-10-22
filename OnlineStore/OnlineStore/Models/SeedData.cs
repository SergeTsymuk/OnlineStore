using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Data;
using System;
using System.Linq;

namespace OnlineStore.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                if (context.Pages.Any())
                {
                    return;
                }
                context.Pages.AddRange(
                    new Page { Title = "Home", Slug = "main page", Content = "home page", Sorting = 0 },
                    new Page { Title = "About", Slug = "about us", Content = "about us page", Sorting = 100 },
                    new Page { Title = "Services", Slug = "services page", Content = "services page", Sorting = 100 },
                    new Page { Title = "Contact", Slug = "contact", Content = "contact page", Sorting = 100 });
                context.SaveChanges();
            }
        }
    }
}
