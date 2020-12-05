using AutoMapper;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetCarByIdQueryHandler : MediatRHandler, IRequestHandler<GetCarByIdQuery, Car>
    {
        private readonly IMapper _mapper;
        private readonly ICarCatalogService _carCatalogService;

        public GetCarByIdQueryHandler(IMapper mapper, ICarCatalogService carCatalogService)
        {
            _mapper = mapper;
            _carCatalogService = carCatalogService;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _carCatalogService.GetByIdAsync(request.Id);
            return await Task.FromResult(_mapper.Map<Car>(car));
        }
    }
}
