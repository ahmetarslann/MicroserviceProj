using AutoMapper;
using MediatR;
using MicroserviceProj.Catalog.Api.Features.Categories.Dtos;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Extensions;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;

    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper)
        : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FindAsync(request.Id,cancellationToken);
            if (hasCategory == null) 
            {
                return ServiceResult<CategoryDto>.Error(HttpStatusCode.NotFound,"Category not found",
                    $"Category with id ${request.Id} was not found");
            }

            var categoryDto = mapper.Map<CategoryDto>(hasCategory);

            return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());

            return group;
        }
    }
}
