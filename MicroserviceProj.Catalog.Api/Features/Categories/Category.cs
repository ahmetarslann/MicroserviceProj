using MicroserviceProj.Catalog.Api.Features.Courses;
using MicroserviceProj.Catalog.Api.Repositories;

namespace MicroserviceProj.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
