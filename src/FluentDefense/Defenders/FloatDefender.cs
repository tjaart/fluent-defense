using System;
using System.Diagnostics;

namespace FluentDefense.Defenders;

public class FloatDefender : DefenderBase<FloatDefender, float?>
{
    public FloatDefender(float? value, string parameterName) : base(parameterName, value)
    {
    }

    public FloatDefender NotNull()
    {
        if (!Value.HasValue)
        {
            AddError($"{ParameterName} cannot be null.");
        }

        return this;
    }

    public FloatDefender NotZero()
    {
        if (Math.Abs(Value.GetValueOrDefault()) < Double.Epsilon)
        {
            AddError($"{ParameterName} cannot be zero.");
        }

        return this;
    }

    public FloatDefender NotNegative()
    {
        if (Value < 0)
        {
            AddError($"{ParameterName} cannot be negative.");
        }

        return this;
    }

    public FloatDefender InRange(float rangeStart, float rangeEnd)
    {
        Debug.Assert(rangeEnd > rangeStart, "rangeEnd > rangeStart");
        Min(rangeStart);
        Max(rangeEnd);

        return this;
    }

    public FloatDefender Min(float minValue)
    {
        if (Value < minValue)
        {
            AddError($"{ParameterName} value is below the minimum value of {minValue}");
        }

        return this;
    }

    public FloatDefender Max(float maxValue)
    {
        if (Value > maxValue)
        {
            AddError($"{ParameterName} value is above the maximum value of {maxValue}");
        }

        return this;
    }
}