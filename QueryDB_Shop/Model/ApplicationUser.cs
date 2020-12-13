
using QueryDB_Shop.Model.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QueryDB_Shop.Model
{
    public class ApplicationUser 
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public ICollection<ApplicationUserOrder> ApplicationUserOrders { get; set; } = new List<ApplicationUserOrder>();

        // https://metanit.com/sharp/mvc5/12.3.php
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    return userIdentity;
        //}
    }
}
