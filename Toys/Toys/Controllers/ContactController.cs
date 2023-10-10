using Microsoft.AspNetCore.Mvc;
using Toys.DAL;
using Toys.Models;
using Toys.ViewModels.Contact;

namespace Toys.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ContactController(AppDbContext appDbContext)
        {
           _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult>Index(ContactMessageVM model)
        {
            if (String.IsNullOrEmpty(model.Name)||String.IsNullOrEmpty(model.Number) || 
                String.IsNullOrEmpty(model.Email) || String.IsNullOrEmpty(model.Text) ||
                String.IsNullOrEmpty(model.Subject))
            {
                ModelState.AddModelError(String.Empty, "Düzgün daxil edin!!");
                return View(model);
            }
             if(!ModelState.IsValid)return View(model);
            var contact = new Contact()
            {
                CreatedAt = DateTime.Now,
                Email = model.Email,
                Name = model.Name,
                Subject = model.Subject,
                Number = model.Number,
                Text=model.Text
            };

            model.ErrorMessage = "Mesaj uğurla göndərildi";
            await _appDbContext.ContactUs.AddAsync(contact);
            await _appDbContext.SaveChangesAsync();
            return View(model);
        }
    }
}
