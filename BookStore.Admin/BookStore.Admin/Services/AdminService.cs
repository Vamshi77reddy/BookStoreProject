using BookStore.Admin.Context;
using BookStore.Admin.Entities;
using BookStore.Admin.Interface;
using BookStore.Admin.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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

        public AdminEntity addAdmin(AdminModel model)
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

        public AdminEntity AdminLogin(AdminModel model)
        {
            AdminEntity adminEntity = new AdminEntity();
            adminEntity=this.adminContext.Admin.FirstOrDefault(x=>x.Email==model.Email&x.Password==model.Password);
            if(adminEntity==null)
            {
                return adminEntity;
            }
            else
            { 
              return null;           
            }
        }
    }
}
