using Microsoft.Build.Framework;

namespace Toys.Areas.Matrix_Admin.ViewModels.ToysCategory
{
    public class ToysCategorCreateVM
    {
        [Required]
        public string CategoryTitle { get; set; }
        public string? ImgUrl { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}
