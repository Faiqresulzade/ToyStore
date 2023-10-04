using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Toys.Areas.Matrix_Admin.ViewModels.ToysModel
{
    public class ToysCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile Image{ get; set; } 
        public string? ImageURl { get; set; }
        [Required]
        public int Price { get; set; }
        public DateTime CreateAt { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int ToysCategoryId { get; set; }
        public List<SelectListItem>? Toys_Category { get; set; }
    }
}
