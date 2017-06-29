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
using NetCoreWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NetCoreWeb.Models.SuperHui;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace NetCoreWeb
{
    public class Startup
    {
        private readonly AppName appName;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            appName = AppName.UperHui;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (appName == AppName.SportsStroe)
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
                //services.AddTransient<IProductRepository, FakeProductRepository>();
                services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:Identity:ConnectionString"]));
                services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
                services.AddTransient<IProductRepository, EFProductRepository>();
                services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddTransient<IOrderRepository, EFOrderRepository>();
                //Add framework services.
                services.AddMvc();
                services.AddMemoryCache();
                services.AddSession();
            }
            else if(appName == AppName.UperHui)
            {
                services.AddDbContext<SuperHuiDbContext>(options => options.UseSqlServer(Configuration["Data:SuperHui:ConnectionString"]));
                services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:Identity:ConnectionString"]));
                services.AddTransient<ICommentRepository, EFCommentRepository>();
                services.AddTransient<IDishRepository, EFDishRepository>();
                services.AddScoped<Menu>(sp => SessionMenu.GetMenu(sp));
                services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                //Add framework services.
                services.AddMvc();
                services.AddMemoryCache();
                services.AddSession();

                #region CORS
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://www.zoupenghui.com").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
                 });
                #endregion

            }
        }
        //used in develpment envirenment
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<SuperHuiDbContext>(options => options.UseSqlServer(Configuration["Data:SuperHui:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:Identity:ConnectionString"]));
            services.AddTransient<ICommentRepository, EFCommentRepository>();
            services.AddTransient<IDishRepository, EFDishRepository>();
            services.AddScoped<Menu>(sp => SessionMenu.GetMenu(sp));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:52095").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
            #endregion
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
                app.UseExceptionHandler("/Error/Error");
            }

            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files")),
            //    RequestPath = new PathString("/src")
            //});
            app.UseSession();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Error", template: "Error", defaults: new { controller = "Error", action = "Error" });
                if (appName == AppName.SportsStroe)
                {
                    routes.MapRoute(name: null, template: "{category}/Page{page:int}", defaults: new { controller = "Product", action = "List" });
                    routes.MapRoute(name: null, template: "Page{page:int}", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "{category}", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                }    
                else
                {
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                }
            });
            // Shows UseCors with named policy.
            app.UseCors("AllowSpecificOrigin");            
            if (appName == AppName.SportsStroe)
            {
                SeedData.EnsurePopulated(app);
            }
            IdentitySeedData.EnsurePopulated(app);
        }
        //used in develpment envirenment
        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();

            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files")),
            //    RequestPath = new PathString("/src")
            //});
            app.UseSession();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Error", template: "Error", defaults: new { controller = "Error", action = "Error" });
                if (appName == AppName.SportsStroe)
                {
                    routes.MapRoute(name: null, template: "{category}/Page{page:int}", defaults: new { controller = "Product", action = "List" });
                    routes.MapRoute(name: null, template: "Page{page:int}", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "{category}", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "", defaults: new { controller = "Product", action = "List", page = 1 });
                    routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
                }
                else
                {
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                }
            });
            // Shows UseCors with named policy.
            app.UseCors("AllowSpecificOrigin");
            //if (appName == AppName.SportsStroe)
            //{
            //    SeedData.EnsurePopulated(app);
            //}
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }

    //"SPORTS_STROE" -- the app used for test
    //"SUPER_HUI"-- my personal website
    public enum AppName
    {
        SportsStroe,
        UperHui
    }

}
