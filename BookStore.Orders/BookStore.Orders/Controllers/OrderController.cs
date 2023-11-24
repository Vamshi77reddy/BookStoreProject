using BookStore.Book.Entity;
using BookStore.Orders.Entity;
using BookStore.Orders.Interface;
using BookStore.User.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using BookStore.Orders.Model;
using System.Collections.Generic;
using BookStore.Orders.Services;
using System.IO;
using System.Text;
using System.Web;

namespace BookStore.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUser iuser;
        private readonly IOrder order;
        public OrderController(IUser user, IOrder order)
        {
            this.iuser = user;
            this.order = order;
        }
        [HttpGet]
        [Route("GetBook_Detail")]
        public async Task<IActionResult> GetBookDetails(int bookID)
        {
            BookEntity bookEntity = await iuser.GetBookDetailsById(bookID);
            if (bookEntity != null)
            {
                return Ok(bookEntity);
            }
            else
            {
                return BadRequest("Book is not available");
            }
        }

        [HttpGet]
        [Route("GetUser_Details")]
        public async Task<IActionResult> GetUserDetails()
        {
            string token = Request.Headers["Authorization"].ToString();
            token = token.Substring("Bearer ".Length);

            UserEntity user = await iuser.GetUser(token);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("unable to get user");
        }


        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddOrder(int bookID, int quantity)
        {
            string token = Request.Headers["Authorization"].ToString();
            token = token.Substring("Bearer ".Length);

            OrderEntity orderEntity = await order.AddOrder(bookID, quantity, token);
            if (orderEntity != null)
            {
                return Ok(orderEntity);
            }
            return BadRequest("Unable to place order...");
        }


        //[HttpPost]
        //[Route("ParsePayUResponse")]
        //public async Task<IActionResult> ParsePayUResponse([FromBody] string responseContent)
        //{
        //    if (string.IsNullOrEmpty(responseContent))
        //    {
        //        return BadRequest("Invalid PayU response content.");
        //    }

        //    var payUPaymentResponse =  order.ParsePayUResponse(responseContent);

        //    if (payUPaymentResponse.Success)
        //    {

        //        return Ok(payUPaymentResponse);
        //    }
        //    else
        //    {
        //        return BadRequest(payUPaymentResponse.Message);
        //    }
        //}

        [HttpPost]
        [Route("ParsePayUResponses")]
        public async Task<IActionResult> ParsePayUResponses()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string payUResponse = await reader.ReadToEndAsync();
                    //var data = HttpUtility.ParseQueryString(payUResponse);
                    //PayUTransactionResponse payUTransactionResponse = new PayUTransactionResponse();
                    //payUTransactionResponse.Mihpayid = data["mihpayid"];
                    //payUTransactionResponse.Status = data["status"];
                    //payUTransactionResponse.Txnid= data["txnid"];
                    //payUTransactionResponse.Amount = data["amount"];
                    //payUTransactionResponse.FirstName = data["firstName"];
                    //payUTransactionResponse.LastName = data["lastName"];
                    //payUTransactionResponse.Email = data["email"];
                    //payUTransactionResponse.Phone = data["phone"];
                    var queryString = HttpUtility.ParseQueryString(payUResponse);

                    PayUTransactionResponse payuResponse = new PayUTransactionResponse
                    {
                        Mihpayid = queryString["mihpayid"],
                        Mode = queryString["mode"],
                        Bankcode = queryString["bankCode"],
                        Status = queryString["status"],
                        Unmappedstatus = queryString["unmappedStatus"],
                        Key = queryString["key"],
                        Error = queryString["error"],
                        Error_Message = queryString["errorMessage"],
                        Bank_Ref_Num = queryString["bankRefNum"],
                        Txnid = queryString["txnid"],
                        Amount = queryString["amount"],
                        ProductInfo = queryString["productInfo"],
                        FirstName = queryString["firstName"],
                        LastName = queryString["lastName"],
                        Email = queryString["email"],
                        Phone = queryString["phone"],
                        Hash = queryString["hash"],
                        PG_TYPE = queryString["PG_TYPE"]
                    };
                    if (payUResponse == null)
                    {
                        return BadRequest("Invalid PayU response content.");
                    }


                    var payUPaymentResponse = order.ParsePayUResponse(payuResponse);

                    if (payUPaymentResponse.Success)
                    {
                        order.UpdateDatabase(payuResponse);

                        return Ok(payUPaymentResponse);
                    }
                    else
                    {
                        return BadRequest(payUPaymentResponse.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //[Route("ParsePayUResponse")]
        //public async Task<IActionResult> ParsePayUResponse()
        //{
        //    try
        //    {
        //        using (var streamReader = new StreamReader(Request.Body))
        //        {
        //            var requestBody = await streamReader.ReadToEndAsync();

        //            var payUPaymentResponse = order.ParsePayUResponse(requestBody);

        //            if (payUPaymentResponse.Success)
        //            {
        //                return Ok(payUPaymentResponse);
        //            }
        //            else
        //            {
        //                return BadRequest(payUPaymentResponse.Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[Authorize]
        [HttpGet]
        [Route("GetOrderBy_ID")]
        public async Task<IActionResult> GetOrdersByOrderID(string orderID)
        {
            string token = Request.Headers["Authorization"].ToString();
            token = token.Substring("Bearer ".Length);

            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));
          
            OrderEntity orderEntity = await order.GetOrdersByOrderID(orderID, userID, token);
            if (orderEntity != null)
            { 
                return Ok(orderEntity);
            }
            return BadRequest("Unable to get order by id...");
        }

        [HttpGet]
        [Route("GetSuccessOrders")]
        public IActionResult GetSuccessOrders()
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString();
                token = token.Substring("Bearer ".Length);

                int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

                List<OrderEntity> successfulOrders = order.Success(token, userID);

                return Ok(successfulOrders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting successful orders: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetFailOrders")]
        public IActionResult GetFailOrders()
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString();
                token = token.Substring("Bearer ".Length);

                int userID = Convert.ToInt32(User.FindFirstValue("UserID"));

                List<OrderEntity> successfulOrders = order.Failure(token, userID);

                return Ok(successfulOrders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting successful orders: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("removeOrder")]
        public IActionResult RemoveOrder(string orderID)
        {
            int userID = Convert.ToInt32(User.FindFirstValue("UserID"));
            bool isRemove = order.RemoveOrder(orderID, userID);
            if (isRemove)
            {
                return Ok(isRemove);
            }
            return BadRequest("Unable to get order by id...");
        }

      
        
    }
}
