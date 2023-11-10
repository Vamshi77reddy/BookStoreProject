using BookStore.Book.Entity;
using BookStore.Orders.Context;
using BookStore.Orders.Entity;
using BookStore.Orders.Interface;
using BookStore.User.Interface;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BookStore.Orders.Services
{
    public class OrderService : IOrder
    {
        private readonly OrderContext orderContext;
        private readonly IConfiguration configuration;
        private readonly IUser iuser;

        public OrderService(OrderContext orderContext, IConfiguration configuration, IUser user)
        {
            this.orderContext = orderContext;
            this.configuration = configuration;
            this.iuser = user;
        }
        public async Task<OrderEntity> AddOrder(int bookID, int quantity, string token)
        {

            BookEntity book = await iuser.GetBookDetailsById(bookID);
            UserEntity user = await iuser.GetUser(token);

            OrderEntity orderEntity = new OrderEntity();
            orderEntity.UserID = user.UserID;
            orderEntity.OrderQty = quantity;
            orderEntity.BookID = bookID;

            orderEntity.Book = book;
            orderEntity.User = user;

            orderEntity.OrderAmount = (book.ActualPrice - book.DiscountedPrice) * quantity;

            if (user.UserID != null && bookID != null && quantity != 0)
            {
                orderContext.Orders.Add(orderEntity);
                orderContext.SaveChanges();
                return orderEntity;
            }
            return null;

        }
    }
}
