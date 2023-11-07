using Microsoft.AspNetCore.Mvc;

namespace BookStore.User.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
