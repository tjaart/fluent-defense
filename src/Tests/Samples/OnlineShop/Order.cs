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
        this.Defend().LateForDelivery();
        Price.Defend()
            .Min(10)
            .WhenFail((value, name) => throw new Exception($"I can throw my very own exception: {value}:{name}"));
    }
}