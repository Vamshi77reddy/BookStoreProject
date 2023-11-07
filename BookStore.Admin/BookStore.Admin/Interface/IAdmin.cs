﻿using BookStore.Admin.Entities;
using BookStore.Admin.Models;
using System.Collections.Generic;

namespace BookStore.Admin.Interface
{
    public interface IAdmin
    {
        public AdminEntity Register(AdminModel model);

        public string AdminLogin(AdminLogin model);
        public List<AdminEntity> GetUsers();


    }
}
