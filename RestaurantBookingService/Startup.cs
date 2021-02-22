using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantBookingService.DataAccess.Context;
using RestaurantBookingService.Facade;
using RestaurantBookingService.Facade.Interfaces;
using RestaurantBookingService.Managers;
using RestaurantBookingService.Managers.Interfaces;

namespace RestaurantBookingService
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
            services.AddDbContext<Context>(options => options.UseNpgsql(Configuration["DatabaseOptions:ConnectionString"]), ServiceLifetime.Transient);
            
            services.AddTransient<IRestaurantManager, RestaurantManager>(provider => new RestaurantManager());
            services.AddTransient<IReservationManager, Managers.ReservationManager>(provider => new Managers.ReservationManager());
            services.AddTransient<IRepository, RepositoryManager>(provider => new RepositoryManager(provider.GetService<Context>(), new FactoryManager()));

            services.AddTransient<IRestaurantFacade, RestaurantFacade>(provider => new RestaurantFacade(
                provider.GetService<IRepository>()));

            services.AddTransient<IReservationFacade, Facade.ReservationFacade>(provider => new Facade.ReservationFacade(
                provider.GetService<IRepository>()));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
