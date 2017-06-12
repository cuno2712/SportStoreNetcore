﻿using System;
using System.Collections.Generic;
using System.Linq;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication7.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication7
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {


            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
              
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            var a = Configuration["Data:SportStoreProducts:ConnectionString"];
            var b = Configuration["Data:BaselineConnection:ConnectionString"];
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(
                name: null,
                template: "{category}/Page{page:int}",
                defaults: new { controller = "Product", action = "List" }
                );
                routes.MapRoute(
                name: null,
                template: "Page{page:int}",
                defaults: new { controller = "Product", action = "List", page = 1 }
                );
                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new { controller = "Product", action = "List", page = 1 }
                );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new { controller = "Product", action = "List", page = 1 });
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });


            SeedData.EnsurePopulated(app);

        }
    }
}
