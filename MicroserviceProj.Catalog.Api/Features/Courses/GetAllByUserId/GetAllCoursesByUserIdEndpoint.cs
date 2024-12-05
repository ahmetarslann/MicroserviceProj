using AutoMapper;
using MediatR;
using MicroserviceProj.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Courses.GetAllByUserId
{
    public record GetAllCoursesByUserIdQuery(Guid UserId) : IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCoursesByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x => x.UserId == request.UserId).ToListAsync();

            var categories = await context.Categories.ToListAsync();

            foreach (var course in courses)
            {
                course.Category = categories.First(x => x.Id == course.CategoryId);
            }

            var coursesAsDto = mapper.Map<List<CourseDto>>(courses);

            return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
        }
    }
    public static class GetAlCoursesByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllCoursesByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) =>
                    (await mediator.Send(new GetAllCoursesByUserIdQuery(userId))).ToGenericResult());

            return group;
        }
    }
}
