using BookStore.Book.Entity;
using BookStore.User.Interface;
using System.Threading.Tasks;

namespace BookStore.Orders.Interface
{
    public interface IUser
    {
        public   Task<BookEntity> GetBookDetailsById(int bookId);
        public Task<UserEntity> GetUser(string token);


    }
}
