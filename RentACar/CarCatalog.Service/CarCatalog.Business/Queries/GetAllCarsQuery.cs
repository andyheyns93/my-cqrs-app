using CarCatalog.Business.Queries.Base;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Handlers;
using System.Collections.Generic;

namespace CarCatalog.Business.Queries
{
    public class GetAllCarsQuery : Query, IQueryRequest<List<Car>>
    {
        public override string Name => "GetAllCarsQuery";
    }
}
