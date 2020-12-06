using RabbitMQ.Client;

namespace CarCatalog.Core.Interfaces.Messaging.RabbitMq
{
    public interface IRabbitMqClient<T>
    {
        bool IsConnected { get; }
        bool TryConnect();
        T CreateModel();
    }
}
