using System;
using System.Text.RegularExpressions;

namespace FluentDefense.Defenders;

public class StringDefender : DefenderBase<StringDefender, string?>
{
    public StringDefender(string s, string parameterName) : base(parameterName, s)
    {
    }

    public StringDefender ValidUri(UriKind uriKind = UriKind.Absolute)
    {
        NotNullOrWhiteSpace();
        if (!Uri.TryCreate(Value, uriKind, out _))
        {
            AddError($"\"{Value}\" Uri invalid.");
        }

        return this;
    }

    public StringDefender NotNullOrWhiteSpace()
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            AddError($"{ParameterName} cannot be null or whitespace.");
        }

        return this;
    }

    public StringDefender NotNull()
    {
        if (Value == null)
        {
            AddError($"{ParameterName} cannot be null.");
        }

        return this;
    }

    public StringDefender NotNullOrEmpty()
    {
        if (string.IsNullOrEmpty(Value))
        {
            AddError($"{ParameterName} cannot be null or empty.");
        }

        return this;
    }

    public StringDefender ValidEmail()
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(Value);
            return this;
        }
        catch
        {
            AddError($"\"{Value}\" is not a valid e-mail address.");
            return this;
        }
    }

    public StringDefender MatchesRegex(string pattern)
    {
        if (!Regex.IsMatch(Value, pattern))
        {
            AddError($"\"{Value}\" does not match required pattern \"{pattern}\"");
        }

        return this;
    }

    public StringDefender MinLength(int minLength)
    {
        NotNull();
        if (Value?.Length < minLength)
        {
            AddError($"\"{Value}\" is shorter than the required minimum length of {minLength}");
        }

        return this;
    }

    public StringDefender MaxLength(int maxLength)
    {
        NotNull();
        if (Value?.Length > maxLength)
        {
            AddError($"\"{Value}\" is longer than the required maximum length of {maxLength}");
        }

        return this;
    }
}