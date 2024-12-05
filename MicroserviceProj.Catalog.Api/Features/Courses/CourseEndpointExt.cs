using MicroserviceProj.Catalog.Api.Features.Courses.Create;
using MicroserviceProj.Catalog.Api.Features.Courses.Delete;
using MicroserviceProj.Catalog.Api.Features.Courses.GetAll;
using MicroserviceProj.Catalog.Api.Features.Courses.GetAllByUserId;
using MicroserviceProj.Catalog.Api.Features.Courses.GetById;
using MicroserviceProj.Catalog.Api.Features.Courses.Update;

namespace MicroserviceProj.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndpoint()
                .GetAllCoursesGroupItemEndpoint()
                .GetCourseByIdGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseByIdGroupItemEndpoint()
                .GetAllCoursesByUserIdGroupItemEndpoint();
        }
    }
}
