using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Queries.Base;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Handlers;
using System.Collections.Generic;

namespace CarCatalog.Business.Queries
{
    public class GetAllCarsQuery : Query, IQueryRequest<List<CarModel>>
    {
        public override string Name => "GetAllCarsQuery";
    }
}
