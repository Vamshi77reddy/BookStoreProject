using BookStore.Orders.Entity;
using BookStore.Orders.Model;
using System.Threading.Tasks;

namespace BookStore.Orders.Interface
{
    public interface IOrder
    {
        public  Task<OrderEntity> AddOrder(int bookID, int quantity, string token);
        public  Task<OrderEntity> GetOrdersByOrderID(string orderID, int userID, string token);
        public bool RemoveOrder(string orderID, int userID);



    }
}
