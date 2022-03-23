using System;
using System.Runtime.CompilerServices;
using FluentDefense.Defenders;

namespace FluentDefense;

public static class DefensiveExtensions
{
    public static StringDefender Defend(this string str, [CallerArgumentExpression("str")] string parameterName = "")
        => new(str, parameterName);

    public static IntDefender Defend(this int? num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static LongDefender Defend(this long? num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static DoubleDefender Defend(this double? num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static FloatDefender Defend(this float? num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static DecimalDefender Defend(this decimal? num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static DateTimeDefender Defend(this DateTime? value, [CallerArgumentExpression("value")] string parameterName = "")
        => new(value, parameterName);

    // non nullable variants

    public static IntDefender Defend(this int num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static LongDefender Defend(this long num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static DoubleDefender Defend(this double num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static FloatDefender Defend(this float num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static DecimalDefender Defend(this decimal num, [CallerArgumentExpression("num")] string parameterName = "")
        => new(num, parameterName);

    public static DateTimeDefender Defend(this DateTime value, [CallerArgumentExpression("value")] string parameterName = "")
        => new(value, parameterName);
}