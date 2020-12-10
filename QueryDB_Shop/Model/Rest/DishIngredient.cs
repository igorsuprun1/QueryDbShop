using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryDB_Shop.Model.Rest
{
    public class DishIngredient
    {

        public int DishId { get; set; }
        public Dish Dish { get; set; }


        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public long quantityIngredient { get; set; } // количество ингредиентов

        public DateTime DishIngredientDate { get; set; } // дата создания


    }
}
