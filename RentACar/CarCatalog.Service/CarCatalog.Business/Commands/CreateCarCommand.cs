using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Interfaces.Handlers;

namespace CarCatalog.Business.Commands
{
    public class CreateCarCommand : Command<CarDto>, ICommandRequest<CarDto>
    {
        public CreateCarCommand(CarDto payload) : base(payload)
        {
        }

        public override string Name => "CreateCarCommand";
    }
}
