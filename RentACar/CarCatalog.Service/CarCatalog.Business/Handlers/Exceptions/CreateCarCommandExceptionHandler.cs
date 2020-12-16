using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Common.Validation;
using CarCatalog.Core.Interfaces.Commands.Results;
using MediatR.Pipeline;

namespace CarCatalog.Business.Handlers.Exceptions
{
    public class CreateCarCommandExceptionHandler : RequestExceptionHandler<CreateCarCommand, ICommandResult<CarModel>, ValidationException>
    {
        // TODO: MAKE GENERIC
        protected override void Handle(CreateCarCommand request, ValidationException exception, RequestExceptionHandlerState<ICommandResult<CarModel>> state)
        {
            var result = new CommandResult<CarModel>(false, exception.Errors);
            state.SetHandled(result);
        }
    }
}
