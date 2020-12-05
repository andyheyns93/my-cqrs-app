using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.Core.Services
{
    public interface ICarCatalogService
    {
        Task<Car> CreateAsync(string command, Car model);
        Task<Car> GetByIdAsync(int id);
        Task<List<Car>> GetAllAsync();
    }
}
