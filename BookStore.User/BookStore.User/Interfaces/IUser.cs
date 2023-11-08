using BookStore.User.Interface;
using BookStore.User.Model;
using System.Collections.Generic;

namespace BookStore.User.Interfaces
{
    public interface IUser
    {
        public UserEntity Registation(UserModel model);
        public string Login(UserLoginModel loginModel);


    }
}
