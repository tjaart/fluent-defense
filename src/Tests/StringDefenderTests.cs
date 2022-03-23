using FluentAssertions;
using FluentDefense;
using Xunit;

namespace Tests;

public class TestStringDefender
{
    [Fact]
    public void TestMinLength()
    {
        var stringParam = "12345";
        Assert.True(stringParam.Defend().MinLength(5).IsValid);
    }

    [Fact]
    public void TestEmail()
    {
        Assert.Equal(2, "12345".Defend("test").ValidEmail().Errors.Count);
    }

    [Fact]
    public void TestEmailValid()
    {
        Assert.True("john@doe.com".Defend("test").ValidEmail().IsValid);
    }

    [Fact]
    public void ValidUri_ValidInput_ReturnsTrue()
    {
        "http://www.google.com".Defend("test")
            .ValidUri()
            .IsValid
            .Should()
            .BeTrue("Because Uri is valid");
    }

    [Fact]
    public void MatchesRegex_ValidInput_ReturnsTrue()
    {
        "az:45".Defend("test")
            .MatchesRegex("[a-z]{2}:[0-9]{2}")
            .IsValid
            .Should()
            .BeTrue("Because regex matches");
    }

    [Fact]
    public void MatchesRegex_InvalidInput_ReturnsFalse()
    {
        var stringDefender = "az-43".Defend()
            .MatchesRegex("[a-z]{2}:[0-9]{2}");

        stringDefender
            .IsValid
            .Should()
            .BeFalse("Because regex doesn't match");

        stringDefender.ErrorMessage
            .Should()
            .Contain("az-43", "Because that was the input expression");
    }
}