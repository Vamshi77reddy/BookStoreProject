using BookStore.Admin.Interface;
using BookStore.Admin.Models;
using Microsoft.AspNetCore.Authorization;
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
                    return Ok(new { success = true, message = "Admin Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Admin Registration UnSuccessful", data = result });
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
                    return Ok(new { success = true, message = "Admin Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Admin Login unsuccessful", data = result });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

      
    }
}
