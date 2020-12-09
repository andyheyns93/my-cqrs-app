namespace RentACar.Health.Custom.SqlServer
{
    public class SqlServerCommandHealthCheck : SqlServerHealthCheck
    {
        public SqlServerCommandHealthCheck(string connectionString) : base(connectionString)
        {
        }
    }
}
