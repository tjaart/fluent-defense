using System;
using System.Diagnostics;

namespace FluentDefense.Defenders
{
    public class DoubleDefender : DefenderBase
    {
        private string _parameterName;
        private double? _num;

        public DoubleDefender(double? num, string parameterName) : base(parameterName)
        {
            _num = num;
            _parameterName = parameterName;
        }
        
        public DoubleDefender NotNull()
        {
            if (!_num.HasValue)
            {
                AddError($"{_parameterName} cannot be null.");
            }

            return this;
        }

        public DoubleDefender NotZero()
        {
            if (_num == 0)
            {
                AddError($"{_parameterName} cannot be zero.");
            }

            return this;
        }

        public DoubleDefender NotNegative()
        {
            if (_num < 0)
            {
                AddError($"{_parameterName} cannot be negative.");
            }

            return this;
        }

        public DoubleDefender InRange(double rangeStart, double rangeEnd)
        {
            Debug.Assert(rangeEnd > rangeStart, "rangeEnd > rangeStart");
            Min(rangeStart);
            Max(rangeEnd);

            return this;
        }

        public DoubleDefender Min(double minValue, Func<string, double, string>? customMessage=null)
        {
            if (_num < minValue)
            {
                AddError($"{_parameterName} value is below the minimum value of {minValue}");
            }

            return this;
        }

        public DoubleDefender Max(double maxValue)
        {
            if (_num > maxValue)
            {
                AddError($"{_parameterName} value is above the maximum value of {maxValue}");
            }

            return this;
        }

        public DoubleDefender Custom(Func<double?, bool> test, string messageTemplate)
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