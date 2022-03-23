using System;
using FluentDefense.Defenders;

namespace FluentDefense
{
    public static class DefensiveExtensions
    {
        public static StringDefender Defend(this string str, string parameterName)
        {
            return new StringDefender(str, parameterName);
        }
        
        public static IntDefender Defend(this int? num, string parameterName)
        {
            return new IntDefender(num, parameterName);
        }
        
        public static LongDefender Defend(this long? num, string parameterName)
        {
            return new LongDefender(num, parameterName);
        }
        
        public static DoubleDefender Defend(this double? num, string parameterName)
        {
            return new DoubleDefender(num, parameterName);
        }
        
        public static FloatDefender Defend(this float? num, string parameterName)
        {
            return new FloatDefender(num, parameterName);
        }
        
        public static DecimalDefender Defend(this decimal? num, string parameterName)
        {
            return new DecimalDefender(num, parameterName);
        }

        public static DateTimeDefender Defend(this DateTime? value, string parameterName)
        {
            return new DateTimeDefender(value, parameterName);
        }
        
        
        // non nullable variants
        
        public static IntDefender Defend(this int num, string parameterName)
        {
            return new IntDefender(num, parameterName);
        }
        
        public static LongDefender Defend(this long num, string parameterName)
        {
            return new LongDefender(num, parameterName);
        }
        
        public static DoubleDefender Defend(this double num, string parameterName)
        {
            return new DoubleDefender(num, parameterName);
        }
        
        public static FloatDefender Defend(this float num, string parameterName)
        {
            return new FloatDefender(num, parameterName);
        }
        
        public static DecimalDefender Defend(this decimal num, string parameterName)
        {
            return new DecimalDefender(num, parameterName);
        }

        public static DateTimeDefender Defend(this DateTime value, string parameterName)
        {
            return new DateTimeDefender(value, parameterName);
        }
        
    }
}