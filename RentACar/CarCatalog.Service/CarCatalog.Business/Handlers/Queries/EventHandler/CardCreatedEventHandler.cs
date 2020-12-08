using CarCatalog.Business.Handlers;
using CarCatalog.Core.Event;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Queries.EventHandler
{
    public class CardCreatedEventHandler : MediatRHandler, IRequestHandler<CreateCarEvent, Unit>
    {
        private readonly ISyncCarCatalogRepository _syncCarCatalogRepository;

        public CardCreatedEventHandler(ISyncCarCatalogRepository syncCarCatalogRepository)
        {
            _syncCarCatalogRepository = syncCarCatalogRepository;
        }

        public async Task<Unit> Handle(CreateCarEvent request, CancellationToken cancellationToken)
        {
            await _syncCarCatalogRepository.CreateCarAsync(request.Data);

            return Unit.Value;
        }
    }
}
