using CommonLayer.Model;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRl : IUserRl
    {
        private readonly ContextFundoo contextFundoo;
        private readonly IConfiguration configuration;
        public UserRl(ContextFundoo contextFundoo , IConfiguration configuration)
        {
            this.contextFundoo = contextFundoo;
            this.configuration = configuration;
            
        }

        public UserEntity Registration(RegistrationModel registrationModel)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = registrationModel.FirstName;
            userEntity.LastName = registrationModel.LastName;
            userEntity.EmailId = registrationModel.EmailId;
            userEntity.Password = registrationModel.Password;
            contextFundoo.Add(userEntity);
            contextFundoo.SaveChanges();
            return userEntity;
        }

        public string Login(LoginModel loginModel)
        {
            UserEntity userEntity = new UserEntity();

            userEntity = contextFundoo.Users.FirstOrDefault(x => x.EmailId == loginModel.EmailId && x.Password == loginModel.Password);
            if (userEntity != null)
            {
                var token = GenerateJwtToken(userEntity.EmailId, userEntity.UserId);
                return token;
            }
            else
            {
                return null;
            }
        }

        public string GenerateJwtToken(string EmailId, long UserId)
        {
            try
            {
                var LoginTokenHandler = new JwtSecurityTokenHandler();
                var LoginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("Jwt:Key")]));
                var LoginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Email", EmailId.ToString()),
                        new Claim("UserId", UserId.ToString()),
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

        public ForgetPasswordModel ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            var result=contextFundoo.Users.FirstOrDefault(x=>x.EmailId == forgetPasswordModel.EmailId);
            if(result != null)
            {
                forgetPasswordModel.Token = GenerateJwtToken(result.EmailId, result.UserId);
                return forgetPasswordModel;
            }else
            {
                return null;
            }
        }
        
        public ResetPasswordModel ResetPassword(string email,ResetPasswordModel resetPasswordModel)
        {
            var result=contextFundoo.Users.First(x=>x.EmailId==email);
            result.Password = resetPasswordModel.ConfirmPassword;
            contextFundoo.SaveChanges();
            return resetPasswordModel;

        }

        public List<UserEntity> getAllusers()
        {
            var userentity=contextFundoo.Users.ToList();
            if(userentity.Count > 0)
            {
                return userentity; 
            }else { return null; }
        }


    }
}