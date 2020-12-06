using AutoMapper;
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
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
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
            services.AddControllers();

            services.AddSingleton(Log.Logger);

            services.AddMemoryCache();
            services.AddSingleton<IQueryDbClient>(options => new QueryDbClient(Configuration["ConnectionStrings:QueryConnection"]));
            services.AddSingleton<ICommandDbClient>(options => new CommandDbClient(Configuration["ConnectionStrings:CommandConnection"]));

            services.AddSingleton<IRabbitMqClient<IModel>>(options =>
            {
                return new RabbitMqClient(options.GetService<ILogger>(), GetRabbitMqConfiguration());
            });
            services.AddSingleton<IEventBus>(options =>
            {
                return new RabbitMqEventBus(options.GetService<IMediator>(), options.GetService<IRabbitMqClient<IModel>>(), GetRabbitMqConfiguration());
            });

            services.AddSingleton<IQueryCarCatalogRepository, QueryCarCatalogRepository>();
            services.AddSingleton<ICommandCarCatalogRepository, CommandCarCatalogRepository>();
            services.AddSingleton<ISyncCarCatalogRepository, SyncCarCatalogRepository>();

            services.Decorate<IQueryCarCatalogRepository, CachedQueryCarCatalogRepository>();

            services.AddSingleton(_ =>
            {
                var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new CarCatalogProfile()));
                return mapperConfig.CreateMapper();
            });

            services.AddMediatR(typeof(MediatRHandler).Assembly);
        }

        //services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
        private RabbitMqConfiguration GetRabbitMqConfiguration() => Configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();

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
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<CarCatalog.Business.Queries.Event.CreateCarEvent>();
        }
    }
}
