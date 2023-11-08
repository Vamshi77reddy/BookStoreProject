using BookStore.Book.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Book.Context
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options) { }

        public DbSet<BookEntity>Book { get; set; }
    }
}
