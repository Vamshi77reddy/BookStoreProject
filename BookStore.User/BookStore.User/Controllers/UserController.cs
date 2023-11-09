using BookStore.User.Interfaces;
using BookStore.User.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.User.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser user;
        public UserController(IUser user)
        {
            this.user = user;
        }


        [HttpPost]
        [Route("UserRegistration")]
        public IActionResult Registration(UserModel model)
        {
            var result = user.Registation(model);
            if (result != null) 
            {
                return Ok(new { success = true, message = "User Registration Successful", data = result });
            }else
            {
                return BadRequest(new {success = false, message = "User Registration UnSuccessful", data = result });
            }
        }

        [HttpPost]
        [Route("UserLogin")]

        public IActionResult UserLogin(UserLoginModel loginModel)
        {
            var result=user.Login(loginModel);
            if (result != null)
            {
                return Ok(new { success = true, message = "User Login Successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "User Login UnSuccessful", data = result });

            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public IActionResult GetUser()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var result = user.GetUserbyId(userId);
            if (result != null)
            {
                return Ok(new { success = true, message = "User Details fetched Successfully", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "User Details fetched UnSuccessful", data = result });

            }

        }
       
    }
}
