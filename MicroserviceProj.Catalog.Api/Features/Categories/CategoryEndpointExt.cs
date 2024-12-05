using MicroserviceProj.Catalog.Api.Features.Categories.Create;
using MicroserviceProj.Catalog.Api.Features.Categories.GetAll;
using MicroserviceProj.Catalog.Api.Features.Categories.GetById;

namespace MicroserviceProj.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").WithTags("Categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetCategoryByIdGroupItemEndpoint();
        }
    }
}
