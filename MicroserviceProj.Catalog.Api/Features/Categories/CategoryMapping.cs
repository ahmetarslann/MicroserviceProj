using AutoMapper;
using MicroserviceProj.Catalog.Api.Features.Categories.Dtos;

namespace MicroserviceProj.Catalog.Api.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
