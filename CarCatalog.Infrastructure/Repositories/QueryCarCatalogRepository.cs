using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Interfaces.Repositories.Base;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Repositories
{
    public class QueryCarCatalogRepository : IQueryCarCatalogRepository
    {
        private readonly IQueryDbClient _dbClient;

        public QueryCarCatalogRepository(IQueryDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            using (var connection = _dbClient.GetConnection()) 
            {
                var query = "SELECT * FROM r_cars WHERE id = @Id";
                return (await connection.QuerySingleAsync<Car>(query, new { Id = id }));
            }
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            using (var connection = _dbClient.GetConnection())
            {
                var query = "SELECT * FROM r_cars";
                return (await connection.QueryAsync<Car>(query)).ToList();
            }
        }
    }
}
