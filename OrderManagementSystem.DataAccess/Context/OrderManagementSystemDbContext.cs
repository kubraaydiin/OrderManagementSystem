
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Entity;

namespace OrderManagementSystem.DataAccess.Context
{
    public class OrderManagementSystemDbContext : DbContext
    {
        public OrderManagementSystemDbContext(DbContextOptions<OrderManagementSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
