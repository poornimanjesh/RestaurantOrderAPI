using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurentController : ControllerBase
    {
        // GET: api/<RestaurentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RestaurentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string jsonsave = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/order.json");

            var savedOrder = JsonSerializer.Deserialize<Order>(jsonsave);



            return jsonsave;
        }

        // POST api/<RestaurentController>
        [HttpPost]
        public decimal Post([FromBody] Orders orders)
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/orders.json",string.Empty);

            Orders savedOrder = null;
            decimal finalBill = 0;
            string jsonsave = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/orders.json");

            if(!string.IsNullOrEmpty(jsonsave))
             savedOrder = JsonSerializer.Deserialize<Orders>(jsonsave);

            if (savedOrder == null)
            {
                savedOrder = new Orders();
                savedOrder.OrderList = new List<Order>();
            }

            foreach (var order in orders.OrderList)
            {

                Restaurant restaurant = new Restaurant();
                var total2 = Restaurant.CalculateTotal(order);
                order.TotalPrice = total2;
                finalBill = total2;
                savedOrder.OrderList.Add(order);
            }

            var json = JsonSerializer.Serialize(savedOrder);
            var loc = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "/Orders.json", json);
            return finalBill;
        }

        // PUT api/<RestaurentController>/5
        [HttpPut("{id}")]
        public decimal Put(int id, [FromBody] Order order)
        {
            Orders savedOrder = null;
            decimal finalCost = 0.0M;
            string jsonsave = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/orders.json");
            if (!string.IsNullOrEmpty(jsonsave))
                 savedOrder = JsonSerializer.Deserialize<Orders>(jsonsave);

            if (savedOrder == null)
            {
                savedOrder = new Orders();
                savedOrder.OrderList = new List<Order>();
            }

            var orderfromDB = savedOrder.OrderList.Find(o => o.Number == id);

            //clear menu if update menuitem
            if (order.IsUpdate == true)
                orderfromDB.MenuItems.Clear();

            foreach (MenuItem orderMenuItem in order.MenuItems)
            {
                orderfromDB.MenuItems.Add(orderMenuItem);
            }

            if (order.IsUpdate == false)
            {
                var originalCost = orderfromDB.TotalPrice;

                Restaurant restaurant = new Restaurant();
                var total2 = Restaurant.CalculateTotal(order);

                var updatedCost = total2;

                finalCost = updatedCost + originalCost;
            }
            else 
            {
                finalCost =  Restaurant.CalculateTotal(order);
            }
            return finalCost;
        }
        

        // DELETE api/<RestaurentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
