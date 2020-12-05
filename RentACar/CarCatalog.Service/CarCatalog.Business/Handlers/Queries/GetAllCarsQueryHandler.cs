using AutoMapper;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetAllCarsQueryHandler : MediatRHandler, IRequestHandler<GetAllCarsQuery, List<Car>>
    {
        private readonly IMapper _mapper;
        private readonly ICarCatalogService _carCatalogService;

        public GetAllCarsQueryHandler(IMapper mapper, ICarCatalogService carCatalogService)
        {
            _mapper = mapper;
            _carCatalogService = carCatalogService;
        }

        public async Task<List<Car>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carCatalogService.GetAllAsync();
            return await Task.FromResult(_mapper.Map<List<Car>>(cars));
        }
    }
}
