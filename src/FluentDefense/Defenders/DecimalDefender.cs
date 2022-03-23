using System.Diagnostics;

namespace FluentDefense.Defenders;

public class DecimalDefender : DefenderBase<DecimalDefender, decimal?>
{
    public DecimalDefender(decimal? num, string parameterName) : base(parameterName, num)
    {
    }

    public DecimalDefender NotNull()
    {
        if (!Value.HasValue)
        {
            AddError($"{ParameterName} cannot be null.");
        }

        return this;
    }

    public DecimalDefender NotZero()
    {
        if (Value == 0)
        {
            AddError($"{ParameterName} cannot be zero.");
        }

        return this;
    }

    public DecimalDefender NotNegative()
    {
        if (Value < 0)
        {
            AddError($"{ParameterName} cannot be negative.");
        }

        return this;
    }

    public DecimalDefender InRange(decimal rangeStart, decimal rangeEnd)
    {
        Debug.Assert(rangeEnd > rangeStart, "rangeEnd > rangeStart");
        Min(rangeStart);
        Max(rangeEnd);

        return this;
    }

    public DecimalDefender Min(decimal minValue)
    {
        if (Value < minValue)
        {
            AddError($"{ParameterName} value is below the minimum value of {minValue}");
        }

        return this;
    }

    public DecimalDefender Max(decimal maxValue)
    {
        if (Value > maxValue)
        {
            AddError($"{ParameterName} value is above the maximum value of {maxValue}");
        }

        return this;
    }

    // public DecimalDefender Custom(Func<decimal?, bool> test, string messageTemplate)
    // {
    //     Debug.Assert(test != null, nameof(test) + " != null");
    //     if (!test.Invoke(_num))
    //     {
    //         AddError(string.Format(messageTemplate, _num));
    //     }
    //
    //     return this;
    // }
}