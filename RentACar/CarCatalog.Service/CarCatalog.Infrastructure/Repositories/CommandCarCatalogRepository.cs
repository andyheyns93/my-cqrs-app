using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Interfaces.Repositories.Base;
using Dapper;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Repositories
{
    public class CommandCarCatalogRepository : ICommandCarCatalogRepository
    {
        private readonly ICommandDbClient _dbClient;

        public CommandCarCatalogRepository(ICommandDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task CreateCarAsync(string command, Car payload)
        {
            using (var connection = _dbClient.GetConnection())
            {
                var query = "INSERT INTO [dbo].[w_cars]([command], [payload]) VALUES (@Command, @Payload)";
                var result = await connection.ExecuteAsync(query, new
                {
                    Command = command,
                    Payload = JsonConvert.SerializeObject(payload)
                });
            }
        }
    }
}
