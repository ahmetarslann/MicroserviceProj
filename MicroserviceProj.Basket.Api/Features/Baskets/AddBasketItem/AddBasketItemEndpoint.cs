using MediatR;
using MicroserviceProj.Shared.Filters;
using MicroserviceProj.Shared.Extensions;

namespace MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult()).AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();

            return group;
        }
    }
}
