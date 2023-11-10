using BookStore.Orders.Entity;
using System.Threading.Tasks;

namespace BookStore.Orders.Interface
{
    public interface IOrder
    {
        public  Task<OrderEntity> AddOrder(int bookID, int quantity, string token);

    }
}
