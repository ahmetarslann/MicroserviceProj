using MediatR;
using MicroserviceProj.Shared.Extensions;
using MicroserviceProj.Shared.Filters;

namespace MicroserviceProj.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult()).AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}
