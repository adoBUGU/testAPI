using Microsoft.EntityFrameworkCore;
using trackingAPI.Model;

namespace trackingAPI.Data
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions options) : base(options)
        { 
        }
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Item> Items { get; set; }
    }
}
