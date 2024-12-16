using MicroserviceProj.Shared;

namespace MicroserviceProj.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id):IRequestByServiceResult;
}
