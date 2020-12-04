using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Domain;
using MediatR;

namespace CarCatalog.Business.Commands
{

    public class CreateCarCommand : Command<Car>, IRequest<Car>
    {
        public CreateCarCommand(Car payload) : base(payload)
        {
        }
    }
}
