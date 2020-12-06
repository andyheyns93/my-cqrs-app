using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Interfaces.Handlers;
using CarCatalog.Business.Commands.Results;

namespace CarCatalog.Business.Commands
{
    public class CreateCarCommand : Command<CarDto>, ICommandRequest<CarDto, CreateCommandResult<CarDto>>
    {
        public CreateCarCommand(CarDto payload) : base(payload)
        {
        }
        public override string Name => "CreateCarCommand";
    }
}
