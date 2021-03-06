﻿using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.Event;
using CarCatalog.Core.Interfaces.Messaging.RabbitMq;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqEventBusPublisher : Publisher
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqClient _rabbitMqClient;

        public RabbitMqEventBusPublisher(IOptions<RabbitMqConfiguration> rabbitMqConfiguration, IRabbitMqClient rabbitMqClient)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
            _rabbitMqClient = rabbitMqClient;
        }

        public override async Task ExecuteAsync(IEventBusMessage eventMessage)
        {
            using (var channel = await _rabbitMqClient.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMqConfiguration.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    Formatting = Formatting.None
                };

                var json = JsonConvert.SerializeObject(eventMessage, settings);
                var body = Encoding.UTF8.GetBytes(json);

                Log.Information($"publishing event on queue: { _rabbitMqConfiguration.QueueName} with id: {eventMessage.Id}");
                channel.BasicPublish(exchange: "", routingKey: _rabbitMqConfiguration.QueueName, basicProperties: null, body: body);
            }
        }
    }
}
