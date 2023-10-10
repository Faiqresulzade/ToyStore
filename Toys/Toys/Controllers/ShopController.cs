using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toys.DAL;
using Toys.Models;
using Toys.ViewModels.Shop;

namespace Toys.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ShopController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ViewResult> Index(ShopIndexVM model)
        
        {
            var toys = FilterByPrice(model.MaxValue, model.MinValue);
            model = new ShopIndexVM()
            {
                Toys = await toys.Include(c => c.Category).OrderByDescending(t=>t.Id).Take(3).ToListAsync(),
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LoadMore(int count)
        {
            var toys = await _appDbContext.Toys.OrderByDescending(t=>t.Id).Skip(3*count).Take(3).ToListAsync();

            return PartialView("_ShopPartial",toys);
        }

        private IQueryable<ToysModel> FilterByPrice(int? maxValue, int? minValue)
        {
            return _appDbContext.Toys.Where(t => (maxValue != null ? t.Price <= maxValue : true) && (minValue != null ? t.Price >= minValue : true));
        }
    }
}
