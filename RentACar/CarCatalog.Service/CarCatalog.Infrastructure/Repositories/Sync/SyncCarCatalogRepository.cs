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

        public async Task CreateCarAsync(Car syncObject)
        {
            using (var connection = _dbClient.GetConnection())
            {
                var query = "INSERT INTO [dbo].[r_cars]([brand], [model], [year]) VALUES (@Brand, @Model, @Year)";
                var result = await connection.ExecuteAsync(query, new { syncObject.Brand, syncObject.Model, syncObject.Year });
            }
        }
    }
}
