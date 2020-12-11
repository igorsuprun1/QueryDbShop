using System;
using System.Collections.Generic;
using System.Text;

namespace QueryDB_Shop.Model.Rest
{
    public class DishIngredient_ef_5_0
    {
      

            public int DishId_ef_5_0 { get; set; }
            public Dish_ef_5_0 Dish_ef_5_0 { get; set; }


            public int IngredientId_ef_5_0 { get; set; }
            public Ingredient_ef_5_0 Ingredient_ef_5_0 { get; set; }

            public long quantityIngredient_ef_5_0 { get; set; } // количество ингредиентов

            public DateTime DishIngredientDate_ef_5_0 { get; set; } // дата создания


        
    }
}
