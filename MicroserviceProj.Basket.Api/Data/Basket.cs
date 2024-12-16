namespace MicroserviceProj.Basket.Api.Data
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public Basket(Guid userId, List<BasketItem> basketItems)
        {
            UserId = userId;
            BasketItems = basketItems;
        }

        public Basket()
        {
            
        }


        public bool IsAppliedDiscount => DiscountRate is > 0 && string.IsNullOrEmpty(Coupon);
        public decimal TotalPrice => BasketItems.Sum(x => x.Price);
        public decimal? TotalPriceWithAppliedDiscount => !IsAppliedDiscount ? null : BasketItems.Sum(x => x.PriceByApplyDiscountRate);

        public void ApplyNewDiscount(string coupon, float discountRate) 
        {
            foreach (var item in BasketItems) 
            {
                item.PriceByApplyDiscountRate = item.Price * (decimal)(1 - discountRate);
            }
        }

        public void ApplyAvailableDiscount()
        {
            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscountRate = item.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void ClearDiscount()
        {
            DiscountRate = null;
            Coupon = null;
            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscountRate = null;
            }
        }
    }
}
