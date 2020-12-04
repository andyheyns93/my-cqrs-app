using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Core.Interfaces.MessageClients.RabbitMq
{
    public interface IRabbitMqClient
    {
        IConnection GetConnection();
        string GetQueueName();
    }
}
