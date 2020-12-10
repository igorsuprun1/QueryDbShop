using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QueryDB_Shop.Model.Rest
{
    public class ApplicationUserOrder
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
