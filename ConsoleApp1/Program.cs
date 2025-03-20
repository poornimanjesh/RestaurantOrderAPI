using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Caching;
using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {

        string jsonsave = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/order.json");

        var savedOrder = JsonSerializer.Deserialize<Order>(jsonsave);

        
        foreach (var item in savedOrder.MenuItems) 
        {
            item.Quantity = item.Quantity-1;
        }

        //var itemtoremove = savedOrder.MenuItems.First();

        //savedOrder.MenuItems.Remove(itemtoremove);


        //var cache = new System.Runtime.Caching.MemoryCache("MyTestCache");

        //if (cache["ObjectList"] != null)
        //{
        //  var existingOrder = (Order)cache["ObjectList"];
        //}
        Console.WriteLine("Hello, World!");

        Order order2 = new Order() { Number = 2, OrderTime = DateTime.Now, ServiceCharge = 10 };
        Menu menu2 = new Menu();
        order2.MenuItems = new List<MenuItem>();
        order2.MenuItems.Add(new MenuItem() { Name = "Starter", Price = menu2.Starter, Quantity = 4 });
        order2.MenuItems.Add(new MenuItem() { Name = "Main", Price = menu2.Main, Quantity=4 });
        order2.MenuItems.Add(new MenuItem() { Name = "Drinks", Price = menu2.Drinks, Quantity = 4 });
        order2.OrderTime = DateTime.Now;
        Restaurant restaurant = new Restaurant();
        var total2 = Restaurant.CalculateTotal(order2);

        Console.WriteLine("Total amount of the order2 is: "+total2);


        var json = JsonSerializer.Serialize(order2);
        var loc = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory+"/Order.json", json);

        //cache["ObjectList"] = order2;                 // add
        //order2 = (Order)cache["ObjectList"]; // retrieve
       // cache.Remove("ObjectList");


    }
    
}


