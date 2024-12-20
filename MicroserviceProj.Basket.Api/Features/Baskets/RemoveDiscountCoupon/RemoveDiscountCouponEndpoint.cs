using MediatR;
using MicroserviceProj.Basket.Api.Consts;
using MicroserviceProj.Basket.Api.Dtos;
using MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Filters;
using MicroserviceProj.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using MicroserviceProj.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceProj.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand:IRequestByServiceResult;

    public class RemoveDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (basketAsString is null)
            {
                return ServiceResult<BasketDto>.Error(HttpStatusCode.NotFound, "Basket not found.", "Basket not found for this user.");
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            basket.ClearDiscount();

            basketAsString = JsonSerializer.Serialize(basket);

            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon", async ([FromBody]RemoveDiscountCouponCommand command, [FromServices]IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult());

            return group;
        }
    }
}
