using AutoMapper;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetCarByIdQueryHandler : MediatRHandler, IRequestHandler<GetCarByIdQuery, Car>
    {
        private readonly IMapper _mapper;
        private readonly IQueryCarCatalogRepository _carCatalogRepository;

        public GetCarByIdQueryHandler(IMapper mapper, IQueryCarCatalogRepository carCatalogRepository)
        {
            _mapper = mapper;
            _carCatalogRepository = carCatalogRepository;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            return await _carCatalogRepository.GetCarByIdAsync(request.Id);
        }
    }
}
