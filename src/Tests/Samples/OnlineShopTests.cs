using System;
using Xunit;

namespace Tests.Samples;

public class OnlineShopTests
{
    [Fact]
    public void TestOnlineShop()
    {
        var onlineShop = new OnlineShop.OnlineShop();
        onlineShop.OrderItem(12350, 15, DateTime.UtcNow, DateTime.UtcNow);
    }
}