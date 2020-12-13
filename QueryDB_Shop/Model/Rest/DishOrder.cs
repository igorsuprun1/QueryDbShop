using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryDB_Shop.Model.Rest
{
    public class DishOrder
    {

        public int DishId { get; set; }
        public Dish Dish { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Quality { get; set; }

        public DateTime ?DateTimeDishOrder { get; set; }
    }
}
