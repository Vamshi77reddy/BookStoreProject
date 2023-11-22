using System.Security.Cryptography;
using System.Text;
using System;

namespace BookStore.Orders.Model
{
    public class PayUPaymentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Amount { get; set; }
        public string TransactionId { get; set; }
        public string OrderID { get; set; }
        public string Surl { get; set; } 
        public string Furl { get; set; } 
        public string MerchantKey { get; set; }
        public string Hash { get; set; } 
        public string ProductInfo { get; set; }


        }
        
}


