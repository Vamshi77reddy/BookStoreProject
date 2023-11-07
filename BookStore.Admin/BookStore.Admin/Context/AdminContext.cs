using BookStore.Admin.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Admin.Context
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions options) : base(options) { } 
        
        public DbSet<AdminEntity> Admin { get; set; }
    }
}
