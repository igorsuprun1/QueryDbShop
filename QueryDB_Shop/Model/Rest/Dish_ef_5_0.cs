using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QueryDB_Shop.Model.Rest
{

    // Модель для EntityFramework Core 5.0 FluentAPI У Блюда  есть много Ингридиентов
    public class Dish_ef_5_0
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Weight { get; set; }
        public int Calories { get; set; }
        //остатки
        public int Remain { get; set; }
        //забронировано
        public int Reserved { get; set; }

        //Ссылка на категорию связь один ко многим
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //Ссылка на Стану происхлждения связь один ко многим
        public int CountryOriginId { get; set; }
        public CountryOrigin CountryOrigin { get; set; }

        // Многие ко многим ссылка на промежуточную таблицу
        public List<Ingredient_ef_5_0> Ingredients_ef_5_0 { get; set; } = new List<Ingredient_ef_5_0>();
        public List<DishIngredient_ef_5_0> DishIngredients_ef_5_0 { get; set; } = new List<DishIngredient_ef_5_0>();

      //  public ICollection<DishOrder> DishOrders { get; set; }




    }
}
