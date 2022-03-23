using System;

namespace FluentDefense.Defenders;

public class DateTimeDefender : DefenderBase<DateTimeDefender, DateTime?>
{
    private readonly DateTime? _value;

    public DateTimeDefender(DateTime? value, string parameterName) : base(parameterName, value)
    {
        _value = value;
    }

    public static Func<DateTime> Now { get; set; } = () => DateTime.Now;
    public static Func<DateTime> NowUtc { get; set; } = () => DateTime.UtcNow;

    public DateTimeDefender NotNull()
    {
        if (!_value.HasValue)
        {
            AddError($"{ParameterName} cannot be null");
        }

        return this;
    }

    public DateTimeDefender IsInFuture()
    {
        NotNull();
        if (_value == null || _value.Value <= Now())
        {
            AddError($"{_value} is not a future date.");
        }

        return this;
    }

    public DateTimeDefender IsInPast()
    {
        NotNull();
        if (_value == null || _value.Value >= DateTime.Now)
        {
            AddError($"{_value} is not a future date.");
        }

        return this;
    }

    public DateTimeDefender IsInFutureUtc()
    {
        NotNull();
        if (_value == null || _value.Value <= NowUtc())
        {
            AddError($"{_value} is not a UTC future date.");
        }

        return this;
    }

    public DateTimeDefender IsInPastUtc()
    {
        NotNull();
        if (_value == null || _value.Value >= NowUtc())
        {
            AddError($"{_value} is not a future date.");
        }

        return this;
    }

    public DateTimeDefender NotDefault()
    {
        NotNull();
        if (_value == null || _value.Value == default)
        {
            AddError($"{_value} was never initialized.");
        }

        return this;
    }
}