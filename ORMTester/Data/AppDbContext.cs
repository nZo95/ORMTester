using Microsoft.EntityFrameworkCore;
using ORMTester.Models;

namespace ORMTester.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
