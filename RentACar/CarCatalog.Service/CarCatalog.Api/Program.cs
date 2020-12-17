using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace RentACar
{
    #pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingcontext, config) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? hostingcontext.HostingEnvironment.EnvironmentName;
                    config.AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{env}.json", true, true);
                })
                .UseSerilog(Logging.ConfigureLogger)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    #pragma warning restore CS1591
}
