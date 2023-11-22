using CommonLayer.Model;
using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interfaces
{
    public interface IUserRl
    {
        public UserEntity Registration(RegistrationModel registrtionModel);
        public string Login(LoginModel loginModel);
        public ForgetPasswordModel ForgetPassword(ForgetPasswordModel forgetPasswordModel);
        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPasswordModel);

        public List<UserEntity> getAllusers();

    }
}
