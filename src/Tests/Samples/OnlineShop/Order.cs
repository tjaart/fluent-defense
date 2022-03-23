using System;
using FluentDefense;

namespace Tests.Samples.OnlineShop;

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