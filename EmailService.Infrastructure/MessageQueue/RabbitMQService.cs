using System;
using System.Text;
using System.Threading.Tasks;
using EmailService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace EmailService.Infrastructure.MessageQueue
{
    public class RabbitMQService : IMessageQueue, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService(IOptions<AppSettings> appSettings)
        {
            var settings = appSettings.Value;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(settings.RabbitMQConnectionString)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task Publish<T>(T message, string queueName)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: properties,
                body: body);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}