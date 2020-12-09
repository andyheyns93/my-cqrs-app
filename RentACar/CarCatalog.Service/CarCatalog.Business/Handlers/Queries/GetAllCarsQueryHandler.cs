using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Queries;
using CarCatalog.Business.Queries.Base;
using CarCatalog.Core.Interfaces.Queries.Results;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetAllCarsQueryHandler : MediatRHandler, IRequestHandler<GetAllCarsQuery, IQueryResult<IEnumerable<CarModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IQueryCarCatalogRepository _queryCarCatalogRepository;

        public GetAllCarsQueryHandler(IMapper mapper, IQueryCarCatalogRepository queryCarCatalogRepository)
        {
            _mapper = mapper;
            _queryCarCatalogRepository = queryCarCatalogRepository;
        }

        public async Task<IQueryResult<IEnumerable<CarModel>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var domainObjects = await _queryCarCatalogRepository.GetCarsAsync();

            var models = _mapper.Map<List<CarModel>>(domainObjects);
            return await Task.FromResult(new QueryResult<IEnumerable<CarModel>>(models));
        }
    }
}
