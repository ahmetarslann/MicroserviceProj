using AutoMapper;
using MediatR;
using MicroserviceProj.Catalog.Api.Features.Categories.GetAll;
using MicroserviceProj.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceProj.Catalog.Api.Features.Courses.GetAll
{
    public record GetAllCoursesQuery():IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }

            var courseDtos = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
        }
    }

    public static class GetAllCoursesEndpoint
    {
        public static RouteGroupBuilder GetAllCoursesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                    (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult());

            return group;
        }
    }
}
