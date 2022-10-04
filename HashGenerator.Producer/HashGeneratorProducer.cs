
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace HashGenerator.Producer
{

    public sealed class HashGeneratorProducer : IHashGeneratorProducer
    {       
        private readonly ConnectionFactory _connectionFactory;
        public HashGeneratorProducer(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task Publish<T>(T data, string queue)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var json = JsonConvert.SerializeObject(data);

                    var body = Encoding.UTF8.GetBytes(json);               

                    channel.ExchangeDeclare(exchange: "hash", type: ExchangeType.Fanout);

                    channel.QueueDeclare(queue: queue, exclusive: false);

                    channel.BasicPublish(exchange: "hash", routingKey: "hashes", body: body);

                    await Task.Yield();
                }
            }
        }
    }
}
