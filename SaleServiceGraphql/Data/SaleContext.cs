using Microsoft.EntityFrameworkCore;
using SaleServiceGraphql.Models;

namespace SaleServiceGraphql.Data
{
    public class SaleContext : DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
