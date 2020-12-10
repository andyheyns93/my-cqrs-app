using CarCatalog.Core.Interfaces.Event;
using CarCatalog.Core.Interfaces.EventBus;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Api.MessagingIEventBusMessage
{
    public class MessageBrokerWorker : Worker
    {
        private readonly IEventBusSubscriber _eventBusSubscriber;

        public MessageBrokerWorker(IEventBusSubscriber eventBusSubscriber)
        {
            _eventBusSubscriber = eventBusSubscriber;
        }

        public override async Task ExecuteAsync()
        {
            Log.Information("MessageBrokerWorker executing.");
            await _eventBusSubscriber.Subscribe<IEventBusMessage>();
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Information("MessageBrokerWorker stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}
