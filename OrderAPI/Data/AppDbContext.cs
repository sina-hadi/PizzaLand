using Microsoft.EntityFrameworkCore;
using OrderAPI.Model;

namespace OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<OrderHeader> OrderHeadrs { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
