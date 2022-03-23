using FluentDefense.Defenders;

namespace Tests.Samples.OnlineShop;

public static class TestIntDefenderExtension
{
    public static IntDefender IsEven(this IntDefender defender)
    {
        defender.Custom(value => value % 2 == 0, (value, name) => $"{value} for {name} is not even");
        return defender;
    }
}