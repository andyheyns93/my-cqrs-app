using RabbitMQ.Client;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.Messaging.RabbitMq
{
    public interface IRabbitMqClient
    {
        Task<IModel> CreateModel();
    }
}
