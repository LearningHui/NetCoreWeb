using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    public class Menu
    {
        private List<MenuLine> lineCollection = new List<MenuLine>();
        public virtual void AddItem(Dish dish, int quantity)
        {
            MenuLine line = lineCollection.Where(d => d.Dish.DishID == dish.DishID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new MenuLine { Dish = dish, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Dish dish) => lineCollection.RemoveAll(l => l.Dish.DishID == dish.DishID);
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Dish.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<MenuLine> Lines => lineCollection;
    }
    public class MenuLine
    {
        public int MenuLineID { get; set; }
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
    }
}
