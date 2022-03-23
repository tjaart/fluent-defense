using System;
using System.Diagnostics;

namespace FluentDefense.Defenders;

public class DoubleDefender : DefenderBase<DoubleDefender, double?>
{
    public DoubleDefender(double? value, string parameterName) : base(parameterName, value)
    {
    }

    public DoubleDefender NotNull()
    {
        if (!Value.HasValue)
        {
            AddError($"{ParameterName} cannot be null.");
        }

        return this;
    }

    public DoubleDefender NotZero()
    {
        if (Value == 0)
        {
            AddError($"{ParameterName} cannot be zero.");
        }

        return this;
    }

    public DoubleDefender NotNegative()
    {
        if (Value < 0)
        {
            AddError($"{ParameterName} cannot be negative.");
        }

        return this;
    }

    public DoubleDefender InRange(double rangeStart, double rangeEnd)
    {
        Debug.Assert(rangeEnd > rangeStart, "rangeEnd > rangeStart");
        Min(rangeStart);
        Max(rangeEnd);

        return this;
    }

    public DoubleDefender Min(double minValue, Func<string, double, string>? customMessage = null)
    {
        if (Value < minValue)
        {
            AddError($"{ParameterName} value is below the minimum value of {minValue}");
        }

        return this;
    }

    public DoubleDefender Max(double maxValue)
    {
        if (Value > maxValue)
        {
            AddError($"{ParameterName} value is above the maximum value of {maxValue}");
        }

        return this;
    }
}