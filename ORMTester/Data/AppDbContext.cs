using Microsoft.EntityFrameworkCore;
using ORMTester.Models;

namespace ORMTester.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Shop> Shops { get; set; }
    }
}
