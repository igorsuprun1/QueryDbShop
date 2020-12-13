using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QueryDB_Shop.Model.Rest
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Qty { get; set; }
        public string Status { get; set; }

        public ICollection<DishOrder> DishOrders { get; set; } = new List<DishOrder>();

        public ICollection<ApplicationUserOrder> ApplicationUserOrders { get; set; } = new List<ApplicationUserOrder>();
       

        // public ApplicationUser ApplicationUser { get; set; }
        //public Dish Dish { get; set; }

        //public Order()
        //{
        //    DishOrders = new List<DishOrder>();
        //}

        //public void Add(List<Dish> argDishes)
        //{
        //    argDishes
        //}

    }
}
