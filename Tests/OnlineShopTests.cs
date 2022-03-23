using System;
using Xunit;

namespace Tests;

public class OnlineShopTests
{
    [Fact]
    public void TestOnlineShop()
    {
        var onlineShop = new OnlineShop();
        onlineShop.OrderItem(2350, 15, DateTime.UtcNow, DateTime.UtcNow);

    }
}