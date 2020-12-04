using CarCatalog.Core.Domain;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.Repositories
{
    public interface ICommandCarCatalogRepository
    {
        Task CreateCarAsync(string command, Car payload);
    }
}
