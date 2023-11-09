using BookStore.User.Context;
using BookStore.User.Interface;
using BookStore.User.Interfaces;
using BookStore.User.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookStore.User.Services
{
    public class UserService : IUser
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public UserService(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }


        public UserEntity Registation(UserModel model)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.Email = model.Email;
                userEntity.Password = model.Password;
                userEntity.Address = model.Address;
                context.Add(userEntity);
                context.SaveChanges();
                return userEntity;
            }catch(Exception ex)
            {
                throw ex;
            }

        }


        public string GenerateJwtToken(string Email, long UserID)
        {
            try
            {
                var LoginTokenHandler = new JwtSecurityTokenHandler();
                var LoginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("Jwt:Key")]));
                var LoginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Email", Email.ToString()),
                        new Claim("UserID", UserID.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(LoginTokenKey, SecurityAlgorithms.HmacSha256Signature),
                };
                var token = LoginTokenHandler.CreateToken(LoginTokenDescriptor);
                return LoginTokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LoginResponse Login(UserLoginModel loginModel)
        {
            UserEntity userEntity=new UserEntity();
            userEntity = context.Users.FirstOrDefault(x=>x.Email== loginModel.Email&&x.Password==loginModel.Password);
            if (userEntity != null)
            {
                var token = GenerateJwtToken(userEntity.Email, userEntity.UserID);
                var response = new LoginResponse { Token = token, User = userEntity };

                return response;
            }
            else
            {
                return null;
            }

        } 

        public UserEntity GetUserbyId(long usedId)
        {
            var userEntity = context.Users.FirstOrDefault(x => x.UserID == usedId);
            if (userEntity != null) 
            { 
                return userEntity;
            }
            else
            {
                return null;
            }
        }
    }
}
