using System;
using System.Diagnostics;

namespace FluentDefense.Defenders
{
    public class IntDefender : DefenderBase<IntDefender, int?>
    {
        public IntDefender(int? num, string parameterName) : base(parameterName, num)
        {
        }

        public IntDefender NotNull()
        {
            if (!Value.HasValue)
            {
                AddError($"{ParameterName} cannot be null.");
            }

            return this;
        }

        public IntDefender NotZero()
        {
            if (Value == 0)
            {
                AddError($"{ParameterName} cannot be zero.");
            }

            return this;
        }

        public IntDefender NotNegative()
        {
            if (Value < 0)
            {
                AddError($"{ParameterName} cannot be negative.");
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
            if (Value < minValue)
            {
                AddError($"{ParameterName} value is below the minimum value of {minValue}");
            }

            return this;
        }

        public IntDefender Max(int maxValue)
        {
            if (Value > maxValue)
            {
                AddError($"{ParameterName} value is above the maximum value of {maxValue}");
            }

            return this;
        }

        // public IntDefender Custom(Func<int?, bool> test, string messageTemplate)
        // {
        //     Debug.Assert(test != null, nameof(test) + " != null");
        //     if (!test.Invoke(Value))
        //     {
        //         AddError(string.Format(messageTemplate, Value));
        //     }
        //
        //     return this;
        // }
    }
}