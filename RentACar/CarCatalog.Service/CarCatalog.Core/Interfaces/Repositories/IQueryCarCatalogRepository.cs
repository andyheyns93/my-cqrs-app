using CarCatalog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.Repositories
{
    public interface IQueryCarCatalogRepository
    {
        Task<List<Car>> GetCarsAsync();
        Task<Car> GetCarByIdAsync(Guid id);
    }
}
