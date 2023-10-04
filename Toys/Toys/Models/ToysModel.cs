namespace Toys.Models
{
    public class ToysModel:BaseModels
    {
        public int CategoryID { get; set; }
        public string ImageURl { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public ToysCategory Toys_Category { get; set; }
    }
}
