using MediatR;
using MicroserviceProj.Basket.Api.Consts;
using MicroserviceProj.Basket.Api.Data;
using MicroserviceProj.Basket.Api.Dtos;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Xml.Linq;

namespace MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            // TODO : UserId value must came from Token. 
            var userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey,cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(
                request.CourseId,
                request.CourseName,
                request.ImageUrl,
                request.Price,
                null);


            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(userId, [newBasketItem]);
            }
            else
            {
                currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

                var exitingBasketItem = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);
                if (exitingBasketItem is not null)
                {
                    currentBasket.BasketItems.Remove(exitingBasketItem);
                    currentBasket.BasketItems.Add(newBasketItem);
                }
                else
                {
                    currentBasket.BasketItems.Add(newBasketItem);
                }
            }

            basketAsString = JsonSerializer.Serialize(currentBasket);

            await distributedCache.SetStringAsync(cacheKey, basketAsString, token:cancellationToken);


            return ServiceResult.SuccessAsNoContent();
        }
    }
}
