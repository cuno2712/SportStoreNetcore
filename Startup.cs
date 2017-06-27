using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

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

            //MSSQL
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:MSSQLConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:MSSQLConnectionString"]));
            //MySQL
            //services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(Configuration["Data:SportStoreProducts:MySQLConnectionString"]));
            //services.AddDbContext<AppIdentityDbContext>(options => options.UseMySQL(Configuration["Data:SportStoreProducts:MySQLConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>(config => 
            {
                config.User.RequireUniqueEmail = true;
                config.Password = new PasswordOptions
                {
                    RequireDigit = true,                    
                    RequireUppercase = false,
                    RequireLowercase = true,
                    RequiredLength = 8,
                };
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.FromResult<object>(null);
                    }
                };
            }
            ).AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();            
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(name: "Error", template: "Error",
defaults: new { controller = "Error", action = "Error" });
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
            app.UseIdentity();
            IdentitySeedData.EnsurePopulated(app);

        }
    }
}
