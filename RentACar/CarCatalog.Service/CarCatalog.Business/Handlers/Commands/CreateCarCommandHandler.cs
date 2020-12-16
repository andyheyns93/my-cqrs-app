using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.EventBus;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarCatalog.Core.Event;
using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Interfaces.Commands.Results;
using Serilog;
using CarCatalog.Business.Validation;
using System.Linq;
using System.Collections.Generic;
using CarCatalog.Core.Common.Validation;
using CarCatalog.Core.Interfaces.Commands;
using CarCatalog.Core.Interfaces.Validation;
using CarCatalog.Business.Validation.Validators;

namespace CarCatalog.Business.Handlers.Commands
{
    public class CreateCarCommandHandler : MediatRCommandHandler, IRequestHandler<CreateCarCommand, ICommandResult<CarModel>>
    {
        private readonly IMapper _mapper;
        private readonly IValidatorFactory _validatorFactory;
        private readonly ICommandCarCatalogRepository _commandCarCatalogRepository;
        private readonly IEventBusPublisher _eventBusPublisher;

        public CreateCarCommandHandler(
            IMapper mapper,
            IValidatorFactory validatorFactory,
            IEventBusPublisher eventBusPublisher,
            ICommandCarCatalogRepository commandCarCatalogRepository)
        {
            _mapper = mapper;
            _validatorFactory = validatorFactory;
            _commandCarCatalogRepository = commandCarCatalogRepository;
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task<ICommandResult<CarModel>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.Create<CreateCarCommand, CreateCarCommandValidator>();
            await ValidateAsync(request, validator);

            var domainObj = _mapper.Map<Car>(request.Payload);
            var newDomainObj = Car.CreateNewCar(domainObj.Brand, domainObj.Model, domainObj.Year);
            var success = await _commandCarCatalogRepository.CreateCarAsync(request.Name, newDomainObj);

            if (success)
            {
                Log.Information("CreateCarCommand successfully created");
                var createCarEvent = new CreateCarEvent(newDomainObj);
                _ = Task.Run(() => _eventBusPublisher.PublishAsync(createCarEvent));
            } else
                Log.Error("CreateCarCommand unsuccessful");

            var newDtoObject = _mapper.Map<CarModel>(newDomainObj);
            return await Task.FromResult(new CommandResult<CarModel>(newDtoObject, success));
        }
    }
}
