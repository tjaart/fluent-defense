using System;
using FluentDefense;
using Xunit;

namespace Tests;

public class TestDateDefender
{
    [Fact]
    public void TestNotDefault()
    {
        var dt = DateTime.Now;
        var errors = dt
            .Defend(nameof(dt))
            .NotDefault()
            .Errors;

        Assert.Empty(errors
        );
    }

    [Fact]
    public void TestNotDefaultError()
    {
        var dt = new DateTime();
        var errors = dt
            .Defend(nameof(dt))
            .NotDefault()
            .Errors;

        Assert.NotEmpty(errors
        );
    }
}