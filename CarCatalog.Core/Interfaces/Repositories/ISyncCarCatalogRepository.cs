using CarCatalog.Core.Domain;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.Repositories
{
    public interface ISyncCarCatalogRepository
    {
        Task CreateCarAsync(Car syncObject);
    }
}
