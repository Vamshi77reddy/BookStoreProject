using BookStore.Orders.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Orders.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
        public DbSet<OrderEntity> Orders { get; set; }

    }
}
