using CarCatalog.Core.Domain;
using MediatR;

namespace CarCatalog.Business.Queries
{
    public class GetCarByIdQuery : IRequest<Car>
    {
        public GetCarByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
