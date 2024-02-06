using Microsoft.EntityFrameworkCore;

namespace Student.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
    }
}
