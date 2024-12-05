using AutoMapper;
using MediatR;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper)
        : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id,cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult.Error(HttpStatusCode.NotFound, "Course not found.", $"Course with id ({request.Id}) not found.");
            }

            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId);
            if (!hasCategory)
            {
                return ServiceResult.Error(HttpStatusCode.NotFound, "Category not found.", $"Category with id ({request.CategoryId}) not found.");
            }

            hasCourse.Name = request.Name;
            hasCourse.Description = request.Description;
            hasCourse.Price = request.Price;
            hasCourse.CategoryId = request.CategoryId;
            hasCourse.ImageUrl = request.ImageUrl;

            context.Courses.Update(hasCourse);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
