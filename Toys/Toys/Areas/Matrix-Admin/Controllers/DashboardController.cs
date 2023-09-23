using Microsoft.AspNetCore.Mvc;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Area("Matrix-Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
