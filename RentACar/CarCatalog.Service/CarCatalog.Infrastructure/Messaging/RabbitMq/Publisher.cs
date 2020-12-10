using CarCatalog.Core.Interfaces.Event;
using CarCatalog.Core.Interfaces.EventBus;
using Polly;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;
using Serilog;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq
{
    public abstract class Publisher : IEventBusPublisher
    {
        public abstract Task ExecuteAsync(IEventBusMessage eventMessage);
        public async Task PublishAsync(IEventBusMessage eventMessage)
        {
            await RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .Or<InvalidOperationException>()
                .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(5), (ex, time) =>
                {
                    Log.Error(ex, ex.Message);
                }).ExecuteAsync(async () =>
                {
                    await ExecuteAsync(eventMessage);
                });
        }
    }
}
