﻿using MediatR;
using MicroserviceProj.Basket.Api.Consts;
using MicroserviceProj.Basket.Api.Dtos;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProj.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (basketAsString is null)
            {
                return ServiceResult.Error(HttpStatusCode.NotFound, "Basket not found.", "Basket not found for this user.");
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsString = JsonSerializer.Serialize(basket);

            await distributedCache.SetStringAsync(cacheKey, basketAsString, token:cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
