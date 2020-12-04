using AutoMapper;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetAllCarsQueryHandler : MediatRHandler, IRequestHandler<GetAllCarsQuery, List<Car>>
    {
        private readonly IQueryCarCatalogRepository _carCatalogRepository;

        public GetAllCarsQueryHandler(IMapper mapper, IQueryCarCatalogRepository carCatalogRepository)
        {
            _carCatalogRepository = carCatalogRepository;
        }

        public async Task<List<Car>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            return await _carCatalogRepository.GetCarsAsync();
        }
    }
}
