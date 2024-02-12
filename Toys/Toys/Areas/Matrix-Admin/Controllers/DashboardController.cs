using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Matrix-Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
