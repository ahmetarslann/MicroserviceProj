using FluentValidation;
using FluentValidation.AspNetCore;
using MicroserviceProj.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceProj.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            //MediatR
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

            //FluentValidator
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);

            //AutoMapper
            services.AddAutoMapper(assembly);

            //Others
            services.AddScoped<IIdentityService, IdentityServiceFake>();
            return services;
        }
    }
}
