using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Queries.Base;
using CarCatalog.Core.Interfaces.Handlers;
using CarCatalog.Core.Interfaces.Queries.Results;
using System.Collections.Generic;

namespace CarCatalog.Business.Queries
{
    public class GetAllCarsQuery : Query, IQueryRequest<IQueryResult<IEnumerable<CarModel>>>
    {
        public override string Name => "GetAllCarsQuery";
    }
}
