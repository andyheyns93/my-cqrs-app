using AutoMapper;
using CarCatalog.Api.MessagingIEventBusMessage;
using CarCatalog.Api.Profiles;
using CarCatalog.Business.Handlers;
using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.EventBus;
using CarCatalog.Core.Interfaces.Messaging.RabbitMq;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Interfaces.Repositories.Base;
using CarCatalog.Infrastructure.Base;
using CarCatalog.Infrastructure.Messaging.RabbitMq;
using CarCatalog.Infrastructure.Repositories;
using CarCatalog.Infrastructure.Repositories.Cache;
using CarCatalog.Infrastructure.Repositories.Sync;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RentACar.Health;
using Serilog;

namespace RentACar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddControllers();

            services.AddSingleton(Log.Logger);

            services.AddMemoryCache();
            services.AddSingleton<IQueryDbClient>(options => new QueryDbClient(Configuration["ConnectionStrings:QueryConnection"]));
            services.AddSingleton<ICommandDbClient>(options => new CommandDbClient(Configuration["ConnectionStrings:CommandConnection"]));

            services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
            services.AddSingleton<IEventBusPublisher, RabbitMqEventBusPublisher>();
            services.AddSingleton<IEventBusSubscriber, RabbitMqEventBusSubscriber>();

            services.AddSingleton<IQueryCarCatalogRepository, QueryCarCatalogRepository>();
            services.AddSingleton<ICommandCarCatalogRepository, CommandCarCatalogRepository>();
            services.AddSingleton<ISyncCarCatalogRepository, SyncCarCatalogRepository>();

            services.Decorate<IQueryCarCatalogRepository, CachedQueryCarCatalogRepository>();

            services.AddSingleton(_ =>
            {
                var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new CarCatalogProfile()));
                return mapperConfig.CreateMapper();
            });

            services.AddMediatR(typeof(MediatRHandler).Assembly, typeof(Startup).Assembly);
            services.AddHealthChecks()
                .AddSqlServerQueryHealthCheck(Configuration["ConnectionStrings:QueryConnection"], HealthStatus.Unhealthy)
                .AddSqlServerCommandHealthCheck(Configuration["ConnectionStrings:CommandConnection"], HealthStatus.Unhealthy);

            services.AddHostedService<MessageBrokerWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultHealthChecks();
                endpoints.MapControllers();
            });

        }
    }
}
