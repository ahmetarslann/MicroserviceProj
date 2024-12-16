using MicroserviceProj.Shared;

namespace MicroserviceProj.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(
        Guid CourseId,
        string CourseName,
        decimal Price,
        string? ImageUrl
        ):IRequestByServiceResult;

}
