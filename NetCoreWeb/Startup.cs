using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using NetCoreWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NetCoreWeb.Models.SuperHui;
using Microsoft.Extensions.FileProviders;
using System.IO;
using NetCoreWeb.Areas.Bus.Models;
using NetCoreWeb.Areas.SportsStore.Models;
using NetCoreWeb.Areas.Cooking.Models;
using NetCoreWeb.Areas.Photo.Models;

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
            SetConfigureServices(services);

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://www.zoupenghui.com").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
            #endregion
        }
        //used in develpment envirenment
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            SetConfigureServices(services);

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:52095").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
            #endregion
        }

        private void SetConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:Identity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            if(appName == AppName.UperHui)
            {
                services.AddDbContext<SuperHuiDbContext>(options => options.UseSqlServer(Configuration["Data:SuperHui:ConnectionString"]));
                services.AddTransient<ICommentRepository, EFCommentRepository>();
                services.AddTransient<IDishRepository, EFDishRepository>();
                services.AddTransient<IAlbumRepository, EFAlbumRepository>();
                //services.AddScoped<Menu>(m => SessionMenu.GetMenu(m));
                //services.AddTransient<IDishOrderRepository, EFDishOrderRepository>();
            }
            #region others
            else if (appName == AppName.SportsStroe)
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
                services.AddTransient<IProductRepository, EFProductRepository>();
                services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
                services.AddTransient<IOrderRepository, EFOrderRepository>();
            }
            else if (appName == AppName.BusTicket)
            {
                services.AddDbContext<BusTicketDbContext>(options => options.UseSqlServer(Configuration["Data:BusTicket:ConnectionString"]));
                services.AddTransient<ITicketRepository, EFTicketRepository>();
                services.AddScoped<TicketCart>(tc => SessionTicketCart.GetCart(tc));
                services.AddTransient<ITicketOrderRepository, EFTicketOrderRepository>();
            } 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //生产环境
            app.UseExceptionHandler("/Error/Error");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files/Pictures")),
                RequestPath = new PathString("/Pictures")
            });
            app.UseStaticFiles(new StaticFileOptions()//烹饪菜品图片文件
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files/Pictures/Dishes")),
                RequestPath = new PathString("/Dishes")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()//文件目录浏览（危险，默认禁止启用）
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files/Pictures")),
                RequestPath = new PathString("/Pictures")
            });
            app.UseSession();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Error", template: "Error", defaults: new { controller = "Error", action = "Error" });
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}");
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
            // Shows UseCors with named policy.
            app.UseCors("AllowSpecificOrigin");            
            IdentitySeedData.EnsurePopulated(app);
        }
        //used in develpment envirenment
        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //development 
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files/Pictures")),
                RequestPath = new PathString("/Pictures")
            });
            app.UseStaticFiles(new StaticFileOptions()//烹饪菜品图片文件
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files/Pictures/Dishes")),
                RequestPath = new PathString("/Dishes")
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()//文件目录浏览（危险，默认禁止启用）
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files/Pictures")),
                RequestPath = new PathString("/Pictures")
            });
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
                    routes.MapRoute(name: "areas", template: "{area:exists}/{controller=Home}/{action=Index}");
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                }
            });
            // Shows UseCors with named policy.
            app.UseCors("AllowSpecificOrigin");
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
    public enum AppName
    {
        UperHui,//my main app
        SportsStroe,//Just used for dev test
        BusTicket//used in Ticket Order app         
    }

}
