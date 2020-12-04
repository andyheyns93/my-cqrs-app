using CarCatalog.Core.Interfaces.Repositories.Base;
using System.Data;
using System.Data.SqlClient;

namespace CarCatalog.Infrastructure.Base
{
    public class DbClient : IDbClient
    {
        private readonly string _connectionString;

        public DbClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
