using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.Messaging.RabbitMq;
using CarCatalog.Infrastructure.Messaging.RabbitMq.Base;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqClient : BaseRabbitMqClient, IRabbitMqClient
    {
        public RabbitMqClient(ILogger logger, IOptions<RabbitMqConfiguration> rabbitMqConfiguration) : base(logger, rabbitMqConfiguration)
        {
        }

        public async Task<IModel> CreateModel()
        {
            if (!IsConnected)
                TryConnect();

            if (!IsConnected)
            {
                _logger.Fatal("No RabbitMQ connections are available to perform this action");
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }
            return await Task.FromResult(_connection.CreateModel());
        }
    }
}
