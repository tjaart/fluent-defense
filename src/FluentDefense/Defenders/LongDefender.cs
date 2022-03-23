using System;
using System.Diagnostics;

namespace FluentDefense.Defenders;

public class LongDefender : DefenderBase
{
    private string _parameterName;
    private long? _num;

    public LongDefender(long? num, string parameterName) : base(parameterName)
    {
        _num = num;
        _parameterName = parameterName;
    }
        
    public LongDefender NotNull()
    {
        if (!_num.HasValue)
        {
            AddError($"{_parameterName} cannot be null.");
        }

        return this;
    }

    public LongDefender NotZero()
    {
        if (_num == 0)
        {
            AddError($"{_parameterName} cannot be zero.");
        }

        return this;
    }

    public LongDefender NotNegative()
    {
        if (_num < 0)
        {
            AddError($"{_parameterName} cannot be negative.");
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
        if (_num < minValue)
        {
            AddError($"{_parameterName} value is below the minimum value of {minValue}");
        }

        return this;
    }

    public LongDefender Max(long maxValue)
    {
        if (_num > maxValue)
        {
            AddError($"{_parameterName} value is above the maximum value of {maxValue}");
        }

        return this;
    }

    public LongDefender Custom(Func<long?, bool> test, string messageTemplate)
    {
        Debug.Assert(test != null, nameof(test) + " != null");
        if (!test.Invoke(_num))
        {
            AddError(string.Format(messageTemplate, _num));
        }

        return this;
    }
}