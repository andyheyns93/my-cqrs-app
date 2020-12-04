using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.MessageClients
{
    public interface IMessageClient
    {
        Task SendMessage<T>(T objToSend);
    }
}
