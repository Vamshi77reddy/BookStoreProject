using BookStore.Book.Entity;
using BookStore.Orders.Context;
using BookStore.Orders.Entity;
using BookStore.Orders.Interface;
using BookStore.Orders.Model;
using BookStore.User.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            string orderId = GenerateUniqueTransactionID();
            string transactionID = orderId;

            if (user == null || book == null || quantity <= 0)
            {
                return null;
            }

            OrderEntity orderEntity = new OrderEntity
            {
                OrderId = orderId,
                UserID = user.UserID,
                OrderQty = quantity,
                BookID = bookID,
                Book = book,
                User = user,
                
                OrderAmount = (book.DiscountedPrice) * quantity

            };

            orderContext.Orders.Add(orderEntity);
            orderContext.SaveChanges();


            string Key = "7EHTED";
            string Salt = "zlYowqwt8tB07MBwnSQBxnDXustXturV";

            var paymentRequest = CreatePaymentRequest(
                orderEntity.OrderId.ToString(), orderEntity.OrderAmount, transactionID,
                user.FirstName, user.LastName, user.Email, user.Phone, Key, Salt);

            var paymentResponse = await SendPaymentRequestAsync(paymentRequest);

            if (paymentResponse.Success)
            {
                orderEntity.IsSuccess = true;
                paymentResponse.Message = orderEntity.url;
                orderContext.SaveChanges();
            }
            else
            {
                orderEntity.IsSuccess = false;
                orderEntity.url = paymentResponse.Message;

                orderContext.SaveChanges();
            }

            return orderEntity;
        }

        private string GenerateUniqueTransactionID()
        {

            return Guid.NewGuid().ToString();
        }

        private PayUPaymentRequest CreatePaymentRequest(
            string orderID, float orderAmount, string transactionID,
            string firstName, string lastName, string email, string phone,
            string payuKey, string payuSalt)
        {


            var paymentRequest = new PayUPaymentRequest
            {
                TransactionId = orderID,
                OrderID = orderID,
                Amount = orderAmount,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Surl = "https://secure.payu.in/_payment.success",
                Furl = "https://secure.payu.in/_payment.fail",
                MerchantKey = payuKey,
                Hash = "",
            };

            paymentRequest.Hash = GenerateHash(paymentRequest, payuKey, payuSalt, transactionID);

            return paymentRequest;
        }

        private string GenerateHash(PayUPaymentRequest paymentRequest, string key, string salt, string transactionID)
        {
            try
            {
                string hashString =
                   $"{key}|{paymentRequest.TransactionId}|{paymentRequest.Amount}|{"Book"}|{paymentRequest.FirstName}|{paymentRequest.Email}|||||||||||{salt}";


                using (var sha512 = new SHA512CryptoServiceProvider())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(hashString);
                    byte[] hashBytes = sha512.ComputeHash(bytes);

                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        sb.AppendFormat("{0:x2}", b);
                    }

                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating hash: " + ex.Message);
            }
        }

        public async Task<PayUPaymentResponse> SendPaymentRequestAsync(PayUPaymentRequest paymentRequest)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var formData = new Dictionary<string, string>
                {
                    { "key", paymentRequest.MerchantKey },
                    { "txnid", paymentRequest.TransactionId },
                    { "amount", paymentRequest.Amount.ToString() },
                    { "productinfo", "Book" },
                    { "firstname", paymentRequest.FirstName },
                    {"lastname",paymentRequest.LastName },
                    { "email", paymentRequest.Email },
                    { "phone", paymentRequest.Phone },
                    { "surl", paymentRequest.Surl },
                    { "furl", paymentRequest.Furl },
                    { "hash", paymentRequest.Hash }
                };

                    var content = new FormUrlEncodedContent(formData);

                    var response = await httpClient.PostAsync("https://test.payu.in/_payment", content);

                    if (response.IsSuccessStatusCode)
                    {
                        //string responseContent = await response.Content.ReadAsStringAsync();
                        // var paymentResponse = ParsePayUResponse(responseContent);
                        var Presponse = response.RequestMessage.RequestUri.ToString();

                        PayUPaymentResponse payUPaymentResponse = new PayUPaymentResponse();
                        payUPaymentResponse.Message = Presponse;
                         return payUPaymentResponse;
                       // return Redirect(paymentRequest.Surl);

                    }
                    else
                    {
                        return new PayUPaymentResponse { Success = false, Message = "Error in PayU API response" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new PayUPaymentResponse { Success = false, Message = "Exception occurred: " + ex.Message };
            }
        }

        public  PayUPaymentResponse ParsePayUResponse(string responseContent)//stream responseContent  stream reader pass the steram then add read to end async method
        {
            try
            {
                var queryString = HttpUtility.ParseQueryString(responseContent);

                var payuResponse = new PayUTransactionResponse
                {
                    Mihpayid = queryString["mihpayid"],
                    Mode = queryString["mode"],
                    BankCode = queryString["bankCode"],
                    Status = queryString["status"],
                    UnmappedStatus = queryString["unmappedStatus"],
                    Key = queryString["key"],
                    Error = queryString["error"],
                    ErrorMessage = queryString["errorMessage"],
                    BankRefNum = queryString["bankRefNum"],
                    Txnid = queryString["txnid"],
                    Amount = decimal.Parse(queryString["amount"]),
                    ProductInfo = queryString["productInfo"],
                    FirstName = queryString["firstName"],
                    LastName = queryString["lastName"],
                    Email = queryString["email"],
                    Phone = queryString["phone"],
                    Hash = queryString["hash"],
                    PG_TYPE = queryString["PG_TYPE"]
                };

                if (payuResponse == null)
                {
                    return new PayUPaymentResponse { Success = false, Message = "PayU response deserialization failed." };
                }

                var paymentResponse = new PayUPaymentResponse
                {
                    Success = payuResponse.Status.ToLower() == "success",
                    Message = payuResponse.ErrorMessage
                };

                return paymentResponse;
            }
            catch (Exception ex)
            {
                return new PayUPaymentResponse { Success = false, Message = "Error parsing PayU response: " + ex.Message };
            }
        }
        public async Task<OrderEntity> GetOrdersByOrderID(string orderID, int userID, string token)
        {
            OrderEntity orderEntity = orderContext.Orders.Where(x => x.OrderId == orderID && x.UserID == userID).FirstOrDefault();
            if (orderEntity != null)
            {
                orderEntity.Book = await iuser.GetBookDetailsById(Convert.ToInt32(orderEntity.BookID));
                orderEntity.User = await iuser.GetUser(token);
        
                return orderEntity;
            }
            return null;
        }
        

        public bool RemoveOrder(string orderID, int userID)
        {
            OrderEntity orderEntity = orderContext.Orders.Where(x => x.OrderId == orderID && x.UserID == userID).FirstOrDefault();
            if (orderEntity != null)
            {
                orderContext.Orders.Remove(orderEntity);
                orderContext.SaveChanges();

                return true;
            }
            return false;
        }
    }

}
