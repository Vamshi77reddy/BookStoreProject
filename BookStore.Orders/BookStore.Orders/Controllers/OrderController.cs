using BookStore.Book.Entity;
using BookStore.Orders.Entity;
using BookStore.Orders.Interface;
using BookStore.User.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

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
