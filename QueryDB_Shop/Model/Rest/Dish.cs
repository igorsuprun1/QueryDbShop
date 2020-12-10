using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QueryDB_Shop.Model.Rest
{
   

    public class Dish
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
        public ICollection<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();

        public ICollection<DishOrder> DishOrders { get; set; }

       

    }

}
