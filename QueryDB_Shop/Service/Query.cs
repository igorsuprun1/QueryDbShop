using Microsoft.EntityFrameworkCore;
using QueryDB_Shop.Data;
using QueryDB_Shop.Model;
using QueryDB_Shop.Model.Rest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryDB_Shop.Service
{
     public class Query
    {
        //private readonly ApplicationDbContext _appDbContext;
        //public Query (ApplicationDbContext appDbContext)
        //{
        //    _appDbContext = appDbContext;
        //}

        
        public List<ApplicationUser> GetUser()
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! GET GET GET GET");
            List<ApplicationUser> _result = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                 _result = db.ApplicationUsers.ToList();

            }
            
            return _result;
        }


         public void AddIngredient(List<Ingredient> argingredient )
         {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                foreach (Ingredient i in argingredient)

                {

                    Ingredient ingr = new Ingredient { Name = i.Name, AllquantityIngredient = i.AllquantityIngredient };
                    db.Ingredients.AddRange(ingr);
                }
               
                db.SaveChanges();

            }

            
        }







        //https://metanit.com/sharp/entityframeworkcore/3.3.php
        public void addDish( List<Ingredient> argingredient, long argQuantityIngredient)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Category category1 = new Category { Name = "Напитки" };
                Category category2 = new Category { Name = "Закуски" };
                db.Categories.AddRange(category1, category2);

                CountryOrigin countryOrigin = new CountryOrigin { Name = "Европейская" };
                db.CountryOrigins.AddRange(countryOrigin);


                
                Dish dish1 = new Dish { Name = "чай", Category = category1, CountryOrigin = countryOrigin, Calories = 330, Weight = 500, Remain = 100 };
               
                db.Dishes.AddRange(dish1);

                Console.WriteLine(" List<Ingredient> argingredient -- "  + argingredient.Count.ToString() );

                foreach (Ingredient ingr in argingredient)
                {
                    if (ingr.AllquantityIngredient > argQuantityIngredient)
                    {
                        var y = db.Ingredients.Find(ingr.Id);
                        if(y != null)
                        { 
                            y.DishIngredients.Add(new DishIngredient { Dish = dish1, Ingredient = y, quantityIngredient = argQuantityIngredient, DishIngredientDate = DateTime.Now });

                           Console.WriteLine(" Блюдо  создано ... отнимаем  инградиенты : " + ingr.Name);

                            long num = ingr.AllquantityIngredient - argQuantityIngredient;
                      
                            y.AllquantityIngredient = num;

                        }
                        db.SaveChanges();
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! addDish");

                    }
                    else
                    {
                        Console.WriteLine(" Блюдо не возможно создать ... не досточно инградиента : " + ingr.Name);
                    }

                }
                     Console.WriteLine("...выводим");

                var dishess = db.Dishes.Include(c => c.DishIngredients).ToList();
                // выводим все курсы
                foreach (var c in dishess)
                {
                    Console.WriteLine($"Блюда: {c.Name}");
                    // выводим всех студентов для данного кура
                    foreach (var s in c.DishIngredients)
                        Console.WriteLine($"Name: {s.Ingredient.Name}  Date: {s.DishIngredientDate.ToShortDateString()}  Mark: {s.quantityIngredient}");
                    Console.WriteLine("-------------------");
                }
                Console.WriteLine("...Закончили вывод");
               

               

            }
        }


        public void addAllDish()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Category category1 = new Category { Name = "Напитки" };
                Category category2 = new Category { Name = "Закуски" };
                db.Categories.AddRange(category1, category2);

                CountryOrigin countryOrigin = new CountryOrigin { Name = "Европейская" };
                db.CountryOrigins.AddRange(countryOrigin);

                Ingredient ingredient1 = new Ingredient { Name = "вода" };
                Ingredient ingredient2 = new Ingredient { Name = "сахар" };
                Ingredient ingredient3 = new Ingredient { Name = "морква" };
                Ingredient ingredient4 = new Ingredient { Name = "Лимон" };
                Ingredient ingredient5 = new Ingredient { Name = "капуста" };
                db.Ingredients.AddRange(ingredient1, ingredient2, ingredient3, ingredient4, ingredient5);


                Dish dish1 = new Dish { Name = "Limonad", Category = category1, CountryOrigin = countryOrigin, Calories = 330, Weight = 500, Remain = 100 };
                Dish dish2 = new Dish { Name = "Салат", Category = category2, CountryOrigin = countryOrigin, Calories = 730, Weight = 200, Remain = 14 };
                db.Dishes.AddRange(dish1, dish2);


                ingredient1.DishIngredients.Add(new DishIngredient { Dish = dish1, quantityIngredient = 250, DishIngredientDate = DateTime.Now });
                ingredient2.DishIngredients.Add(new DishIngredient { Dish = dish1, quantityIngredient = 50, DishIngredientDate = DateTime.Now });
                ingredient4.DishIngredients.Add(new DishIngredient { Dish = dish1, quantityIngredient = 10, DishIngredientDate = DateTime.Now });

                ingredient2.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 10, DishIngredientDate = DateTime.Now });
                ingredient3.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 250, DishIngredientDate = DateTime.Now });
                ingredient4.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 90, DishIngredientDate = DateTime.Now });
                ingredient5.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 80, DishIngredientDate = DateTime.Now });


                db.SaveChanges();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! addDish");


                Console.WriteLine("...выводим");

                var dishess = db.Dishes.Include(c => c.DishIngredients).ToList();
                // выводим все курсы
                foreach (var c in dishess)
                {
                    Console.WriteLine($"Course: {c.Name}");
                    // выводим всех студентов для данного кура
                    foreach (var s in c.DishIngredients)
                        Console.WriteLine($"Name: {s.Ingredient.Name}  Date: {s.DishIngredientDate.ToShortDateString()}  Mark: {s.quantityIngredient}");
                    Console.WriteLine("-------------------");
                }
                Console.WriteLine("...Закончили вывод");

            }
        }




    }
}
