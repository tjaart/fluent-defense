using FluentDefense;

namespace Tests.Samples.OnlineShop.CountryCode;

public readonly record struct CountryCode
{
    public CountryCode(string code)
    {
        Code = code.Defend()
            .MatchesRegex("^[A-Z]{2}$")
            .ValueOrThrow();
    }

    public string Code { get; }
}