using System.ComponentModel.DataAnnotations;

namespace Toys.Areas.Matrix_Admin.ViewModels.HomeHeaderVM
{
    public class HomeHeaderUpdateVM
    {
        [Required,MaxLength(30)]
         public string Title { get; set; }
        [Required,MaxLength(50)]
        public string Text { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
