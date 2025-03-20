public class Restaurant
{
    public static decimal CalculateTotal(Order order)
    {
        decimal orderTotal = 0;

        foreach (var item in order.MenuItems)
        {
            decimal drinksPrice = 0;
            if (item.Name == "Drinks")
            {
                drinksPrice = 0;

                if (IsOrderEligebleForDiscount(order.OrderTime))
                    drinksPrice = GetDrinksDiscount(item.Price);
                else
                    drinksPrice = item.Price;

                orderTotal += drinksPrice * item.Quantity;
            }
            else
            {
                orderTotal += item.Price * item.Quantity;
            }
        }

        decimal serviceCharge = orderTotal / order.ServiceCharge;

        decimal total = orderTotal + serviceCharge;

        return total;
    }

    private static bool IsOrderEligebleForDiscount(DateTime orderHours)
    {
        bool isOrderEligeble = false;

        if (orderHours.Hour < 19)
            isOrderEligeble = true;

        return isOrderEligeble;
    }
    private static decimal GetDrinksDiscount(decimal drinks)
    {
        decimal total = 0;

        decimal drinksDiscount = 0.3M * drinks;//apply 30% discount on drinks before 7 pm

        total = drinks - drinksDiscount;

        return total;
    }
}


