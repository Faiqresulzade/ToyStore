using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toys.Areas.Matrix_Admin.ViewModels.Contact;
using Toys.DAL;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Area("Matrix-admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ContactController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var model = new ContactIndexVM()
            {
                Message = await _appDbContext.ContactUs.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var message = await _appDbContext.ContactUs.FindAsync(id);
            if (message == null) return BadRequest();

             _appDbContext.Remove(message);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
