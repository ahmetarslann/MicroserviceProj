using MediatR;
using MicroserviceProj.Shared.Extensions;

namespace MicroserviceProj.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
                    (await mediator.Send(new GetBasketQuery())).ToGenericResult());

            return group;
        }
    }
}
