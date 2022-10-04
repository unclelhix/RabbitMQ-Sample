using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using System.Reflection;
using MediatR;
using HashGenerator.Core.Commands;

namespace HashGenerator.Core
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
         
            var typeAdapterConfig = new TypeAdapterConfig();
      
            var mappingRegistrations = TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
   
            mappingRegistrations.ToList().ForEach(register => register.Register(typeAdapterConfig));
         
            var mapperConfig = new Mapper(typeAdapterConfig);

            services.AddSingleton<IMapper>(mapperConfig);

            services.AddTransient<IRequestHandler<AddHashCommand, Unit>, AddHashCommandHandler>();

            var applicationAssembly = typeof(AssemblyReference).Assembly;

            services.AddMediatR(applicationAssembly);

            return services;

        }
    }
}