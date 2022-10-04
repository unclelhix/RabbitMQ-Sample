using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HashGenerator.Producer.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddProducerService(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {             
                return new ConnectionFactory
                {
                    HostName = "localhost"
                };
            });

            services.AddTransient<IHashGeneratorProducer, HashGeneratorProducer>();
           
            return services;
        }
    }
}
