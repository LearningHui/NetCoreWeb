using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Areas.Cooking.Models
{
    public class Dish
    {
        public int DishID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        //public decimal Price { get; set; }
        /// <summary>
        /// 菜品图片名称
        /// </summary>
        public string ImageName{ get; set; }
        public string Remark { get; set; }
        public bool Disabled { get; set; }
    }
}
