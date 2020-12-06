using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Interfaces.Handlers;
using CarCatalog.Business.Commands.Results;

namespace CarCatalog.Business.Commands
{
    public class CreateCarCommand : Command<CarModel>, ICommandRequest<CarModel, CreateCommandResult<CarModel>>
    {
        public CreateCarCommand(CarModel payload) : base(payload)
        {
        }
        public override string Name => "CreateCarCommand";
    }
}
