using BookStore.Book.Entity;
using BookStore.Book.Model;
using System.Collections.Generic;

namespace BookStore.Book.Interface
{
    public interface IBook
    {
        public BookEntity AddBook(BookModel book);
        public List<BookEntity> GetBooks();
        public BookEntity GetBookbyId(int id);
        public BookEntity updateBook(UpdateModel update, long id);
        public bool Delete(long id);

    }
}
