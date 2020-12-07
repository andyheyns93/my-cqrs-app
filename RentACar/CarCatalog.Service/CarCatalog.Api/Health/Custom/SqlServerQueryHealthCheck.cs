using System.Data;

namespace RentACar.Health.Custom
{
    public class SqlServerQueryHealthCheck : SqlServerHealthCheck
    {
        public SqlServerQueryHealthCheck(string connectionString) : base(connectionString)
        {
        }
    }
}
