using System;
using FluentDefense;
using FluentDefense.Defenders;

namespace Tests;

public class OnlineShop
{
    public Order OrderItem(int itemId, int percentDiscount, DateTime orderDate, DateTime deliveryDate)
    {
        itemId.Defend()
            .NotZero()
            .NotNegative()
            .DivisibleBy10()
            .Custom(i => i.ToString().Length == 5, "Order ids must be five digits")
            .Throw();

        orderDate.Defend()
            .NotDefault()
            .IsInPastUtc()
            .Throw();

        deliveryDate.Defend()
            .IsInFuture();

        return new Order
        {
            Price = 5
        };
    }

    
}

public class Order
{
    public DateTime DeliveryDateUtc { get; set; }

    public double Price { get; set; }
    public bool Delivered { get; set; }
    
    public void Cancel()
    {
        this.Defend().NotLateForDelivery();
        Price.Defend()
            .Min(10, (p, d) => $"Orders with {p} under & {d} cannot be cancelled");

    }
}

public static class TestIntDefenderExtension
{
    public static IntDefender DivisibleBy10(this IntDefender defender)
    {
        defender.Custom(i => i % 10 == 0, "{0} is not divisible by 10.");
        return defender;
    }
}