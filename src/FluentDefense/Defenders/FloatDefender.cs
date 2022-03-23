using System;
using System.Diagnostics;

namespace FluentDefense.Defenders;

public class FloatDefender : DefenderBase
{
    private string _parameterName;
    private float? _num;

    public FloatDefender(float? num, string parameterName) : base(parameterName)
    {
        _num = num;
        _parameterName = parameterName;
    }
        
    public FloatDefender NotNull()
    {
        if (!_num.HasValue)
        {
            AddError($"{_parameterName} cannot be null.");
        }

        return this;
    }

    public FloatDefender NotZero()
    {
        if (_num == 0)
        {
            AddError($"{_parameterName} cannot be zero.");
        }

        return this;
    }

    public FloatDefender NotNegative()
    {
        if (_num < 0)
        {
            AddError($"{_parameterName} cannot be negative.");
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
        if (_num < minValue)
        {
            AddError($"{_parameterName} value is below the minimum value of {minValue}");
        }

        return this;
    }

    public FloatDefender Max(float maxValue)
    {
        if (_num > maxValue)
        {
            AddError($"{_parameterName} value is above the maximum value of {maxValue}");
        }

        return this;
    }

    public FloatDefender Custom(Func<float?, bool> test, string messageTemplate)
    {
        Debug.Assert(test != null, nameof(test) + " != null");
        if (!test.Invoke(_num))
        {
            AddError(string.Format(messageTemplate, _num));
        }

        return this;
    }
}