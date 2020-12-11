using CarCatalog.Api.Contracts.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Api.MessagingIEventBusMessage
{
    public class Worker : BackgroundService, IWorker
    {
        private int baseDelay = 5000;
        public virtual Task ExecuteAsync() => Task.Run(() => { });

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information($"Worker starting");
                try
                {
                    await ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Log.Error($"Error running at: {ex.Message}", ex);
                }
                await Task.Delay(baseDelay, stoppingToken);
            }
            await Task.CompletedTask;
        }
    }
}
