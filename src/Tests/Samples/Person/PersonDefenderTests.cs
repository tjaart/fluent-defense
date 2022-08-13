using System;
using FluentAssertions;
using Xunit;

namespace Tests.Samples.Person;

public class PersonDefenderTests
{
    [Fact]
    public void TestPersonDefender()
    {
        var applicant = new Person();

        var act = () => applicant.Defend()
            .HasEnoughInfoProvided()
            .Throw();

        act.Should().Throw<ArgumentException>()
            .WithMessage(@"applicant is invalid.
Name was not supplied for person.
'0' is not a valid age for person 'applicant'. Age must be between 1 and 120 years (Parameter 'applicant')");
    }
}