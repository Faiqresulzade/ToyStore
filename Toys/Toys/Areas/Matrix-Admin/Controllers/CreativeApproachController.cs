using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toys.Areas.Matrix_Admin.ViewModels.CreativeApproach;
using Toys.DAL;
using Toys.Models;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Matrix-admin")]
    public class CreativeApproachController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreativeApproachController(AppDbContext appDbContext, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {

            CreativeApproachIndexVM model = new CreativeApproachIndexVM()
            {
                GetCreativeApproach = await _appDbContext.CreativeApproaches.FirstOrDefaultAsync()
            };
            if (model != null) return View(model);
            return NotFound();
        }

        public async Task<IActionResult> Update(int id)
        {
            var approach = await _appDbContext.CreativeApproaches.FindAsync(id);
            if (approach == null) return NotFound();

            CreativeApproachUpdateVM model = new CreativeApproachUpdateVM()
            {
                Title = approach.Title,
                Text = approach.Text,
                Image1PhotoPasss = approach.Image1,
                Image2PhotoPasss = approach.Image2
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreativeApproachUpdateVM model, int id)
        {
            if (!ModelState.IsValid) return View(model);
            var approach = await _appDbContext.CreativeApproaches.FindAsync(id);
            if (approach == null) return NotFound();

            if (model.Image1 != null)
            {
                if (!_fileService.IsImage(model.Image1))
                {
                    ModelState.AddModelError("Image1", "Yüklənən şəkil image formatında olmalıdır!!");
                    return View(model);
                }
                if (!_fileService.CheckSize(model.Image1, 260))
                {
                    ModelState.AddModelError("Image1", "Şəkilin ölçüsü 260KB-dan böyükdür!!");
                    return View(model);
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath, approach.Image1);
                approach.Image1 = await _fileService.Upload(model.Image1, _webHostEnvironment.WebRootPath);
            }

            if (model.Image2 != null)
            {
                if (!_fileService.IsImage(model.Image2))
                {
                    ModelState.AddModelError("Image2", "Yüklənən şəkil image formatında olmalıdır!!");
                    return View(model);
                }
                if (!_fileService.CheckSize(model.Image2, 260))
                {
                    ModelState.AddModelError("Image2", "Şəkilin ölçüsü 260KB-dan böyükdür!!");
                    return View(model);
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath, approach.Image2);
                approach.Image2 = await _fileService.Upload(model.Image2, _webHostEnvironment.WebRootPath);
            }

            approach.Text = model.Text;
            approach.Title = model.Title;
            approach.ModifiedAt = DateTime.Now;

            _appDbContext.Update(approach);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
