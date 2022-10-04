// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using HashGenerator.Shared.Models;



Console.WriteLine("Initializing...!");

var factory = new ConnectionFactory() { 
    HostName = "localhost",
    DispatchConsumersAsync = true
};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "hash", type: ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "hash", routingKey: "hashes");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var serialzedObject = JsonConvert.DeserializeObject<List<HashModel>>(message);

    Console.WriteLine($"Consumer - Recieved new message: {message}");
    
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine("Consuming");

Console.ReadKey();