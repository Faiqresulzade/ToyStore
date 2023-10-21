using Microsoft.AspNetCore.Mvc;

namespace Toys.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
