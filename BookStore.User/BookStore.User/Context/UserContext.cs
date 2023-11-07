using BookStore.User.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.User.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
    }
}
