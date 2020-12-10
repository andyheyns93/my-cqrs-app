using CarCatalog.Core.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RentACar.Health.Custom.RabbitMq
{
    public class RabbitMqHealthCheck : IHealthCheck
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;

        public RabbitMqHealthCheck(RabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqConfiguration.Hostname,
                    Port = _rabbitMqConfiguration.Port,
                    UserName = _rabbitMqConfiguration.UserName,
                    Password = _rabbitMqConfiguration.Password
                };
                factory.CreateConnection();
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, description: e.Message, exception: e));
            }
        }
    }
}
