using MediatR;
using MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProj.Shared.Filters;
using MicroserviceProj.Shared.Extensions;

namespace MicroserviceProj.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult()).AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();

            return group;
        }
    }
}
