using CarCatalog.Api.Models;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.MessageClients.RabbitMq;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Infrastructure.MessageClients;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Messaging
{
    public class CarCatalogWriteReceiver : BackgroundService, IMessageReceiver
    {
        private IModel _channel;
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly ISyncCarCatalogRepository _syncCarCatalogRepository;

        public CarCatalogWriteReceiver(IRabbitMqClient rabbitMqClient, ISyncCarCatalogRepository syncCarCatalogRepository)
        {
            _rabbitMqClient = rabbitMqClient;
            _syncCarCatalogRepository = syncCarCatalogRepository;

            _channel = rabbitMqClient.GetConnection().CreateModel();
            _channel.QueueDeclare(queue: rabbitMqClient.GetQueueName(), durable: false, exclusive: false, autoDelete: false, arguments: null);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var carModel = JsonSerializer.Deserialize<Car>(content);

                HandleMessage(carModel);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_rabbitMqClient.GetQueueName(), false, consumer);

            return Task.CompletedTask;
        }

        public void HandleMessage(Car objToHandle)
        {
            _syncCarCatalogRepository.CreateCarAsync(objToHandle);
        }


        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }
    }
}
