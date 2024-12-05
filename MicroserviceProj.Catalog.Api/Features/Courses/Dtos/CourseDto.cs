using MicroserviceProj.Catalog.Api.Features.Categories.Dtos;

namespace MicroserviceProj.Catalog.Api.Features.Courses.Dtos
{
    public record CourseDto(
        Guid id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        Guid UserId,
        CategoryDto Category,
        FeatureDto Feature);

}
