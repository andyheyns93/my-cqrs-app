using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;
using System;

namespace RentACar
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
           (hostingContext, loggerConfiguration) =>
           {
               var configuration = hostingContext.Configuration;
               var env = hostingContext.HostingEnvironment;

               loggerConfiguration.ReadFrom.Configuration(configuration, sectionName: "Logging:Serilog")
                    .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .Enrich.WithExceptionDetails();

               if (hostingContext.HostingEnvironment.IsDevelopment())
                   loggerConfiguration.MinimumLevel.Override("RentACar", LogEventLevel.Debug);

               var elasticUrl = hostingContext.Configuration.GetValue<string>("Logging:ElasticUrl");
               if (!string.IsNullOrEmpty(elasticUrl))
               {
                   loggerConfiguration.WriteTo.Elasticsearch(
                       new ElasticsearchSinkOptions(new Uri(elasticUrl))
                       {
                           AutoRegisterTemplate = true,
                           AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                           IndexFormat = "RentACar-logs-{0:yyyy.MM.dd}",
                           MinimumLogEventLevel = LogEventLevel.Debug
                       });
               }
           };
    }
}
