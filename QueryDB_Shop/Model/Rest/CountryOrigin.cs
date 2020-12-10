using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QueryDB_Shop.Model.Rest
{
    public class CountryOrigin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        public ICollection<Dish> Dishes { get; set; }

        //public ICollection<Category> Categories { get; set; }
    }
}
