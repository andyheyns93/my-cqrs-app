using CarCatalog.Core.Domain;

namespace CarCatalog.Infrastructure.MessageClients
{
    public interface IMessageReceiver
    {
        void HandleMessage(Car objToHandle);
    }
}
