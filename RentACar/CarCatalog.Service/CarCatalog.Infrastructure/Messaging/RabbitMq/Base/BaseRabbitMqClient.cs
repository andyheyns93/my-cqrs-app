using CarCatalog.Core.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Serilog;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq.Base
{
    public abstract class BaseRabbitMqClient
    {
        private readonly object sync_root = new object();

        protected IConnection _connection;
        protected readonly RabbitMqConfiguration _rabbitMqConfiguration;

        public BaseRabbitMqClient(IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
        }

        protected bool IsConnected => _connection != null && _connection.IsOpen;

        protected bool TryConnect()
        {
            Log.Information("RabbitMQ Client is trying to connect");
            lock (sync_root)
            {
                _connection = GetConnection();
                if (IsConnected)
                {
                    Log.Information($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");
                    return true;
                }
                else
                {
                    Log.Fatal("RabbitMQ connections could not be created and opened");
                    return false;
                }
            }
        }

        private IConnection GetConnection()
        {
            if (_connection == null)
                _connection = GetInstance();
            return _connection;
        }

        private IConnection GetInstance()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqConfiguration.Hostname,
                Port = _rabbitMqConfiguration.Port,
                UserName = _rabbitMqConfiguration.UserName,
                Password = _rabbitMqConfiguration.Password
            };
            return factory.CreateConnection();
        }
    }
}
