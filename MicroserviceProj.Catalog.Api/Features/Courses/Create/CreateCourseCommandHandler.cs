using AutoMapper;
using MassTransit;
using MediatR;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) 
        : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId);
            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error(HttpStatusCode.NotFound,"Category not found.", $"Category with id ({request.CategoryId}) not found.");
            }

            var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name);
            if (hasCourse) 
            {
                return ServiceResult<Guid>.Error(HttpStatusCode.BadRequest, "Course already exists.", $"Course with name ({request.Name}) already exists.");
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.CreatedTime = DateTime.Now;
            newCourse.Id = NewId.NextSequentialGuid();
            newCourse.Feature = new Feature()
            {
                Duration = 0, //Duration will calculate with algorithm.
                Rating = 0, //Rating will calculate with algorithm.
                EducatorFullName = "Ahmet ARSLAN" //EducatorFullName will came from token or KeyClock.
            };

            context.Courses.Add(newCourse);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id,$"api/courses/{newCourse.Id}");
        }
    }
}
