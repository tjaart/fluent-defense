using System;
using System.Diagnostics;

namespace FluentDefense.Defenders
{
    public class IntDefender : DefenderBase
    {
        private string _parameterName;
        private int? _num;

        public IntDefender(int? num, string parameterName) : base(parameterName)
        {
            
            
            _num = num;
            _parameterName = parameterName;
        }

        public IntDefender NotNull()
        {
            if (!_num.HasValue)
            {
                AddError($"{_parameterName} cannot be null.");
            }

            return this;
        }

        public IntDefender NotZero()
        {
            if (_num == 0)
            {
                AddError($"{_parameterName} cannot be zero.");
            }

            return this;
        }

        public IntDefender NotNegative()
        {
            if (_num < 0)
            {
                AddError($"{_parameterName} cannot be negative.");
            }

            return this;
        }

        public IntDefender InRange(int rangeStart, int rangeEnd)
        {
            Debug.Assert(rangeEnd > rangeStart, "rangeEnd > rangeStart");
            
            Min(rangeStart);
            Max(rangeEnd);

            return this;
        }

        public IntDefender Min(int minValue)
        {
            if (_num < minValue)
            {
                AddError($"{_parameterName} value is below the minimum value of {minValue}");
            }

            return this;
        }

        public IntDefender Max(int maxValue)
        {
            if (_num > maxValue)
            {
                AddError($"{_parameterName} value is above the maximum value of {maxValue}");
            }

            return this;
        }

        public IntDefender Custom(Func<int?, bool> test, string messageTemplate)
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