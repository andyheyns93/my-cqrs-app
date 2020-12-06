using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Interfaces.Repositories.Base;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<Car> GetCarByIdAsync(Guid id)
        {
            using (var connection = _dbClient.GetConnection()) 
            {
                // TODO: https://blog.pagesd.info/2020/02/18/map-columns-properties-dapper/
                var query = "SELECT AggregateId as Id, Brand, Model, Year FROM R_Cars WHERE AggregateId = @AggregateId";
                var results = await connection.QueryAsync<Car>(query, new { AggregateId = id });
                return results.FirstOrDefault();
            }
        }

        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public async Task<List<Car>> GetCarsAsync()
        {
            using (var connection = _dbClient.GetConnection())
            {
                // TODO: https://blog.pagesd.info/2020/02/18/map-columns-properties-dapper/
                var query = "SELECT AggregateId as Id, Brand, Model, Year FROM R_Cars";
                var results = await connection.QueryAsync<Car>(query);
                return results.ToList();
            }
        }
    }
}
