using CarCatalog.Core.Interfaces.MessageClients.RabbitMq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.MessageClients.RabbitMq
{
    public class RabbitMqMessageClient : IMessageClient
    {
        private readonly IRabbitMqClient _rabbitMqClient;

        public RabbitMqMessageClient(IRabbitMqClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
        }

        public async Task SendMessage<T>(T objToSend)
        {
            using (var channel = _rabbitMqClient.GetConnection().CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMqClient.GetQueueName(), durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(objToSend);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _rabbitMqClient.GetQueueName(), basicProperties: null, body: body);
            }
            await Task.Run(() => { });
        }
    }
}
