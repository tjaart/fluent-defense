using System;

namespace FluentDefense.Defenders
{
    public class DateTimeDefender : DefenderBase
    {
        private readonly DateTime? _value;
        
        public static Func<DateTime> MomentGenerator { get; set; } = () => DateTime.Now;
        public static Func<DateTime> MomentGeneratorUtc { get; set; } = () => DateTime.UtcNow;

        public DateTimeDefender(DateTime? value, string parameterName) : base(parameterName)
        {
            _value = value;
        }

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
            if (_value == null || _value.Value <= MomentGenerator())
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
            if (_value == null || _value.Value <= MomentGeneratorUtc())
            {
                AddError($"{_value} is not a UTC future date.");
            }

            return this;
        }
        
        public DateTimeDefender IsInPastUtc()
        {
            NotNull();
            if (_value == null || _value.Value >= MomentGeneratorUtc())
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
}