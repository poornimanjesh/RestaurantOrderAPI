public class Order
{
    public int Number { get; set; }
    public decimal TotalPrice { get; set; }
    public int Discount { get; set; }
    public int ServiceCharge { get; set; }
    public DateTime OrderTime { get; set; }
    public List<MenuItem> MenuItems { get; set; }
    public bool IsUpdate { get; set; }
}


