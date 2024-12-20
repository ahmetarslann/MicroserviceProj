using System.Text.Json.Serialization;

namespace MicroserviceProj.Basket.Api.Dtos
{
    public record BasketDto
    {
        public List<BasketItemDto> BasketItems { get; set; } = new();

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        [JsonIgnore]
        public bool IsAppliedDiscount => DiscountRate is > 0 && string.IsNullOrEmpty(Coupon);

        public decimal TotalPrice => BasketItems.Sum(x => x.Price);

        public decimal? TotalPriceWithAppliedDiscount => !IsAppliedDiscount ? null : BasketItems.Sum(x => x.PriceByApplyDiscountRate);

        public BasketDto(List<BasketItemDto> basketItems) 
        {
            BasketItems = basketItems;
        }

        public BasketDto()
        {

        }
    }
}
