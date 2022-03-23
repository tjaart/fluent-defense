using System.Diagnostics;

namespace FluentDefense.Defenders;

public class LongDefender : DefenderBase<LongDefender, long?>
{
    public LongDefender(long? value, string parameterName) : base(parameterName, value)
    {
    }

    public LongDefender NotNull()
    {
        if (!Value.HasValue)
        {
            AddError($"{ParameterName} cannot be null.");
        }

        return this;
    }

    public LongDefender NotZero()
    {
        if (Value == 0)
        {
            AddError($"{ParameterName} cannot be zero.");
        }

        return this;
    }

    public LongDefender NotNegative()
    {
        if (Value < 0)
        {
            AddError($"{ParameterName} cannot be negative.");
        }

        return this;
    }

    public LongDefender InRange(long rangeStart, long rangeEnd)
    {
        Debug.Assert(rangeEnd > rangeStart, "rangeEnd > rangeStart");
        Min(rangeStart);
        Max(rangeEnd);

        return this;
    }

    public LongDefender Min(long minValue)
    {
        if (Value < minValue)
        {
            AddError($"{ParameterName} value is below the minimum value of {minValue}");
        }

        return this;
    }

    public LongDefender Max(long maxValue)
    {
        if (Value > maxValue)
        {
            AddError($"{ParameterName} value is above the maximum value of {maxValue}");
        }

        return this;
    }
}