namespace Toys.Areas.Matrix_Admin.ViewModels.ToysCategory
{
    public class ToysCategoryUpdateVM
    {
        public string CategoryTitle { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
