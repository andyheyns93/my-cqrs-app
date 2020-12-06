using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetAllCarsQueryHandler : MediatRHandler, IRequestHandler<GetAllCarsQuery, List<CarModel>>
    {
        private readonly IMapper _mapper;
        private readonly IQueryCarCatalogRepository _queryCarCatalogRepository;

        public GetAllCarsQueryHandler(IMapper mapper, IQueryCarCatalogRepository queryCarCatalogRepository)
        {
            _mapper = mapper;
            _queryCarCatalogRepository = queryCarCatalogRepository;
        }

        public async Task<List<CarModel>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var domainObjects = await _queryCarCatalogRepository.GetCarsAsync();
            return await Task.FromResult(_mapper.Map<List<CarModel>>(domainObjects));
        }
    }
}
