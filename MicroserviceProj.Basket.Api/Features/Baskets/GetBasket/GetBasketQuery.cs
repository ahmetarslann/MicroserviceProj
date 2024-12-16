using MicroserviceProj.Basket.Api.Dtos;
using MicroserviceProj.Shared;

namespace MicroserviceProj.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery:IRequestByServiceResult<BasketDto>;

}
