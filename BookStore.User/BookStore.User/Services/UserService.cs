using BookStore.User.Context;
using BookStore.User.Interface;
using BookStore.User.Interfaces;
using BookStore.User.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

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

       public UserEntity Login(UserLoginModel loginModel)
        {
            var result=context.Users.FirstOrDefault(x=>x.Email== loginModel.Email&&x.Password==loginModel.Password);
            if (result != null)
            {
               return result;
            }
            else
            {
                return null;
            }

        }
            
        

    }
}
