using BookStore.Admin.Context;
using BookStore.Admin.Entities;
using BookStore.Admin.Interface;
using BookStore.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookStore.Admin.Services
{
    public class AdminService : IAdmin
    {
        private readonly AdminContext adminContext;
        private readonly IConfiguration configuration;

        public AdminService(AdminContext adminContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.adminContext = adminContext;
        }

        public AdminEntity Register(AdminModel model)
        {
            try
            {
                AdminEntity adminEntity = new AdminEntity();
                adminEntity.FirstName = model.FirstName;
                adminEntity.LastName = model.LastName;
                adminEntity.Email = model.Email;
                adminEntity.Password = model.Password;
                adminContext.Add(adminEntity);
                adminContext.SaveChanges();
                return adminEntity;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        //public string GenerateJwtToken(string Email, long AdminID)
        //{ 
        //    try
        //    {
        //        var LoginTokenHandler = new JwtSecurityTokenHandler();
        //        var LoginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("Jwt:Key")]));
        //        var LoginTokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("Email", Email.ToString()),
        //                new Claim("AdminID", AdminID.ToString()),
        //                new Claim(ClaimTypes.Role, "Admin")
        //            }),
        //            Expires = DateTime.UtcNow.AddHours(1),
        //            SigningCredentials = new SigningCredentials(LoginTokenKey, SecurityAlgorithms.HmacSha256Signature),
        //        };
        //        var token = LoginTokenHandler.CreateToken(LoginTokenDescriptor);
        //        return LoginTokenHandler.WriteToken(token);
        //    }
        //    catch (Exception e)
        //    {           
        //        throw e; 
        //    }
        //}

        public string GenerateJwtToken(string Email, long AdminID)
        {
            var claims = new List<Claim>
            {
                new Claim("AdminID", AdminID.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Role,"Admin")
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(1), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string AdminLogin(AdminLogin model)
        {
            try
            {
                AdminEntity adminEntity = new AdminEntity();
                adminEntity = this.adminContext.Admin.FirstOrDefault(x => x.Email == model.Email & x.Password == model.Password);
                if (adminEntity != null)
                {
                    var token = GenerateJwtToken(adminEntity.Email, adminEntity.AdminID);
                    return token;
                }
                else
                {
                    return null;
                }
            }catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}
