using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Toys.Areas.Matrix_Admin.ViewModels.ToysModel
{
    public class ToysUpdateVM
    {
        [Required]
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageURl { get; set; }
        [Required]
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int ToysCategoryId { get; set; }
        public List<SelectListItem>? Toys_Category { get; set; }

        public ToysUpdateVM()
        {

        }
        
        public ToysUpdateVM(string title,string? imageURl, int price, string description, int toysCategoryId)
        {
            Title=title;
            ImageURl = imageURl;
            Price=price;
            Description=description;
            ModifiedAt = DateTime.Now;
            ToysCategoryId=toysCategoryId;
        }
    }
}
