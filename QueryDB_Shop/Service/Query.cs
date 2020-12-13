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


        //Метод добавления данных
        #region
        public void AddUser()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // создаем два объекта User
                ApplicationUser user1 = new ApplicationUser { UserName = "Tom" };
                ApplicationUser user2 = new ApplicationUser { UserName = "Alice" };

                //добавляем их в бд
                db.ApplicationUsers.Add(user1);
                db.ApplicationUsers.Add(user2);
                //записываем добавленые записи в базу данных
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");
            }
        }
        #endregion

        //Метод вывода списка пользователей 
        #region
        public List<ApplicationUser> GetUser()
        {
            Console.WriteLine("!! GET GET GET GET !! Пользователей");

            //создаем пустой лист
            List<ApplicationUser> _result = new List<ApplicationUser>();
            //Подключения к базе
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //Запрос к базе
                 _result = db.ApplicationUsers.ToList();

            }
            
            return _result;
        }
        #endregion

        //Метод добавления ингредиентов данных
        #region
        public void AddIngredient(List<Ingredient> argingredient )
         {
            //Подключения к базе
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // Ищем обекты *Ингридиенты* в прилетающем нам списке инградиентов
                foreach (Ingredient i in argingredient)
                {
                    // данные найденого инградиент служат для создания нового обекта *Ингридиент* для записи в бд
                    Ingredient ingr = new Ingredient { Name = i.Name, AllquantityIngredient = i.AllquantityIngredient };
                    //добавляем их в бд
                    db.Ingredients.AddRange(ingr);
                }
                //записываем добавленые записи в базу данных
                db.SaveChanges();

            }
        }
        #endregion

        //Метод добавления блюда 
        //https://metanit.com/sharp/entityframeworkcore/3.3.php
        #region
        public void addDish( List<Ingredient> argingredient, long argQuantityIngredient)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //Запрос к бд нужной нам категории
                var category = db.Categories.FirstOrDefault(s => s.Name == "Напитки");
                //Запрос к бд нужной нам страны происхождения
                var countryOrigin = db.CountryOrigins.FirstOrDefault(q => q.Name == "Европейская" );
                //Создаем новое блюда для записи в бд
                Dish dish1 = new Dish { Name = "чай", Category = category, CountryOrigin = countryOrigin, Calories = 330, Weight = 500, Remain = 100 };
                //Добавляем в бд созданое рание блюдо
                db.Dishes.AddRange(dish1);
                //Ищем Инградиенты для этого блюда ( с которого оно состоит и в каких пропорциях)
                foreach (Ingredient ingr in argingredient)
                {
                    // Условие хватает ли остатков этого инградиента на *Складе* если нет то не создаем Блюдо
                    if (ingr.AllquantityIngredient > argQuantityIngredient)
                    {
                        //Ищем этот инградиент в бд *на всякий случай для перестраховки , но думаю можно и без этого* 
                        var y = db.Ingredients.Find(ingr.Id);
                        //Если он есть то идем дальше
                        if(y != null)
                        { 
                            //добавляем блюду инградиет в промежуточную таблицу с указание сколько инградиента было потрацено чтобы обновить остатки*Склада*
                            y.DishIngredients.Add(new DishIngredient { Dish = dish1, Ingredient = y, quantityIngredient = argQuantityIngredient, DishIngredientDate = DateTime.Now });

                           Console.WriteLine(" Блюдо  создано ... отнимаем  инградиенты : " + ingr.Name);
                            //Отнимаем от остатков в *Складе* потраченые количество инградиента
                            long num = ingr.AllquantityIngredient - argQuantityIngredient;
                            //Обновляем остатки в *Складе* в бд
                            y.AllquantityIngredient = num;
                            //Записываем это все в бд
                            db.SaveChanges();
                        }

                        db.SaveChanges();
                        Console.WriteLine("!!! addDish  !!!");

                    }
                    else
                    {
                        Console.WriteLine(" Блюдо не возможно создать ... не досточно инградиента : " + ingr.Name);
                    }

                }
                     
               
            }
        }
        #endregion

        //Метод вывода блюда с его составом 
        //https://metanit.com/sharp/entityframeworkcore/3.3.php
        #region
        public void GetAllDish()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.WriteLine("...выводим");

                //Запрос к базе данных с включение дерева связей таблицы Многие-ко-Многим (Include первое включение*DishIngredients*, ThenInclude включение нижнего уровня дерева *Ingredient*
                var dishess = db.Dishes.Include(c => c.DishIngredients).ThenInclude(s => s.Ingredient).ToList();
                // выводим все блюда
                foreach (var c in dishess)
                {
                    try
                    {
                        Console.WriteLine($"Блюдо: {c.Name}");
                        // выводим все инградиенты  для данного Блюда
                        foreach (var s in c.DishIngredients)
                            Console.WriteLine($"Name: {s.Ingredient.Name}  Date: {s.DishIngredientDate.ToShortDateString()}  Mark: {s.quantityIngredient}");
                        Console.WriteLine("-------------------");

                    }
                    catch
                    {
                        Console.WriteLine("...error");

                    }
                   
                }
                Console.WriteLine("...Закончили вывод");

            }
        }
        #endregion

        //Метод вывода блюда с его составом 
        //https://metanit.com/sharp/entityframeworkcore/3.3.php
        #region
        public void GetAllDishAll()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.WriteLine("...выводим");

                //Запрос к базе данных с включение дерева связей таблицы Многие-ко-Многим (Include первое включение*DishIngredients*, ThenInclude включение нижнего уровня дерева *Ingredient*
                var dishess = db.Dishes.Include(q=>q.Category).Include(o => o.CountryOrigin).Include(c => c.DishIngredients).ThenInclude(s => s.Ingredient).ToList();
                // выводим все блюда
                foreach (var c in dishess)
                {
                    try
                    {
                        Console.WriteLine($"Кухня : {c.CountryOrigin.Name} Категория : {c.Category.Name}  Блюдо: {c.Name}  " );
                        // выводим все инградиенты  для данного Блюда
                        foreach (var s in c.DishIngredients)
                            Console.WriteLine($"Name: {s.Ingredient.Name}  Date: {s.DishIngredientDate.ToShortDateString()}  Количество: {s.quantityIngredient}");
                        Console.WriteLine("-------------------");

                    }
                    catch
                    {
                        Console.WriteLine("...error");

                    }

                }
                Console.WriteLine("...Закончили вывод");

            }
        }
        #endregion

         // Заказ
        #region 
        public void Order()
        {
            
            //Подключения к базе
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
              Console.WriteLine(" ADD Order: ");
                //Запрос к базе
                var usr = db.ApplicationUsers.FirstOrDefault(s => s.UserName == "igorsuprun1@gmail.com");
               
               // Dictionary<Dish, int> listDishes = new Dictionary<Dish, int>();
                Dictionary<int, Dish> listDishes = new Dictionary<int, Dish>();
               // var dish1 = db.Dishes.Include(v => v.Category).Include(q => q.CountryOrigin).Include(i =>i.DishIngredients).FirstOrDefault(s => s.Name == "Чай");
                var dish2 = db.Dishes.FirstOrDefault(s => s.Name == "Лимонад");
                var dish1 = db.Dishes.FirstOrDefault(s => s.Name == "Чай");
                var dish3 = db.Dishes.FirstOrDefault(s => s.Name == "Салат");
                listDishes.Add(2,dish2);
                listDishes.Add(1,dish1);
                listDishes.Add(3, dish3);

                Order order = new Order { Status = " В процесе" };
                //order.Add(listDishes);
                
                db.Orders.AddRange(order);
                order.ApplicationUserOrders.Add(new ApplicationUserOrder { ApplicationUser = usr, Order = order });
                db.SaveChanges();
                //Ищем Инградиенты для этого блюда ( с которого оно состоит и в каких пропорциях)
                foreach (var d in listDishes)
                {
                    // Условие хватает ли остатков этого Блюдa на *Складе* если нет то не создаем Order

                    if (d.Value.Remain > d.Key)
                    {
                        //Ищем этот инградиент в бд *на всякий случай для перестраховки , но думаю можно и без этого* 
                        var y = db.Dishes.Find(d.Value.Id);
                        //Если он есть то идем дальше
                        if (y != null)
                        {
                            Console.WriteLine("!!!  !!!");
                            //добавляем Заказу Блюда в промежуточную таблицу с указание количества бляд было потрацено чтобы обновить остатки*Склада*
                            y.DishOrders.Add(new DishOrder { Order=order, Dish=y, Quality = d.Key,DateTimeDishOrder = DateTime.Now});
                            Console.WriteLine("!!!  !!!");
                            // Console.WriteLine(" Блюдо  создано ... отнимаем  инградиенты : " + ingr.Name);
                            //Отнимаем от остатков в *Складе* потраченые количество инградиента
                            long num = d.Value.Remain - d.Key ;
                            //Обновляем остатки в *Складе* в бд
                            y.Remain = num;
                            //Записываем это все в бд
                            db.SaveChanges();
                        }

                        db.SaveChanges();
                        Console.WriteLine("!!! addDish  !!!");

                    }
                    else
                    {
                       // Console.WriteLine(" Блюдо не возможно создать ... не досточно инградиента : " + ingr.Name);
                    }

                }



            }
            
          
        }
        #endregion


        // Заказ
        #region 
        public void GetUserOrder()
        {
            var orders = new List<Order>();
            //Подключения к базе
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.WriteLine(" GET Order: ");
                //Запрос к базе
                var users = db.ApplicationUsers.Include(o => o.ApplicationUserOrders).ThenInclude(o=> o.Order).ThenInclude(o=>o.DishOrders).ThenInclude(d => d.Dish).ToList();
                foreach (var user in users)
                {

                    foreach (var order in user.ApplicationUserOrders.ToList())
                    {
                        
                        foreach (var dish in order.Order.DishOrders)
                        {
                            Console.WriteLine();
                            Console.WriteLine(user.UserName + " -> " + order.OrderId.ToString() + " -> " + order.Order.Status +" -> "+dish.Dish.Name);

                        }


                    }

                }

                //var usr = db.ApplicationUsers.Include(o => o.ApplicationUserOrders).ThenInclude(u => u.).FirstOrDefault(s => s.UserName == "Tom");

                




            }


        }
        #endregion


        //Метод загрузки данных с отношениям связей Многие-ко-Многим ef 3.0 **Пример**
        //https://metanit.com/sharp/entityframeworkcore/3.6.php
        #region
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


                Dish dish1 = new Dish { Name = "Чай", Category = category1, CountryOrigin = countryOrigin, Calories = 330, Weight = 500, Remain = 100 };
                Dish dish2 = new Dish { Name = "Салат", Category = category2, CountryOrigin = countryOrigin, Calories = 730, Weight = 200, Remain = 14 };
                Dish dish3 = new Dish { Name = "Лимонад", Category = category2, CountryOrigin = countryOrigin, Calories = 730, Weight = 200, Remain = 14 };
                db.Dishes.AddRange(dish1, dish2, dish3);

                ingredient1.DishIngredients.Add(new DishIngredient { Dish = dish3, Ingredient = ingredient1, quantityIngredient = 250, DishIngredientDate = DateTime.Now });



                ingredient1.DishIngredients.Add(new DishIngredient { Dish = dish1, quantityIngredient = 250, DishIngredientDate = DateTime.Now });
                ingredient2.DishIngredients.Add(new DishIngredient { Dish = dish1, quantityIngredient = 50, DishIngredientDate = DateTime.Now });
                ingredient4.DishIngredients.Add(new DishIngredient { Dish = dish1, quantityIngredient = 10, DishIngredientDate = DateTime.Now });

                ingredient2.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 10, DishIngredientDate = DateTime.Now });
                ingredient3.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 250, DishIngredientDate = DateTime.Now });
                ingredient4.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 90, DishIngredientDate = DateTime.Now });
                ingredient5.DishIngredients.Add(new DishIngredient { Dish = dish2, quantityIngredient = 80, DishIngredientDate = DateTime.Now });


                db.SaveChanges();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! addDish");


            }
        }
        #endregion
        //Метод загрузки данных с отношениям связей Многие-ко-Многим ef 5.0 **Пример**
        //https://metanit.com/sharp/entityframeworkcore/3.6.php
        #region
        public void addAllDish_ef_5_0()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Category category1 = new Category { Name = "Напитки" };
                Category category2 = new Category { Name = "Закуски" };
                db.Categories.AddRange(category1, category2);

                CountryOrigin countryOrigin = new CountryOrigin { Name = "Европейская" };
                db.CountryOrigins.AddRange(countryOrigin);

                Ingredient_ef_5_0 ingredient1 = new Ingredient_ef_5_0 { Name_ef_5_0 = "вода" };
                Ingredient_ef_5_0 ingredient2 = new Ingredient_ef_5_0 { Name_ef_5_0 = "сахар" };
                Ingredient_ef_5_0 ingredient3 = new Ingredient_ef_5_0 { Name_ef_5_0 = "морква" };
                Ingredient_ef_5_0 ingredient4 = new Ingredient_ef_5_0 { Name_ef_5_0 = "Лимон" };
                Ingredient_ef_5_0 ingredient5 = new Ingredient_ef_5_0 { Name_ef_5_0 = "капуста" };
                db.Ingredients_ef_5_0.AddRange(ingredient1, ingredient2, ingredient3, ingredient4, ingredient5);


                Dish_ef_5_0 dish1 = new Dish_ef_5_0 { Name = "Limonad", Category = category1, CountryOrigin = countryOrigin, Calories = 330, Weight = 500, Remain = 100 };
                Dish_ef_5_0 dish2 = new Dish_ef_5_0 { Name = "Салат", Category = category2, CountryOrigin = countryOrigin, Calories = 730, Weight = 200, Remain = 14 };
                Dish_ef_5_0 dish3 = new Dish_ef_5_0 { Name = "Газировка", Category = category2, CountryOrigin = countryOrigin, Calories = 730, Weight = 200, Remain = 14 };
                db.Dishes_ef_5_0.AddRange(dish1, dish2, dish3);

                //первый вариант добавления данных  работает только при модели мани то мани ef 5.0
                ingredient1.Dishes_ef_5_0.Add(dish3);

                //Второй вариант добавления данных работает в мододелях мани то мани ef 5.0 и ef 3.0
                ingredient1.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish1, Ingredient_ef_5_0 = ingredient1,  quantityIngredient_ef_5_0 = 250, DishIngredientDate_ef_5_0 = DateTime.Now });
                ingredient2.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish1, Ingredient_ef_5_0 = ingredient2, quantityIngredient_ef_5_0 = 50, DishIngredientDate_ef_5_0 = DateTime.Now });
                ingredient4.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish1, Ingredient_ef_5_0 = ingredient4, quantityIngredient_ef_5_0 = 10, DishIngredientDate_ef_5_0 = DateTime.Now });

                ingredient2.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish2, Ingredient_ef_5_0 = ingredient2, quantityIngredient_ef_5_0 = 10, DishIngredientDate_ef_5_0 = DateTime.Now });
                ingredient3.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish2, Ingredient_ef_5_0 = ingredient3, quantityIngredient_ef_5_0 = 250, DishIngredientDate_ef_5_0 = DateTime.Now });
                ingredient4.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish2, Ingredient_ef_5_0 = ingredient4, quantityIngredient_ef_5_0 = 90, DishIngredientDate_ef_5_0 = DateTime.Now });
                ingredient5.DishIngredients_ef_5_0.Add(new DishIngredient_ef_5_0 { Dish_ef_5_0 = dish2, Ingredient_ef_5_0 = ingredient5, quantityIngredient_ef_5_0 = 80, DishIngredientDate_ef_5_0 = DateTime.Now });

                 db.SaveChanges();
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! addDish");

            }
        }
        #endregion
        //Метод выгрузки данных с отношениям связей Многие-ко-Многим ef 5.0 **Пример**
        //https://metanit.com/sharp/entityframeworkcore/3.6.php
        #region
        public void GetAllDish_ef_5_0()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.WriteLine("...выводим");

                var dishess = db.Dishes_ef_5_0.Include(c => c.DishIngredients_ef_5_0).ThenInclude(s=>s.Ingredient_ef_5_0).ToList();
                // выводим все курсы
                foreach (var c in dishess)
                {
                    try
                    {
                        Console.WriteLine($"Course: {c.Name}");
                        // выводим всех студентов для данного кура
                        foreach (var s in c.DishIngredients_ef_5_0)
                            Console.WriteLine($"Name: {s.Ingredient_ef_5_0.Name_ef_5_0}  Date: {s.DishIngredientDate_ef_5_0.ToShortDateString()}  Mark: {s.quantityIngredient_ef_5_0}");
                        Console.WriteLine("-------------------");

                    }
                    catch
                    {
                        Console.WriteLine("...error");

                    }
                   
                }
                Console.WriteLine("...Закончили вывод");

            }
        }

        #endregion

    }
}
