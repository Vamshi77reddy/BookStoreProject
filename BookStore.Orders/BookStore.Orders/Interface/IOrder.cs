using BookStore.Orders.Entity;
using BookStore.Orders.Model;
using BookStore.Orders.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Orders.Interface
{
    public interface IOrder
    {
        public  Task<OrderEntity> AddOrder(int bookID, int quantity, string token);
        public  Task<OrderEntity> GetOrdersByOrderID(string orderID, int userID, string token);
        public bool RemoveOrder(string orderID, int userID);
        public void UpdateDatabase(PayUTransactionResponse payUTransactionResponse);
        public List<OrderEntity> Success(string token, int userID);
        public List<OrderEntity> Failure(string token, int userID);

         public PayUPaymentResponse ParsePayUResponse(PayUTransactionResponse payUTransactionResponse);


    }
}
