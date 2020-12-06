using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Interfaces.Repositories.Base;
using Dapper;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Repositories.Sync
{
    public class SyncCarCatalogRepository : ISyncCarCatalogRepository
    {
        private readonly IQueryDbClient _dbClient;

        public SyncCarCatalogRepository(IQueryDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task<bool> CreateCarAsync(Car syncObject)
        {
            using (var connection = _dbClient.GetConnection())
            {
                var query = "INSERT INTO [dbo].[R_Cars]([AggregateId],[Brand], [Model], [Year]) VALUES (@AggregateId, @Brand, @Model, @Year)";
                var result = await connection.ExecuteAsync(query, new { AggregateId = syncObject.Id, syncObject.Brand, syncObject.Model, syncObject.Year });
                return result > 0;
            }
        }
    }
}
