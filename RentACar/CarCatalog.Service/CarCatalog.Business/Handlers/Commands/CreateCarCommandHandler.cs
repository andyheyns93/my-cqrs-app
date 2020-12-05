using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CarDto>
    {
        private readonly IMapper _mapper;
        private readonly ICarCatalogService _carCatalogService;
        

        public CreateCarCommandHandler(IMapper mapper, ICarCatalogService carCatalogService)
        {
            _mapper = mapper;
            _carCatalogService = carCatalogService;
        }

        public async Task<CarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request.Payload);

            // VALIDATION

            await _carCatalogService.CreateAsync(request.Name, car);

            return await Task.FromResult(_mapper.Map<CarDto>(request.Payload));
        }
    }
}
