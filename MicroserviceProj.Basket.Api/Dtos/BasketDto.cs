using System.Text.Json.Serialization;

namespace MicroserviceProj.Basket.Api.Dtos
{
    public record BasketDto(Guid UserId, List<BasketItemDto> BasketItems)
    {
    }
}
