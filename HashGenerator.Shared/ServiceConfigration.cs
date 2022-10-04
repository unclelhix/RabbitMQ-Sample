using HashGenerator.Shared.Contracts;
using HashGenerator.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HashGenerator.Shared
{
    public static class ServiceConfigration
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {            
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<ISha1Service, Sha1Service>();

            return services;
        }
    }
}