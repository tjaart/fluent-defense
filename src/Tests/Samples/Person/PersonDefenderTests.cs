using Xunit;

namespace Tests.Samples.Person;

public class PersonDefenderTests
{
    [Fact]
    public void TestPersonDefender()
    {
        var applicant = new Person();

        applicant.Defend()
            .HasEnoughInfoProvided()
            .Throw();
    }
}