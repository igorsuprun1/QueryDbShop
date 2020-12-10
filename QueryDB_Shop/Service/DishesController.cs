
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueryDB_Shop.Service
{

    
    
    public class DishesController 
    {
        //private readonly ApplicationDbContext _appDbContext;
        //public DishesController(ApplicationDbContext appDbContext)
        //{
        //    _appDbContext = appDbContext;
        //}

        
        // GET: api/<DishesController>
        //[HttpGet]
        public void Get()
        {
            ////Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! GET GET GET GET");
            //List<SharedDish> _result = new List<SharedDish>();
            ////List<Dish> _dishes = _appDbContext.Dishes.Include(c => c.Category).Include(c => c.CountryOrigin).ToList();
            ////foreach (Dish _dish in _dishes)
            ////{

            ////    ///_result.Add(_dish.ToShared());
            ////    Console.WriteLine("******");
            ////    Console.WriteLine(_result.Count.ToString());
            ////}
            //_result.Add(new SharedDish { Name = "sdf", CountryOriginName = "wer", Weight = 2 });
            //return _result;
        }

        //// GET api/<DishesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<DishesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<DishesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DishesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
