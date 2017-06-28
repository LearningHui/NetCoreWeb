﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    public interface IDishRepository
    {
        IEnumerable<Dish> Dishes { get; }
        void SaveDish(Dish dish);
        Dish DeleteDish(int dishID);
    }
}
