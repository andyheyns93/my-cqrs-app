using CarCatalog.Core.Interfaces.Event;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.EventBus
{
    public interface IEventBusPublisher
    {
        Task PublishAsync(IEventBusMessage @event);
    }
}
