using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLayer.Interfaces;

namespace fundooPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBl userBl;
        public UserController(IUserBl userBl)
        {
            this.userBl = userBl;
        }

        [HttpPost]
        [Route("Registration")]

        public IActionResult UserRegistration(RegistrationModel registrationModel)
        {
            var result=userBl.Registration(registrationModel);
            if(result != null)
            {
                return Ok(new {success = true,message="registration Successful",data=result});
            }
            else
            {
                return BadRequest(new { success = false, message = "registration Successful", data = result });
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult Login(LoginModel loginModel)
        {
            var result=userBl.Login(loginModel);
            if(result!=null)
            {
                return Ok(new {success = true,message="Login Success",data=result});
            }
            else
            {
                return BadRequest(new {success = false,message="Login Failed",data=result });
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgerPassword(ForgetPasswordModel forgetPasswordModel)
        {
            var result=userBl.ForgetPassword(forgetPasswordModel);
            if (result!=null)
            {
                return Ok(new {success = true,message="Token Generated", data=result});
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to generate token", data = result });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var email = User.FindFirst("Email").Value;
            var result=userBl.ResetPassword(email, resetPasswordModel);
            if( result!=null)
            {
                return Ok(new {success = true,message="Password reset successful",data= result});
            }
            else
            {
                return BadRequest(new { success = false, message = "Password reset unsuccessful", data = result });
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult getUsers()
        {
            var result=userBl.getAllusers();
            if (result != null)
            {
                return Ok(new { success = true, message = "Getting all users  successful", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Getting all users  successful", data = result });
            }
        }
    }
}
