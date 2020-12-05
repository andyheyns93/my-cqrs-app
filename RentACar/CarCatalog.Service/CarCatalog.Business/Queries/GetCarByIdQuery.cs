using CarCatalog.Business.Queries.Base;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Handlers;

namespace CarCatalog.Business.Queries
{
    public class GetCarByIdQuery : Query, IQueryRequest<Car>
    {
        public GetCarByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }

        public override string Name => "GetCarByIdQuery";
    }
}
