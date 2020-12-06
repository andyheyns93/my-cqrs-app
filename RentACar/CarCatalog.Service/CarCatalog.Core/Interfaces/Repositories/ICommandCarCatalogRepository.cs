using CarCatalog.Core.Domain;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.Repositories
{
    public interface ICommandCarCatalogRepository
    {
        Task<bool> CreateCarAsync(string command, Car payload);
    }
}
