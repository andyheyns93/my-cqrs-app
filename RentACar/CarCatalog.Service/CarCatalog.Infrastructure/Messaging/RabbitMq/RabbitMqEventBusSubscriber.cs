using CarCatalog.Core.Configuration;
using CarCatalog.Core.Event;
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
    public class RabbitMqEventBusSubscriber : IEventBusSubscriber
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IMediator _mediator;

        public RabbitMqEventBusSubscriber(IRabbitMqClient rabbitMqClient, IOptions<RabbitMqConfiguration> rabbitMqConfiguration, IMediator mediator)
        {
            _rabbitMqClient = rabbitMqClient;
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
            _mediator = mediator;
        }

        public async Task Subscribe<T>() where T : IEventBusMessage
        {
            var channel = await _rabbitMqClient.CreateModel();

            channel.QueueDeclare(queue: _rabbitMqConfiguration.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += HandleMessage(channel);

            channel.BasicConsume(_rabbitMqConfiguration.QueueName, false, consumer);

            Log.Information($"subscribing worker on queue: { _rabbitMqConfiguration.QueueName}");
        }

        private EventHandler<BasicDeliverEventArgs> HandleMessage(IModel channel)
        {
            return async (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                await ProcessEvent(message);
                channel.BasicAck(ea.DeliveryTag, false);
            };
        }

        private async Task ProcessEvent(string message)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.None
            };
            var @event = JsonConvert.DeserializeObject(message, settings) as IEventBusMessage;

            Log.Information($"publishing event message on queue: { _rabbitMqConfiguration.QueueName} with id: {@event.Id}");
            await _mediator.Send(@event);
        }
    }
}
