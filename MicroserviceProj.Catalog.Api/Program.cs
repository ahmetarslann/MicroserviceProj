using MicroserviceProj.Catalog.Api;
using MicroserviceProj.Catalog.Api.Features.Categories;
using MicroserviceProj.Catalog.Api.Features.Courses;
using MicroserviceProj.Catalog.Api.Options;
using MicroserviceProj.Catalog.Api.Repositories;
using MicroserviceProj.Shared.Extensions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));

var app = builder.Build();

app.AddCategoryGroupEndpointExt();
app.AddCourseGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
