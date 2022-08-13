using System.Runtime.CompilerServices;

namespace Tests.Samples.Person;

public static class PersonDefenderExtensions
{
    public static PersonDefender Defend(this Person person, [CallerArgumentExpression("person")] string parameterName = "")
        => new(parameterName, person);
}