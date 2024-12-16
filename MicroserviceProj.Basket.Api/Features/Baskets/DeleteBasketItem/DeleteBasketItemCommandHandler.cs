using MediatR;
using MicroserviceProj.Basket.Api.Consts;
using MicroserviceProj.Basket.Api.Dtos;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace MicroserviceProj.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (basketAsString is null) 
            {
                return ServiceResult.Error(HttpStatusCode.NotFound, "Basket not found.","Basket not found for this user.");
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketItemForDelete = currentBasket!.BasketItems.FirstOrDefault(b => b.Id == request.Id);
            if (basketItemForDelete is null) 
            {
                return ServiceResult.Error(HttpStatusCode.NotFound, "Course not found.", "Course not found for this user.");
            }

            currentBasket.BasketItems.Remove(basketItemForDelete);

            basketAsString = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
