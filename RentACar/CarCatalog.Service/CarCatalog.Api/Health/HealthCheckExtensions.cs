﻿using CarCatalog.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RentACar.Health.Custom.RabbitMq;
using RentACar.Health.Custom.SqlServer;
using System;
using System.Collections.Generic;

namespace RentACar.Health
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddSqlServerQueryHealthCheck(this IHealthChecksBuilder builder,
            string connectionString,HealthStatus failureStatus = HealthStatus.Degraded, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            return builder.AddCheck($"Sql Server (read): ", new SqlServerQueryHealthCheck(connectionString), failureStatus, tags, timeout);
        }

        public static IHealthChecksBuilder AddSqlServerCommandHealthCheck(this IHealthChecksBuilder builder,
            string connectionString, HealthStatus failureStatus = HealthStatus.Degraded,  IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            return builder.AddCheck($"Sql Server (write): ", new SqlServerCommandHealthCheck(connectionString), failureStatus, tags, timeout);
        }

        public static IHealthChecksBuilder AddRabbitMqHealthCheck(this IHealthChecksBuilder builder,
           RabbitMqConfiguration rabbitMqConfiguration, HealthStatus failureStatus = HealthStatus.Degraded, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            return builder.AddCheck($"RabbitMq: ", new RabbitMqHealthCheck(rabbitMqConfiguration), failureStatus, tags, timeout);
        }
    }
}
