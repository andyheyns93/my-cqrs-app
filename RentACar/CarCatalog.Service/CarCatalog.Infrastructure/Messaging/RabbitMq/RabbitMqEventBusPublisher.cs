using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.Event;
using CarCatalog.Core.Interfaces.EventBus;
using CarCatalog.Core.Interfaces.Messaging.RabbitMq;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqEventBusPublisher : IEventBusPublisher
    {
        private readonly ILogger _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqClient _rabbitMqClient;

        public RabbitMqEventBusPublisher(ILogger logger, IOptions<RabbitMqConfiguration> rabbitMqConfiguration, IRabbitMqClient rabbitMqClient)
        {
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
            _rabbitMqClient = rabbitMqClient;
        }

        public async Task Publish(IEventBusMessage @event)
        {
            using (var channel = await _rabbitMqClient.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMqConfiguration.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.None
                };

                var json = JsonConvert.SerializeObject(@event, settings);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _rabbitMqConfiguration.QueueName, basicProperties: null, body: body);
            }
        }
    }
}
