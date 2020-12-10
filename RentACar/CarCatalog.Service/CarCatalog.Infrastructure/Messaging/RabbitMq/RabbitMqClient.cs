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
        public RabbitMqClient(IOptions<RabbitMqConfiguration> rabbitMqConfiguration) : base(rabbitMqConfiguration)
        {
        }

        public async Task<IModel> CreateModel()
        {
            if (!IsConnected)
                TryConnect();

            if (!IsConnected)
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

            return await Task.FromResult(_connection.CreateModel());
        }
    }
}
