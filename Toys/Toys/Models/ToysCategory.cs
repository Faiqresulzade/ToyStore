namespace Toys.Models
{
    public class ToysCategory:BaseModels
    {
        public ToysCategory()
        {
            Toys = new List<ToysModel>();
        }
        public string CategoryTitle { get; set; }
        public string ImgUrl { get; set; }
        public List<ToysModel> Toys { get; set; }
    }
}
