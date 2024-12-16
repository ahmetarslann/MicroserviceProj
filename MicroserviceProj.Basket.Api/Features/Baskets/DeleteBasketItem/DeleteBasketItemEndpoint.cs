using MediatR;
using MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem;
using MicroserviceProj.Shared.Filters;
using MicroserviceProj.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceProj.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item", async ([FromBody]DeleteBasketItemCommand command, [FromServices]IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult()).AddEndpointFilter<ValidationFilter<DeleteBasketItemCommandValidator>>();

            return group;
        }
    }
}
