using System;
using System.Text.RegularExpressions;

namespace FluentDefense.Defenders
{
    public class StringDefender : DefenderBase
    {
        private string _str;
        private string _parameterName;

        public StringDefender(string s, string parameterName) : base(parameterName)
        {
            _str = s;
            _parameterName = parameterName;
        }

        public StringDefender ValidUri(UriKind uriKind=UriKind.Absolute)
        {
            NotNullOrEmpty();
            if (!Uri.TryCreate(_str, uriKind, out _))
            {
                AddError($"\"{_str}\" Uri invalid.");
            }
            
            return this;
        }

        public StringDefender NotNullOrWhiteSpace()
        {
            if (string.IsNullOrWhiteSpace(_str))
            {
                AddError($"{_parameterName} cannot be null or whitespace.");
            }
            return this;
        }
        
        public StringDefender NotNull()
        {
            if (_str == null)
            {
                AddError($"{_parameterName} cannot be null.");
            }
            return this;
        }
        
        public StringDefender NotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(_str))
            {
                AddError($"{_parameterName} cannot be null or empty.");
            }
            return this;
        }

        public StringDefender Custom(Func<string, bool> validator, string messageTemplate)
        {
            if (validator(_str))
            {
                AddError(string.Format(messageTemplate, _parameterName));
            }

            return this;
        }

        public StringDefender ValidEmail()
        {
            try {
                var addr = new System.Net.Mail.MailAddress(_str);
                return this;
            }
            catch
            {
                AddError($"\"{_str}\" is not a valid e-mail address.");
                return this;
            }
        }

        public StringDefender MatchesRegex(string pattern)
        {
            if (!Regex.IsMatch(_str, pattern))
            {
                AddError($"\"{_str}\" does not match required pattern \"{pattern}\"");
            }

            return this;
        }

        public StringDefender MinLength(int minLength)
        {
            NotNull();
            if (_str?.Length < minLength)
            {
                AddError($"\"{_str}\" is shorter than the required minimum length of {minLength}");
            }

            return this;
        }
        
        public StringDefender MaxLength(int maxLength)
        {
            NotNull();
            if (_str?.Length > maxLength)
            {
                AddError($"\"{_str}\" is longer than the required maximum length of {maxLength}");
            }

            return this;
        }
    }
}