using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Services;
using CarCatalog.Infrastructure.MessageClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCatalog.Business.Services
{
    public class CarCatalogService : ICarCatalogService
    {
        private readonly IQueryCarCatalogRepository _queryCarCatalogRepository;
        private readonly ICommandCarCatalogRepository _commandCarCatalogRepository;
        private readonly IMessageClient _messageClient;

        public CarCatalogService(
            IQueryCarCatalogRepository queryCarCatalogRepository,
            ICommandCarCatalogRepository commandCarCatalogRepository,
            IMessageClient messageClient
        )
        {
            _queryCarCatalogRepository = queryCarCatalogRepository;
            _commandCarCatalogRepository = commandCarCatalogRepository;
            _messageClient = messageClient;
        }

        public async Task<Car> CreateAsync(string command, Car model)
        {
            await _commandCarCatalogRepository.CreateCarAsync(command, model);
            await _messageClient.SendMessage(model);

            return model;
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _queryCarCatalogRepository.GetCarsAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _queryCarCatalogRepository.GetCarByIdAsync(id);
        }
    }
}
