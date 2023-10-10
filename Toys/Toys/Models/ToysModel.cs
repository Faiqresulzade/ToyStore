namespace Toys.Models
{
    public class ToysModel:BaseModels
    {
        public int CategoryId { get; set; }
        public string ImageURl { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public ToysCategory Category { get; set; }
    }
}
