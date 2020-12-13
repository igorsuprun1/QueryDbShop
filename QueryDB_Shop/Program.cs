using Microsoft.EntityFrameworkCore;
using QueryDB_Shop.Data;
using QueryDB_Shop.Model.Rest;
using QueryDB_Shop.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryDB_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    // создаем два объекта User
            //    ApplicationUser user1 = new ApplicationUser { UserName = "Tom" };
            //    ApplicationUser user2 = new ApplicationUser { UserName = "Alice"};

            //    //добавляем их в бд
            //    db.ApplicationUsers.Add(user1);
            //    db.ApplicationUsers.Add(user2);
            //    db.SaveChanges();
            //    Console.WriteLine("Объекты успешно сохранены");

            //    // получаем объекты из бд и выводим на консоль

            //    Query query = new Query();

            //    query.addDish();
            //    Console.WriteLine("Объекты успешно сохранены");
            //    var users = query.GetUser();

            //    foreach (ApplicationUser u in users)
            //    {
            //        Console.WriteLine($"{u.id}.{u.UserName} ");
            //    }
            //}
            //List<Ingredient> listIngr = new List<Ingredient>();

            //Ingredient ingredient1 = new Ingredient { Name = "вода", AllquantityIngredient = 222 };
            //Ingredient ingredient2 = new Ingredient { Name = "сахар", AllquantityIngredient = 222 };
            //Ingredient ingredient3 = new Ingredient { Name = "яблока", AllquantityIngredient = 222 };
            //Ingredient ingredient4 = new Ingredient { Name = "Лимон", AllquantityIngredient = 222 };
            //listIngr.Add(ingredient1);
            //listIngr.Add(ingredient2);
            //listIngr.Add(ingredient3);
            //listIngr.Add(ingredient4);

            Query query = new Query();

            // query.AddUser();
            // query.GetUser();

            query.Order();


           // query.GetUserOrder();

           //query.addAllDish();

           // //query.addAllDish();
           // Console.WriteLine("...выводим  porgram.cs");
           // query.GetAllDishAll();

           // Console.WriteLine("...выводим  porgram.cs");
           // query.GetAllDish();


           // query.Order();
            //query.AddIngredient(listIngr);

            //listIngr.Clear();

            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    Ingredient ingredien = db.Ingredients.FirstOrDefault(c => c.Name == "вода");
            //    Ingredient ingredien2 = db.Ingredients.FirstOrDefault(c => c.Name == "Лимон");
            //    if (ingredien != null && ingredien2 != null)
            //    {
            //        listIngr.Add(ingredien);
            //        listIngr.Add(ingredien2);

            //    }

            //}
            //query.addDish(listIngr, 45);


            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    Console.WriteLine("...выводим");



            //    var dishess = db.Dishes.Include(c => c.DishIngredients).ToList();
            //    // выводим все курсы
            //    foreach (var c in dishess)
            //    {
            //        Console.WriteLine($"Блюда: {c.Name}");
            //        // выводим всех студентов для данного кура
            //        foreach (var s in c.DishIngredients)
            //        {
            //             Console.WriteLine($"Name: { s.Ingredient.Name}  Date: {s.DishIngredientDate.ToShortDateString()}  Mark: {s.quantityIngredient}");
            //        Console.WriteLine("-------------------");
            //        }


            //    }
            Console.WriteLine("...Закончили вывод");
            //}
            Console.Read();
        }
    }
}
