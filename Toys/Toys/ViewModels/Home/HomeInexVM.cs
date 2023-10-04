using Toys.Models;

namespace Toys.ViewModels.Home
{
    public class HomeInexVM
    {
        public HomeHeader GetHomeHeader { get; set; }
        public CreativeApproach GetCreativeApproach { get; set; }
        public List<ToysCategory> GetToysCategory { get; set; }
        public List<Toys.Models.ToysModel> GetToys { get; set; }
    }
}
