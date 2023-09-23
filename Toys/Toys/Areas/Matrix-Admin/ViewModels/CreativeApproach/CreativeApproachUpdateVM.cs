using Microsoft.Build.Framework;

namespace Toys.Areas.Matrix_Admin.ViewModels.CreativeApproach
{
    public class CreativeApproachUpdateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public IFormFile? Image1 { get; set; }
        public string? Image1PhotoPasss { get; set; }
     
        public IFormFile? Image2 { get; set; }
        public string? Image2PhotoPasss { get; set; }
    }
}
