using CarCatalog.Business.Commands;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Infrastructure.MessageClients;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car>
    {
        private readonly ICommandCarCatalogRepository _carCatalogRepository;
        private readonly IMessageClient _messageClient;

        public CreateCarCommandHandler(ICommandCarCatalogRepository carCatalogRepository, IMessageClient messageClient)
        {
            _carCatalogRepository = carCatalogRepository;
            _messageClient = messageClient;
        }

        public async Task<Car> Handle(CreateCarCommand command, CancellationToken cancellationToken)
        {
            await _carCatalogRepository.CreateCarAsync(nameof(CreateCarCommand), command.Payload);
            await _messageClient.SendMessage(command.Payload);

            return command.Payload;
        }
    }
}
