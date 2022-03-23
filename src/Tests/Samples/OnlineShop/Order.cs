using System;
using FluentDefense;
using Tests.Samples.CountryCode;

namespace Tests.Samples.OnlineShop;

public class Order
{
    public DateTime DeliveryDateUtc { get; set; }

    public double Price { get; set; }
    public bool Delivered { get; set; }

    public CountryCode.CountryCode CountryCode { get; set; }

    public void Cancel()
    {
        Price.Defend()
            .Min(10)
            .WhenFail((value, name) => throw new Exception($"I can throw my very own exception: {value}:{name}"));
    }

    public void FullFill()
    {
        CountryCode.Defend()
            .CanShipTo();
        // rest goes here
    }
}