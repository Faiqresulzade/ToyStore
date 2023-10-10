namespace Toys.ViewModels.Shop
{
    public class ShopIndexVM
    {
        public List<Toys.Models.ToysModel> Toys { get; set; }


        #region filter
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        #endregion

        ///Load-MoreBtn
        ///

        public int? TakeCount { get; set; }

    }
}
