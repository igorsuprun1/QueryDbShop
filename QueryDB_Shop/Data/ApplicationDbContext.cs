using Microsoft.EntityFrameworkCore;
using QueryDB_Shop.Model;
using QueryDB_Shop.Model.Rest;
using System.Collections.Generic;
using System;

namespace QueryDB_Shop.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Microsoft.EntityFrameworkCore
        //Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)
        //Microsoft.EntityFrameworkCore.Tools(необходим для создания классов по базе данных, то есть reverse engineering)
        
        //Конструктор класа
        public ApplicationDbContext()
        {
            // пересоздадим базу данных
           // Database.EnsureDeleted();
            // создать базу данных
            //Database.EnsureCreated();
        }

        //Конструктор класа
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        //Конфигурация подключения к базе данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-BlazorRestModule.Server-0B554AFB-B141-4716-BA15-E13EF128FD8C;Trusted_Connection=True;");
            }
            else
            {
                Console.WriteLine("optionsBuilder.IsConfigured");
            }
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<CountryOrigin> CountryOrigins { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
     
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<DishOrder> DishOrders { get; set; }

        public DbSet<Dish_ef_5_0> Dishes_ef_5_0 { get; set; }
        public DbSet<Ingredient_ef_5_0> Ingredients_ef_5_0 { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Связь многие ко многим FluentAPI У пользователя много заказов и у заказа может быть много пользователей
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserOrder>()
                .HasKey(x => new { x.ApplicationUserId, x.OrderId });
            builder.Entity<ApplicationUserOrder>()
                .HasOne(x => x.ApplicationUser)
                .WithMany(x => x.ApplicationUserOrders)
                .HasForeignKey(x => x.ApplicationUserId);
            builder.Entity<ApplicationUserOrder>()
                .HasOne(x => x.Order)
                .WithMany(x => x.ApplicationUserOrders)
                .HasForeignKey(x => x.OrderId);

            //Связь многие ко многим FluentAPI У пользователя много заказов и у заказа может быть много пользователей
            base.OnModelCreating(builder);
            builder.Entity<DishOrder>()
                .HasKey(x => new { x.DishId, x.OrderId });
            builder.Entity<DishOrder>()
                .HasOne(x => x.Dish)
                .WithMany(x => x.DishOrders)
                .HasForeignKey(x => x.DishId);
            builder.Entity<DishOrder>()
                .HasOne(x => x.Order)
                .WithMany(x => x.DishOrders)
                .HasForeignKey(x => x.OrderId);

            //Связь один  ко многим FluentAPI У Страны происхождения есть много категорий
            builder.Entity<Dish>()
                .HasOne<CountryOrigin>(a => a.CountryOrigin)
                .WithMany(b => b.Dishes)
                .HasForeignKey(c => c.CountryOriginId);

            //Связь один  ко многим FluentAPI У Категории есть много Блюд
            builder.Entity<Dish>()
                .HasOne<Category>(a => a.Category)
                .WithMany(b => b.Dishes)
                .HasForeignKey(c => c.CategoryId);

            //Связь Многие  ко многим EntityFramework Core 3.0 FluentAPI У Блюда  есть много Ингридиентов
            //https://metanit.com/sharp/entityframeworkcore/3.6.php

            base.OnModelCreating(builder);
            builder.Entity<DishIngredient>()
                .HasKey(x => new { x.IngredientId, x.DishId });
            builder.Entity<DishIngredient>()
                .HasOne<Dish>(x => x.Dish)
                .WithMany(x => x.DishIngredients)
                .HasForeignKey(x => x.DishId);
            builder.Entity<DishIngredient>()
                .HasOne<Ingredient>(x => x.Ingredient)
                .WithMany(x => x.DishIngredients)
                .HasForeignKey(x => x.IngredientId);

            //Связь Многие  ко многим EntityFramework Core 5.0  FluentAPI У Блюда есть много Ингридиентов WORK!!!!!!! 
            //https://metanit.com/sharp/entityframeworkcore/3.6.php

            //base.OnModelCreating(builder);
            //builder.Entity<Dish>()
            //    .HasMany(c => c.Ingredients)
            //    .WithMany(s => s.Dishs)
            //    .UsingEntity < DishIngredient > (
            //       j => j
            //        .HasOne(pt => pt.Ingredient)
            //        .WithMany(t => t.DishIngredients)
            //        .HasForeignKey(pt => pt.IngredientId),
            //    j => j
            //        .HasOne(pt => pt.Dish)
            //        .WithMany(p => p.DishIngredients)
            //        .HasForeignKey(pt => pt.DishId),
            //    j =>
            //    {
            //        j.Property(pt => pt.quantityIngredient).HasDefaultValue("3");
            //        j.HasKey(t => new { t.DishId, t.IngredientId });
            //        j.ToTable("DishIngredient");
            //    }
            //);

            //Связь Многие  ко многим EntityFramework Core 5.0  FluentAPI У Блюда есть много Ингридиентов WORK!!!!!!! 
            //https://metanit.com/sharp/entityframeworkcore/3.6.php

            base.OnModelCreating(builder);
            builder.Entity<Dish_ef_5_0>()
                .HasMany(c => c.Ingredients_ef_5_0)
                .WithMany(s => s.Dishes_ef_5_0)
                .UsingEntity<DishIngredient_ef_5_0>(
                   j => j
                    .HasOne(pt => pt.Ingredient_ef_5_0)
                    .WithMany(t => t.DishIngredients_ef_5_0)
                    .HasForeignKey(pt => pt.IngredientId_ef_5_0),
                j => j
                    .HasOne(pt => pt.Dish_ef_5_0)
                    .WithMany(p => p.DishIngredients_ef_5_0)
                    .HasForeignKey(pt => pt.DishId_ef_5_0),
                j =>
                {
                    j.Property(pt => pt.quantityIngredient_ef_5_0).HasDefaultValue("3");
                    j.HasKey(t => new { t.DishId_ef_5_0, t.IngredientId_ef_5_0 });
                    j.ToTable("DishIngredient_ef_5_0");
                }
            );




        }



    }
}
