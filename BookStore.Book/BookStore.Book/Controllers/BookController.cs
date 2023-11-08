using Microsoft.AspNetCore.Mvc;

namespace BookStore.Book.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
