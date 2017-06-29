using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWeb.Infrastructure;

namespace NetCoreWeb.Models.SuperHui
{
    public class SessionMenu : Menu
    {
        public static Menu GetMenu(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionMenu menu = session?.GetJson<SessionMenu>("Menu") ?? new SessionMenu();
            menu.Session = session;
            return menu;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Dish dish, int quantity)
        {
            base.AddItem(dish, quantity);
            Session.SetJson("Menu", this);
        }
        public override void RemoveLine(Dish dish)
        {
            base.RemoveLine(dish);
            Session.SetJson("Menu", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Menu");
        }
    }
}
