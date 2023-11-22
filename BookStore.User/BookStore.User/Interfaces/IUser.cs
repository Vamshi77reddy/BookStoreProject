using BookStore.User.Interface;
using BookStore.User.Model;
using System.Collections.Generic;

namespace BookStore.User.Interfaces
{
    public interface IUser
    {
        public UserEntity Registation(UserModel model);
        public LoginResponse Login(UserLoginModel loginModel);
        public UserEntity GetUserbyId(long userId);
        public bool Delete(long id);


    }
}
