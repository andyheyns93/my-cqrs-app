using System.Data;

namespace RentACar.Health.Custom.SqlServer
{
    public class SqlServerQueryHealthCheck : SqlServerHealthCheck
    {
        public SqlServerQueryHealthCheck(string connectionString) : base(connectionString)
        {
        }
    }
}
