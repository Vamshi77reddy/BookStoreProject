using BookStore.User.Interface;

namespace BookStore.User.Model
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserEntity User { get; set; }
    }
}