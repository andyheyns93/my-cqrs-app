using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Queries.Event;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.EventBus;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarCatalog.Business.Commands.Results;

namespace CarCatalog.Business.Handlers.Commands
{
    public class CreateCarCommandHandler : MediatRHandler, IRequestHandler<CreateCarCommand, CreateCommandResult<CarModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICommandCarCatalogRepository _commandCarCatalogRepository;
        private readonly IEventBus _eventBus;

        public CreateCarCommandHandler(IMapper mapper, IEventBus eventBus, ICommandCarCatalogRepository commandCarCatalogRepository)
        {
            _mapper = mapper;
            _commandCarCatalogRepository = commandCarCatalogRepository;
            _eventBus = eventBus;
        }

        public async Task<CreateCommandResult<CarModel>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var domainObj = _mapper.Map<Car>(request.Payload);

            // VALIDATION
            // TODO

            //SAVE IN WRITE DB
            var newDomainObj = Car.CreateNewCar(domainObj.Brand, domainObj.Model, domainObj.Year);
            var success = await _commandCarCatalogRepository.CreateCarAsync(request.Name, newDomainObj);

            if (success)
            {
                var createCarEvent = new CreateCarEvent(newDomainObj);
                await _eventBus.Publish(createCarEvent);
            }

            var newDtoObject = _mapper.Map<CarModel>(newDomainObj);
            return await Task.FromResult(new CreateCommandResult<CarModel>(newDtoObject, success));
        }
    }
}
