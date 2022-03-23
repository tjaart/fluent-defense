using System;
using System.Diagnostics;

namespace FluentDefense.Defenders
{
    public class DecimalDefender : DefenderBase
    {
        private string _parameterName;
        private decimal? _num;

        public DecimalDefender(decimal? num, string parameterName) : base(parameterName)
        {
            _num = num;
            _parameterName = parameterName;
        }

        public DecimalDefender NotNull()
        {
            if (!_num.HasValue)
            {
                AddError($"{_parameterName} cannot be null.");
            }

            return this;
        }
        
        public DecimalDefender NotZero()
        {
            if (_num == 0)
            {
                AddError($"{_parameterName} cannot be zero.");
            }

            return this;
        }

        public DecimalDefender NotNegative()
        {
            if (_num < 0)
            {
                AddError($"{_parameterName} cannot be negative.");
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
            if (_num < minValue)
            {
                AddError($"{_parameterName} value is below the minimum value of {minValue}");
            }

            return this;
        }

        public DecimalDefender Max(decimal maxValue)
        {
            if (_num > maxValue)
            {
                AddError($"{_parameterName} value is above the maximum value of {maxValue}");
            }

            return this;
        }

        public DecimalDefender Custom(Func<decimal?, bool> test, string messageTemplate)
        {
            Debug.Assert(test != null, nameof(test) + " != null");
            if (!test.Invoke(_num))
            {
                AddError(string.Format(messageTemplate, _num));
            }

            return this;
        }
    }
}