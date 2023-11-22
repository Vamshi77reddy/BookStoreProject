using BusinessLayer.Interfaces;
using CommonLayer.Model;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBl : IUserBl
    {
        private readonly IUserRl userRl;

        public UserBl(IUserRl userRl)
        {
            this.userRl = userRl;
        }

        public string Login(LoginModel loginModel)
        {
           return this.userRl.Login(loginModel);
        }

        public UserEntity Registration(RegistrationModel registrtionModel)
        {
            return userRl.Registration(registrtionModel);
        }
        public ForgetPasswordModel ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            return userRl.ForgetPassword(forgetPasswordModel);
        }

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            return userRl.ResetPassword(email, resetPasswordModel);
        }
        public List<UserEntity> getAllusers()
        {
            return userRl.getAllusers();
        }


    }
}
