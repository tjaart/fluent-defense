using System;
using FluentAssertions;
using FluentDefense;
using Xunit;

namespace Tests;

public class GuidDefenderTests
{
    [Fact]
    public void NotNullOrEmpty_Empty_Throws()
    {
        var customerId = Guid.Empty;

        FluentActions.Invoking(() =>
            {
                customerId.Defend()
                    .NotNullOrEmpty()
                    .Throw();
            })
            .Should()
            .ThrowExactly<ArgumentException>()
            .Which
            .ParamName
            .Should()
            .Be(nameof(customerId));
    }
}