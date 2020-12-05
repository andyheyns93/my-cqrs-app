using AutoMapper;
using CarCatalog.Api.Profiles;
using CarCatalog.Business.Base;
using CarCatalog.Business.Handlers.Commands;
using CarCatalog.Business.Services;
using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.MessageClients.RabbitMq;
using CarCatalog.Core.Interfaces.Repositories;
using CarCatalog.Core.Interfaces.Repositories.Base;
using CarCatalog.Core.Services;
using CarCatalog.Infrastructure.Base;
using CarCatalog.Infrastructure.MessageClients;
using CarCatalog.Infrastructure.MessageClients.RabbitMq;
using CarCatalog.Infrastructure.Repositories;
using CarCatalog.Infrastructure.Repositories.Cache;
using CarCatalog.Infrastructure.Repositories.Sync;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarCatalog.Business.Handlers.Commands;

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

            //services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddMemoryCache();
            services.AddSingleton<IQueryDbClient>(options => new QueryDbClient(Configuration["ConnectionStrings:QueryConnection"]));
            services.AddSingleton<ICommandDbClient>(options => new CommandDbClient(Configuration["ConnectionStrings:CommandConnection"]));

            services.AddSingleton<IRabbitMqClient>(options => new RabbitMqClient(Configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>()));
            services.AddSingleton<IMessageClient, RabbitMqMessageClient>();

            services.AddSingleton<IQueryCarCatalogRepository, QueryCarCatalogRepository>();
            services.AddSingleton<ICommandCarCatalogRepository, CommandCarCatalogRepository>();
            services.AddSingleton<ISyncCarCatalogRepository, SyncCarCatalogRepository>();

            services.Decorate<IQueryCarCatalogRepository, CachedQueryCarCatalogRepository>();

            services.AddSingleton(_ =>
            {
                var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new CarCatalogProfile()));
                return mapperConfig.CreateMapper();
            });

            services.AddMediatR(typeof(AssemblyPointerMediatR).Assembly);
            services.AddTransient(typeof(ICarCatalogService), typeof(CarCatalogService));
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
                endpoints.MapControllers();
            });
        }
    }
}
