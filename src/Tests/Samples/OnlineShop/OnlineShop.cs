using System;
using FluentDefense;

namespace Tests.Samples.OnlineShop;

public class OnlineShop
{
    public Order OrderItem(int itemId, int percentDiscount, DateTime orderDate, DateTime deliveryDate)
    {
        itemId.Defend()
            .NotZero()
            .Custom(i => i.ToString().Length == 5, (_, _) => "Order ids must be five digits")
            .NotNegative()
            .IsEven()
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