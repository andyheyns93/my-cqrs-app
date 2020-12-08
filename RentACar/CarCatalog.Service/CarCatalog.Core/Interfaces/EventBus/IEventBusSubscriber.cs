using CarCatalog.Core.Interfaces.Event;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.EventBus
{
    public interface IEventBusSubscriber
    {
        Task Subscribe<T>() where T : IEventBusMessage;
    }
}
