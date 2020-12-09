using CarCatalog.Api.Contracts.Interfaces;
using CarCatalog.Core.Interfaces.Queries.Results;
using System.Collections.Generic;

namespace CarCatalog.Business.Queries.Base
{
    public class QueryResult<T> : IQueryResult<T> // where T : IModel
    {
        public QueryResult(T model)
        {
            Data = model;
        }

        public T Data { get; set; }
    }
}
