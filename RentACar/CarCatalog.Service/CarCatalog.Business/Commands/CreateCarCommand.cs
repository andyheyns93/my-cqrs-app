using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Interfaces.Commands.Results;
using CarCatalog.Core.Interfaces.Handlers;

namespace CarCatalog.Business.Commands
{
    public class CreateCarCommand : Command<CarModel>, ICommandRequest<CarModel, ICommandResult<CarModel>>
    {
        public CreateCarCommand(CarModel payload) : base(payload)
        {
        }
        public override string Name => "CreateCarCommand";
    }
}
