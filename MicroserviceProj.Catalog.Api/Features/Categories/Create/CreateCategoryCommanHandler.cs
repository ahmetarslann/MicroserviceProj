using MassTransit;
using MediatR;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MicroserviceProj.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommanHandler(AppDbContext context)
        : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var isExists =
                await context.Categories.AnyAsync(x => x.Name == request.Name,cancellationToken: cancellationToken);

            if (isExists) 
            {
                ServiceResult<CreateCategoryResponse>.Error(HttpStatusCode.BadRequest,"Category Name already exists",
                    $"The category name of '{request.Name}' is already exists");
            }

            var category = new Category
            {
                Id = NewId.NextSequentialGuid(),
                Name = request.Name               
            };

            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"<empty>");
        }
    }
}
