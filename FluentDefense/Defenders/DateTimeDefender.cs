using System;

namespace FluentDefense.Defenders
{
    public class DateTimeDefender : DefenderBase
    {
        private DateTime? _value;

        public DateTimeDefender(DateTime? value, string parameterName) : base(parameterName)
        {
            _value = value;
        }

        public DateTimeDefender NotNull()
        {
            if (!_value.HasValue)
            {
                AddError($"{_parameterName} cannot be null");
            }

            return this;
        }

        public DateTimeDefender IsInFuture()
        {
            NotNull();
            if (_value == null || _value.Value <= DateTime.Now)
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
            if (_value == null || _value.Value <= DateTime.UtcNow)
            {
                AddError($"{_value} is not a future date.");
            }

            return this;
        }
        
        public DateTimeDefender IsInPastUtc()
        {
            NotNull();
            if (_value == null || _value.Value >= DateTime.UtcNow)
            {
                AddError($"{_value} is not a future date.");
            }

            return this;
        }

        public DateTimeDefender NotDefault()
        {
            NotNull();
            if (_value == null || _value.Value == default(DateTime))
            {
                AddError($"{_value} was never initialized.");
            }

            return this;
        }
    }
}