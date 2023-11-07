using BookStore.Admin.Interface;
using BookStore.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace BookStore.Admin.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdmin iadmin;

        public AdminController(IAdmin iadmin)
        {
            this.iadmin = iadmin;
        }

        [HttpPost]
        [Route("Registration")]

        public IActionResult Registration(AdminModel admin)
        {
            try
            {
                var result = iadmin.Register(admin);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Registration UnSuccessful", data = result });
                }
            }catch (Exception ex) 
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AdminLogin adminLogin)
        {
            try
            {
                var result = iadmin.AdminLogin(adminLogin);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "User Login unsuccessful", data = result });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("Get users")]

        public IActionResult getAllusers()
        {
            var result=iadmin.GetUsers();
            if (result != null)
            {
                return Ok(new { success = true, message = "Getting all Admin", data = result });

            }
            else
            {
                return BadRequest(new { success = false, Message = " unsuccessful", data = result });

            }
        }
    }
}
