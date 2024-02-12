using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toys.Areas.Matrix_Admin.ViewModels.HomeHeaderVM;
using Toys.DAL;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Matrix-Admin")]
    public class HomeHeaderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeHeaderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var model = new HomeHeaderIndexVM()
            {
                GetHomeHeader = await _appDbContext.HomeHeaders.FirstOrDefaultAsync()
            };
            if(model!= null) return View(model);
            return NotFound();
        }

        public async Task<IActionResult>Update(int id)
        {
            var homeHeader = await _appDbContext.HomeHeaders.FindAsync(id);
            if(homeHeader==null) return NotFound();

            HomeHeaderIndexVM model = new HomeHeaderIndexVM()
            {
                GetHomeHeader = homeHeader
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HomeHeaderIndexVM model,int id)
        {
            if (!ModelState.IsValid) return View(model);
            var homeHeader = await _appDbContext.HomeHeaders.FindAsync(id);
            if (homeHeader == null) return NotFound();

            homeHeader.Title = model.GetHomeHeader.Title;
            homeHeader.Text = model.GetHomeHeader.Text;
            homeHeader.ModifiedAt = DateTime.Now;
            _appDbContext.Update(homeHeader);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
