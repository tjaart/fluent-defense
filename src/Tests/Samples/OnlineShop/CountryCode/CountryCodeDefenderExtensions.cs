using System.Runtime.CompilerServices;

namespace Tests.Samples.OnlineShop.CountryCode;

public static class CountryCodeDefenderExtensions
{
    public static CountryCodeDefender Defend(this CountryCode countryCode, [CallerArgumentExpression("countryCode")] string parameterName = "")
        => new CountryCodeDefender(parameterName, countryCode);
}