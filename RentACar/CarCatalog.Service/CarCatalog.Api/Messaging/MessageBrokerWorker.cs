using CarCatalog.Core.Interfaces.Event;
using CarCatalog.Core.Interfaces.EventBus;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Api.MessagingIEventBusMessage
{
    public class MessageBrokerWorker : BackgroundService
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;

        public MessageBrokerWorker(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventBusSubscriber.Subscribe<IEventBusMessage>();
        }
    }
}
