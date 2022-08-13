using System.Collections.Generic;
using FluentDefense;
using JetBrains.Annotations;

namespace Tests.Samples.OnlineShop.CountryCode;

public class CountryCodeDefender : DefenderBase<CountryCodeDefender, CountryCode>
{
    private static readonly List<CountryCode> ShippableCountries = new()
    {
        new("ZA"),
        new("UK"),
        new("US"),
    };

    public CountryCodeDefender([NotNull] string parameterName, CountryCode value) : base(parameterName, value)
    {
    }

    public CountryCodeDefender CanShipTo()
    {
        if (!ShippableCountries.Contains(Value))
        {
            AddError($"We cannot ship to {Value}");
        }

        return this;
    }
}