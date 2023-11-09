using BookStore.Admin.Entities;
using BookStore.Admin.Models;
using BookStore.User.Model;
using System.Collections.Generic;

namespace BookStore.Admin.Interface
{
    public interface IAdmin
    {
        public AdminEntity Register(AdminModel model);

        public LoginResponse AdminLogin(AdminLogin model);


    }
}
