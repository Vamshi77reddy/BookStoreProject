using BookStore.Orders.Entity;
using System.Threading.Tasks;

namespace BookStore.Orders.Interface
{
    public interface IOrder
    {
        public  Task<OrderEntity> AddOrder(int bookID, int quantity, string token);
        public  Task<OrderEntity> GetOrdersByOrderID(int orderID, int userID, string token);
        public bool RemoveOrder(int orderID, int userID);



    }
}
