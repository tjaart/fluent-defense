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
            .IsEven()
            .Custom(i => i.ToString().Length == 5, (_, _) => "Order ids must be five digits")
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
    public static IntDefender IsEven(this IntDefender defender)
    {
        defender.Custom(value => value % 2 == 0, (value, name) => $"{value} for {name} is not even");
        return defender;
    }
}