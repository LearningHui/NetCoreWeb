﻿using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetCoreWeb.Models.SuperHui;
using NetCoreWeb.Areas.Bus.Models;
using NetCoreWeb.Areas.Cooking.Models;
using NetCoreWeb.Areas.Photo.Models;

namespace NetCoreWeb.Areas.SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            //ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            //if (!context.Products.Any())
            //{
            //    context.Products.AddRange(
            //        new Product { Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275 },
            //        new Product { Name = "Lifejacket", Description = "Protective and fashionable", Category = "Watersports", Price = 48.95m },
            //        new Product { Name = "Soccer Ball", Description = "FIFA-approved size and weight", Category = "Soccer", Price = 19.50m },
            //        new Product { Name = "Corner Flags", Description = "Give your playing field a professional touch", Category = "Soccer", Price = 34.95m },
            //        new Product { Name = "Stadium", Description = "Flat-packed 35,000-seat stadium", Category = "Soccer", Price = 79500 },
            //        new Product { Name = "Thinking Cap", Description = "Improve brain efficiency by 75%", Category = "Chess", Price = 16 },
            //        new Product { Name = "Unsteady Chair", Description = "Secretly give your opponent a disadvantage", Category = "Chess", Price = 29.95m },
            //        new Product { Name = "Human Chess Board", Description = "A fun game for the family", Category = "Chess", Price = 75 },
            //        new Product { Name = "Bling-Bling King", Description = "Gold-plated, diamond-studded King", Category = "Chess", Price = 1200 }); context.SaveChanges();
            //}

            #region 车票预定
            //BusTicketDbContext busTicketDbContext = app.ApplicationServices.GetRequiredService<BusTicketDbContext>();
            //if (!busTicketDbContext.Tickets.Any())
            //{
            //    busTicketDbContext.Tickets.AddRange(
            //        new Ticket { StartStation = "富士康", TerminalStation = "平顶山服务区", Price = 40, Description = "备注信息", Category = "富士康专线" },
            //        new Ticket { StartStation = "富士康", TerminalStation = "叶县", Price = 40, Description = "备注信息", Category = "富士康专线" },
            //        new Ticket { StartStation = "富士康", TerminalStation = "旧县", Price = 40, Description = "备注信息", Category = "富士康专线" },
            //        new Ticket { StartStation = "富士康", TerminalStation = "方城", Price = 50, Description = "备注信息", Category = "富士康专线" },
            //        new Ticket { StartStation = "富士康", TerminalStation = "石桥", Price = 50, Description = "备注信息", Category = "富士康专线" },
            //        new Ticket { StartStation = "富士康", TerminalStation = "南阳", Price = 50, Description = "备注信息", Category = "富士康专线" },
            //        new Ticket { StartStation = "富士康", TerminalStation = "镇平", Price = 60, Description = "备注信息", Category = "富士康专线" }
            //        );
            //    busTicketDbContext.SaveChanges();
            //} 
            #endregion

            SuperHuiDbContext SuperHuiContext = app.ApplicationServices.GetRequiredService<SuperHuiDbContext>();
            if (!SuperHuiContext.Dishes.Any())
            {
                SuperHuiContext.Dishes.AddRange(
                    new Dish { Name = "酱焖嘎鱼", Description = "肉质鲜嫩", Category = "鱼&生鲜", ImageName = "gayu.jpg" },
                    new Dish { Name = "糖醋鱼", Description = "武昌鱼、罗菲鱼", Category = "鱼&生鲜", ImageName = "suantaichaorou.jpg" },
                    new Dish { Name = "小葱拌豆腐", Description = "", Category = "凉菜", ImageName = "hds.jpg" },
                    new Dish { Name = "菠菜花生", Description = "", Category = "凉菜", ImageName = "lianou.jpg" },
                    new Dish { Name = "拍黄瓜", Description = "", Category = "凉菜", ImageName = "hongshaorou.jpg" }
                    );
                SuperHuiContext.SaveChanges();
            }
            
            if (!SuperHuiContext.Albums.Any())
            {
                SuperHuiContext.Albums.AddRange(
                    new Album { Name = "泰山行", Description = "夜爬泰山，别有一番风景", Category = "旅行", CreateTime= System.DateTime.Now},
                    new Album { Name = "紫竹院公园", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "奥森公园", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "凤凰岭", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "香山", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "长城", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "古北水镇", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "玉渊潭公园", Description = "", Category = "旅行", CreateTime = System.DateTime.Now },
                    new Album { Name = "十渡漂流", Description = "", Category = "旅行", CreateTime = System.DateTime.Now }
                    );
                SuperHuiContext.SaveChanges();
            }
        }
    }
}
