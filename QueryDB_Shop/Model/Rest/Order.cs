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

        public ICollection<DishOrder> DishOrders { get; set; }

        public ICollection<ApplicationUserOrder> ApplicationUserOrders { get; set; }


        //public Dish Dish { get; set; }

        //public Order()
        //{
        //    DishOrders = new List<DishOrder>();
        //}



    }
}
