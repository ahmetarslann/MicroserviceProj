using MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProj.Basket.Api.Features.Baskets.DeleteBasketItem;
using MicroserviceProj.Basket.Api.Features.Baskets.GetBasket;

namespace MicroserviceProj.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/baskets").WithTags("Baskets")
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint();
        }
    }
}
