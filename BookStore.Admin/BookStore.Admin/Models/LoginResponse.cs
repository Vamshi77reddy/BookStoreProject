using BookStore.Admin.Entities;

namespace BookStore.User.Model
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public AdminEntity Admin { get; set; }
    }
}