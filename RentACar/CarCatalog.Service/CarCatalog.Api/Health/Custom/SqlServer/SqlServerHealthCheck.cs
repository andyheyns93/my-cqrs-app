using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace RentACar.Health.Custom.SqlServer
{
    public abstract class SqlServerHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public SqlServerHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                return Task.FromResult(HealthCheckResult.Healthy());

            }
            catch (Exception e)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, description: e.Message, exception: e));
            }
        }
    }
}
