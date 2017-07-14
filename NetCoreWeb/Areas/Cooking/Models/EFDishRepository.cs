using NetCoreWeb.Models.SuperHui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Cooking.Models
{
    /// <summary>
    /// 菜品仓储，增删改查
    /// </summary>
    public class EFDishRepository : IDishRepository
    {
        private SuperHuiDbContext context;
        public EFDishRepository(SuperHuiDbContext ctx) { context = ctx; }
        public IEnumerable<Dish> Dishes => context.Dishes;
        public void SaveDish(Dish dish)
        {
            if (dish.DishID == 0)
            {
                context.Dishes.Add(dish);
            }
            else
            {
                Dish dbEntry = context.Dishes.FirstOrDefault(p => p.DishID == dish.DishID);
                if (dbEntry != null)
                {
                    dbEntry.Name = dish.Name;
                    dbEntry.Description = dish.Description;
                    //dbEntry.Price = dish.Price;
                    dbEntry.Category = dish.Category;
                    dbEntry.ImageName = dish.ImageName;
                    dbEntry.Remark = dish.Remark;
                    dbEntry.Disabled = dish.Disabled;
                }
            }
            context.SaveChanges();
        }
        public Dish DeleteDish(int dishID)
        {
            Dish dbEntry = context.Dishes.FirstOrDefault(p => p.DishID == dishID);
            if (dbEntry != null)
            {
                context.Dishes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
