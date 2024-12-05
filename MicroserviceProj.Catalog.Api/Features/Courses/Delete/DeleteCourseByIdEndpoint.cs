using MediatR;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using MicroserviceProj.Shared.Extensions;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Courses.Delete
{
    public record DeleteCourseByIdEndpoint(Guid Id) : IRequestByServiceResult;

    public class DeleteCourseByIdCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseByIdEndpoint, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseByIdEndpoint request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.FindAsync(request.Id, cancellationToken);
            if (hasCourse is null)
            {
                return ServiceResult.Error(HttpStatusCode.NotFound, "Course not found",
                    $"Course with id ${request.Id} was not found");
            }

            context.Courses.Remove(hasCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class DeleteCourseByIdCommandEndpoint
    {
        public static RouteGroupBuilder DeleteCourseByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new DeleteCourseByIdEndpoint(id))).ToGenericResult());

            return group;
        }
    }

}
