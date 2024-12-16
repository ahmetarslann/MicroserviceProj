using MicroserviceProj.Shared;

namespace MicroserviceProj.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate):IRequestByServiceResult;
}
