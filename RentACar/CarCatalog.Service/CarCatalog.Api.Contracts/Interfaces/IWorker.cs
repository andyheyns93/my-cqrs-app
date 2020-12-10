using System.Threading.Tasks;

namespace CarCatalog.Api.Contracts.Interfaces
{
    public interface IWorker
    {
        Task ExecuteAsync();
    }
}
