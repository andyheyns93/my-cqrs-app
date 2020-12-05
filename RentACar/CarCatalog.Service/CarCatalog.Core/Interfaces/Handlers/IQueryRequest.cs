using MediatR;

namespace CarCatalog.Core.Interfaces.Handlers
{
    public interface IQueryRequest<T> : IRequest<T>
    {
    }
}
