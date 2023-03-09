using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public StoreContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-890Q20G; Initial Catalog=OnlineStore; Integrated Security = True;");
        }
    }
}
