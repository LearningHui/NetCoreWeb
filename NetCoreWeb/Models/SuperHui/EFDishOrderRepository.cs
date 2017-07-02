using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    public class EFDishOrderRepository : IDishOrderRepository
    {
        private SuperHuiDbContext context;
        public EFDishOrderRepository(SuperHuiDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Order> Orders =>
            context.Orders.Include(o => o.Lines).ThenInclude(l => l.Dish);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Dish));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
