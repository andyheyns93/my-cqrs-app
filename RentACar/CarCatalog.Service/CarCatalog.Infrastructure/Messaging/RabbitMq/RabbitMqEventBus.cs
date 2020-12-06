using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.Event;
using CarCatalog.Core.Interfaces.EventBus;
using CarCatalog.Core.Interfaces.Messaging.RabbitMq;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqEventBus : IEventBus, IDisposable
    {
        private static Dictionary<string, Type> _subscriptionManager = new Dictionary<string, Type>();

        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqClient<IModel> _rabbitMqClient;
        private readonly IMediator _mediator;
        private IModel _consumerChannel;

        public RabbitMqEventBus(IMediator mediator, IRabbitMqClient<IModel> rabbitMqClient, RabbitMqConfiguration rabbitMqConfiguration)
        {
            _mediator = mediator;
            _rabbitMqClient = rabbitMqClient;
            _rabbitMqConfiguration = rabbitMqConfiguration;

            _consumerChannel = CreateConsumerChannel();
        }

        public async Task Publish(IEvent @event)
        {
            using (var channel = _rabbitMqClient.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(exchange: _rabbitMqConfiguration.BrokerName, type: "direct");

                var json = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(json);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent
                channel.BasicPublish(exchange: _rabbitMqConfiguration.BrokerName, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
            }
            await Task.Run(() => { });
        }

        public async Task Subscribe<T>() where T : IEvent
        {
            var eventName = typeof(T).Name;
            var containsKey = _subscriptionManager.ContainsKey(eventName);
            if (!containsKey)
                _subscriptionManager.Add(eventName, typeof(T));

            using (var channel = _rabbitMqClient.CreateModel())
            {
                channel.QueueBind(queue: _rabbitMqConfiguration.QueueName, exchange: _rabbitMqConfiguration.BrokerName, routingKey: eventName);
            }
            await Task.Run(() => { });
        }

        private IModel CreateConsumerChannel()
        {
            var channel = _rabbitMqClient.CreateModel();

            channel.ExchangeDeclare(exchange: _rabbitMqConfiguration.BrokerName, type: "direct");
            channel.QueueDeclare(queue: _rabbitMqConfiguration.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                await ProcessEvent(eventName, message);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: _rabbitMqConfiguration.QueueName, autoAck: false, consumer: consumer);
            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };
            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subscriptionManager.ContainsKey(eventName))
            {
                var @type = _subscriptionManager[eventName];
                var @event = JsonConvert.DeserializeObject(message, @type) as IEvent;

                await _mediator.Send(@event);
            }
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_consumerChannel != null)
                        _consumerChannel.Dispose();
                    _subscriptionManager.Clear();
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}
