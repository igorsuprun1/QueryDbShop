using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryDB_Shop.Model.Rest
{
     public  class Ingredient_ef_5_0
     {
        [Key]
        public int Id { get; set; }

        public string Name_ef_5_0 { get; set; }

        public long AllquantityIngredient_ef_5_0 { get; set; } // Остаток ингредиента

        public List<Dish_ef_5_0> Dishes_ef_5_0 { get; set; } = new List<Dish_ef_5_0>();
        public List<DishIngredient_ef_5_0> DishIngredients_ef_5_0 { get; set; } = new List<DishIngredient_ef_5_0>();



    }

}
