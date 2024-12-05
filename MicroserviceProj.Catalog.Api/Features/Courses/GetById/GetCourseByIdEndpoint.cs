using AutoMapper;
using MediatR;
using MicroserviceProj.Catalog.Api.Features.Categories.Dtos;
using MicroserviceProj.Catalog.Api.Features.Categories.GetById;
using MicroserviceProj.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Courses.GetById
{

    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (hasCourse is null) 
            {
                return ServiceResult<CourseDto>.Error(HttpStatusCode.NotFound, "Course not found",
                    $"Course with id ${request.Id} was not found");
            }

            var category = (await context.Categories.FindAsync(hasCourse.CategoryId, cancellationToken));

            hasCourse.Category = category!;

            var courseAsDto = mapper.Map<CourseDto>(hasCourse);
            return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);
        }
    }
    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult());

            return group;
        }
    }
}
