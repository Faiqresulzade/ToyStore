using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toys.DAL;
using Toys.ViewModels.Home;

namespace Toys.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> GetData()
        {
            HomeInexVM model = new HomeInexVM()
            {
                GetHomeHeader = await _appDbContext.HomeHeaders.FirstOrDefaultAsync()
            };
            return View(model);
        }
    }
}