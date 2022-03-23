using System.Runtime.CompilerServices;
using FluentDefense;

namespace Tests.Samples.OnlineShop;

public class OrderDefender : DefenderBase
{
    private readonly Order _order;

    public OrderDefender(Order order, string parameterName) : base(parameterName)
    {
        _order = order;
    }

    public OrderDefender NotLateForDelivery()
    {
        if (_order.DeliveryDateUtc.Defend().IsInPastUtc().IsValid)
        {
            AddError("Delivery is late.");
        }

        return this;
    }
}

public static class OrderDefenderExtensions
{
    public static OrderDefender Defend(this Order order, [CallerArgumentExpression("order")] string parameterName = "")
    {
        return new OrderDefender(order, parameterName);
    }
}