using System;
using FluentAssertions;
using FluentDefense;
using Tests.Samples.OnlineShop;
using Xunit;

namespace Tests;

public class TestIntDefender
{
    [Fact]
    public void TestNotZero()
    {
        Assert.Throws<ArgumentException>(() => 0.Defend("test").NotZero().Throw());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(3, 5)]
    public void TestMin(int num, int min)
    {
        Assert.Throws<ArgumentException>(() => num.Defend("test").Min(min).Throw());
    }


    [Theory]
    [InlineData(0, -1, 5)]
    [InlineData(3, 1, 20)]
    public void TestRange(int num, int min, int max)
    {
        num.Defend("test").InRange(min, max).Throw();
    }

    [Fact]
    public void TestInvalidRange()
    {
        Assert.ThrowsAny<Exception>(() => 5.Defend("asd").InRange(5, 4).Throw());
    }

    [Fact]
    public void CustomCheck()
    {
        var result = 35.Defend()
            .IsEven()
            .IsValid
            .Should()
            .BeFalse("Because number is not even");
    }

    [Fact]
    public void ValueOrThrow_ValidValue_ReturnsValue()
    {
        var value = 40.Defend()
            .InRange(20, 50)
            .ValueOrThrow();

        value.Should().Be(40);
    }

    [Fact(DisplayName = "ValueOrThrow with Invalid Value Throws Argument Exception")]
    public void ValueOrThrow_InvalidValue_ThrowsException()
    {
        FluentActions.Invoking(() =>
            {
                var value = 60.Defend()
                    .InRange(20, 50)
                    .ValueOrThrow();
            })
            .Should()
            .ThrowExactly<ArgumentException>("Because the value was outside the range");
    }

    [Fact(DisplayName = "WhenFail with Invalid Value Calls Failure Action")]
    public void WhenFail_InvalidValue_CallsFailureAction()
    {
        var checkValue = "";
        var i1 = 60;
        var value = i1.Defend()
            .InRange(20, 50)
            .WhenFail((i, name) => checkValue = $"{i}:{name}");

        checkValue.Should().Be("60:i1");
    }
}