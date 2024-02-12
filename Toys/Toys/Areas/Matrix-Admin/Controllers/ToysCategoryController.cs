using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toys.Areas.Matrix_Admin.ViewModels.ToysCategory;
using Toys.DAL;
using Toys.Models;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Matrix-Admin")]
    public class ToysCategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ToysCategoryController(AppDbContext appDbContext, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ToysCategoryIndexVM()
            {
                GetToysCategories = await _appDbContext.ToysCategories.ToListAsync()
            };
            if (model == null) return NotFound();
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ToysCategorCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);
            if (!_fileService.IsImage(model.Image))
            {
                ModelState.AddModelError("model.Image", "Yüklənən fayl image formatinda olmalıdır!!");
                return View(model);
            }

            model.ImgUrl = await _fileService.Upload(model.Image, _webHostEnvironment.WebRootPath);


            var toysCategory = new ToysCategory()
            {
                CategoryTitle = model.CategoryTitle,
                CreatedAt = DateTime.Now,
                ImgUrl = model.ImgUrl,
            };

            await _appDbContext.AddAsync(toysCategory);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var toysCategory = await _appDbContext.ToysCategories.FindAsync(id);
            if (toysCategory == null) return NotFound();

            var model = new ToysCategoryUpdateVM()
            {
                ModifiedAt = DateTime.Now,
                CategoryTitle = toysCategory.CategoryTitle,
                ImgUrl = toysCategory.ImgUrl,
            };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(ToysCategoryUpdateVM model, int id)
        {
            if (!ModelState.IsValid) return View(model);

            var toysCategory = await _appDbContext.ToysCategories.FindAsync(id);
            if (toysCategory == null) return NotFound();

            if (model.Image != null)
            {
                if (!_fileService.IsImage(model.Image))
                {
                    ModelState.AddModelError("model.Image", "Yüklənən fayl image formatinda olmalıdır!!");
                    return View(model);
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath, toysCategory.ImgUrl);
                toysCategory.ImgUrl = await _fileService.Upload(model.Image, _webHostEnvironment.WebRootPath);
            }

            toysCategory.ModifiedAt = DateTime.Now;
            toysCategory.CategoryTitle = model.CategoryTitle;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ToysCategory toysCategory = await _appDbContext.ToysCategories.FindAsync(id);

            if (toysCategory == null) return NotFound();

            _fileService.Delete(_webHostEnvironment.WebRootPath, toysCategory.ImgUrl);

            _appDbContext.Remove(toysCategory);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
