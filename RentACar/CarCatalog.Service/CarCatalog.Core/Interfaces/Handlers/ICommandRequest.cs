using MediatR;

namespace CarCatalog.Core.Interfaces.Handlers
{
    public interface ICommandRequest<T, R> : IRequest<R>
    {
    }
}
