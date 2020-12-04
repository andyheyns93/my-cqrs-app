using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.MessageClients.RabbitMq;
using RabbitMQ.Client;

namespace CarCatalog.Infrastructure.MessageClients.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private IConnection _instance;

        public RabbitMqClient(RabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        public IConnection GetConnection()
        {
            if (_instance == null)
                _instance = GetInstance();
            return _instance;
        }

        public string GetQueueName() => _rabbitMqConfiguration.QueueName;

        private IConnection GetInstance() {
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
