// See https://aka.ms/new-console-template for more information


using HashGenerator.Core;
using HashGenerator.Core.Commands;
using HashGenerator.Core.DatabaseContext;
using HashGenerator.Infrastructure.DatabaseContext;
using HashGenerator.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

IConfigurationRoot Configuration;
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
Configuration = configurationBuilder.Build();

var serviceCollection = new ServiceCollection()
                            .AddCoreServices()
                            .AddDbContext<HashGeneratorDbContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                b => b.MigrationsAssembly(typeof(HashGeneratorDbContext).Assembly.FullName)),
                                ServiceLifetime.Transient)
                            .AddScoped<IHashGeneratorDbContext, HashGeneratorDbContext>()
                            .BuildServiceProvider();

Console.WriteLine("Initialize Consumer");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    DispatchConsumersAsync = true
};

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "hash", type: ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;

channel.BasicQos(0, 1, false);

channel.QueueBind(queue: queueName, exchange: "hash", routingKey: "hashes");

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.Received += async (model, ea) =>
{
    var body = Encoding.UTF8.GetString(ea.Body.ToArray());

    var message = JsonConvert.DeserializeObject<List<HashesDTO>>(body);

    Thread.Sleep(1000);

    try
    {

        Console.WriteLine($"Total Hashes Recieved: {message.Count}");

        Console.WriteLine($"Date Created: {message.FirstOrDefault().CreatedOn}");

        var mediator = serviceCollection.GetRequiredService<IMediator>();

        await mediator.Send(new AddHashCommand { HashesDTO = message });

        Console.WriteLine("Hashes Successfully Inserted");

    }

    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine("Consuming");

Console.ReadKey();

