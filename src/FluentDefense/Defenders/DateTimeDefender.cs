using System;

namespace FluentDefense.Defenders;

public class DateTimeDefender : DefenderBase<DateTimeDefender, DateTime?>
{
    public DateTimeDefender(DateTime? value, string parameterName) : base(parameterName, value)
    {
    }

    public static Func<DateTime> Now { get; set; } = () => DateTime.Now;
    public static Func<DateTime> NowUtc { get; set; } = () => DateTime.UtcNow;

    public DateTimeDefender NotNull()
    {
        if (!Value.HasValue)
        {
            AddError($"{ParameterName} cannot be null");
        }

        return this;
    }

    public DateTimeDefender IsInFuture()
    {
        NotNull();
        if (Value == null || Value.Value <= Now())
        {
            AddError($"{Value} is not a future date.");
        }

        return this;
    }

    public DateTimeDefender IsInPast()
    {
        NotNull();
        if (Value == null || Value.Value >= DateTime.Now)
        {
            AddError($"{Value} is not a future date.");
        }

        return this;
    }

    public DateTimeDefender IsInFutureUtc()
    {
        NotNull();
        if (Value == null || Value.Value <= NowUtc())
        {
            AddError($"{Value} is not a UTC future date.");
        }

        return this;
    }

    public DateTimeDefender IsInPastUtc()
    {
        NotNull();
        if (Value == null || Value.Value >= NowUtc())
        {
            AddError($"{Value} is not a future date.");
        }

        return this;
    }

    public DateTimeDefender NotDefault()
    {
        NotNull();
        if (Value == null || Value.Value == default)
        {
            AddError($"{Value} was never initialized.");
        }

        return this;
    }
}