using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products
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
            //services.AddScoped<IProductsProvider, ProductsProvider>();
            //services.AddAutoMapper(typeof(Startup));
            //services.AddDbContext<ProductDbContext>(options =>
            //{
            //    options.UseInMemoryDatabase("Products");
            //});
            //services.AddControllers();
            services.AddApplicationInsightsTelemetry("8ea3060a-8e0e-4368-830a-48b2d4116c97");
            services.AddControllers();
            services.AddScoped<IProductsProvider, ProductsProvider>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<ProductDbContext>(options =>
                options.UseInMemoryDatabase("Products"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
