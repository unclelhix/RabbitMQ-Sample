
using HashGenerator.Core.DatabaseContext;
using HashGenerator.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HashGenerator.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HashGeneratorDbContext>(options =>
                       options.UseSqlServer(
                           configuration.GetConnectionString("DefaultConnection"),
                           b => b.MigrationsAssembly(typeof(HashGeneratorDbContext).Assembly.FullName)), 
                           ServiceLifetime.Transient);

            services.AddScoped<IHashGeneratorDbContext, HashGeneratorDbContext>();

            return services;
        }
    }
}