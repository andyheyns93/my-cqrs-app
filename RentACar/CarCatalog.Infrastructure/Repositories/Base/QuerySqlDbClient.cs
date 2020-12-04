using CarCatalog.Core.Interfaces.Repositories.Base;

namespace CarCatalog.Infrastructure.Base
{
    public class QueryDbClient: DbClient, IQueryDbClient
    {
        public QueryDbClient(string connectionString) : base(connectionString)
        {
        }
    }
}
