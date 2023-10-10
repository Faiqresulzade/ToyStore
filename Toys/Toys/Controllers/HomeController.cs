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

        public async Task<IActionResult> Index()
        {
            HomeInexVM model = new HomeInexVM()
            {
                GetHomeHeader = await _appDbContext.HomeHeaders.FirstOrDefaultAsync(),
                GetCreativeApproach = await _appDbContext.CreativeApproaches.FirstOrDefaultAsync(),
                GetToysCategory = await _appDbContext.ToysCategories.ToListAsync(),
                GetToys = await _appDbContext.Toys.ToListAsync(),
            };
            return View(model);
        }
    }
}