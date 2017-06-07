using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreWeb.Models.SportsStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace NetCoreWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            //services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                if (env.IsDevelopment())
                {
                    routes.MapRoute(name: null, template: "{category}/Page{page:int}", defaults: new { controller = "Product", action = "List" });
                    routes.MapRoute(name: null, template: "Page{page:int}", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "{category}", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                }                    
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
            if (env.IsDevelopment())
            {
                SeedData.EnsurePopulated(app);
            }
        }
    }

}
