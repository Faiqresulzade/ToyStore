using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toys.Areas.Matrix_Admin.ViewModels.ToysModel;
using Toys.DAL;
using Toys.Models;

namespace Toys.Areas.Matrix_Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Matrix-admin")]
    public class ToysController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ToysController(AppDbContext appDbContext, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var model = new ToysIndexVM()
            {
                GetToys = await _appDbContext.Toys.Include(toys => toys.Category).ToListAsync()

            };
            return model != null ? View(model) : NotFound();
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var model = new ToysCreateVM()
            {
                Toys_Category = await _appDbContext.ToysCategories.Select(c => new SelectListItem
                {
                    Text = c.CategoryTitle,
                    Value = c.Id.ToString()
                }).ToListAsync()
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToysCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Toys_Category = await _appDbContext.ToysCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryTitle
            }).ToListAsync();

            if (model.Image != null)
            {
                if (!_fileService.IsImage(model.Image))
                {
                    ModelState.AddModelError("model.Image", "Yüklənən fayl image formatında olmalıdır!!");
                    return View(model);
                }

                model.ImageURl = await _fileService.Upload(model.Image, _webHostEnvironment.WebRootPath);
            }

            var toys = new ToysModel()
            {
                Price = model.Price,
                Title = model.Title,
                CreatedAt = model.CreateAt,
                CategoryId= model.ToysCategoryId,
                ImageURl = model.ImageURl,
                Description=model.Description
            };

            await _appDbContext.Toys.AddAsync(toys);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>Update(int id)
        {
            var toys = await _appDbContext.Toys.FindAsync(id);

            if (toys == null) return BadRequest();

            var model = new ToysUpdateVM(toys.Title,toys.ImageURl,toys.Price,toys.Description,toys.CategoryId);

            model.Toys_Category = await _appDbContext.ToysCategories.Select(c => new SelectListItem()
            {
                Text = c.CategoryTitle,
                Value = c.Id.ToString()
            }).ToListAsync();

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> Update(ToysUpdateVM model, int id)
        {
            model.Toys_Category = await _appDbContext.ToysCategories.Select(c => new SelectListItem()
            {
                Text = c.CategoryTitle,
                Value = c.Id.ToString()
            }).ToListAsync();

            if (!ModelState.IsValid) return View(model);

            var toys = await _appDbContext.Toys.FindAsync(id);

            if (toys == null) return BadRequest();

            if (model.Image != null)
            {
                if (!_fileService.IsImage(model.Image))
                {
                    ModelState.AddModelError("Image", "Yuklenen shekil Image formatinda olmalidir!!");
                    return View(model);
                }
                toys.ImageURl = await _fileService.Upload(model.Image, _webHostEnvironment.WebRootPath);
            }

            toys.Title = model.Title;
            toys.ModifiedAt = DateTime.Now;
            toys.Price = model.Price;
            toys.Description = model.Description;
            toys.CategoryId = model.ToysCategoryId;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
