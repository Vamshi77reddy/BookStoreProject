using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
