using AutoMapper;
using MediatR;
using MicroserviceProj.Basket.Api.Consts;
using MicroserviceProj.Basket.Api.Dtos;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProj.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService, IMapper mapper) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (basketAsString is null)
            {
                return ServiceResult<BasketDto>.Error(HttpStatusCode.NotFound, "Basket not found.", "Basket not found for this user.");
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            if (!basket.BasketItems.Any())
            {
                return ServiceResult<BasketDto>.Error(HttpStatusCode.NotFound, "Basket Item not found.", "Basket Item not found for this basket.");
            }

            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
