namespace RentACar.Health.Custom
{
    public class SqlServerCommandHealthCheck : SqlServerHealthCheck
    {
        public SqlServerCommandHealthCheck(string connectionString) : base(connectionString)
        {
        }
    }
}
