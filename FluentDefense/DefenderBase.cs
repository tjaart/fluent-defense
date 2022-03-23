using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentDefense
{
    public abstract class DefenderBase
    {
        protected string _parameterName;

        private readonly List<string> _messages = new List<string>();

        protected DefenderBase(string parameterName)
        {
            _parameterName = parameterName;
        }

        /// <summary>
        /// Throw an exception if any of the validations fail
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void Throw()
        {
            if (!_messages.Any())
            {
                return;
            }
            throw new  ArgumentException(ErrorMessage, _parameterName);
        }

        private List<string> GetFinalList()
        {
            var finalList = new List<string>();
            if (!_messages.Any())
            {
                return finalList;
            }

            finalList.Add($"{_parameterName} is invalid.");
            finalList.AddRange(_messages);
            return finalList;
        }

        /// <summary>
        /// True if no validation errors occurred in the call chain
        /// </summary>
        public bool IsValid => !_messages.Any();

        /// <summary>
        /// Get a list of errors
        /// </summary>
        public List<string> Errors => GetFinalList();

        /// <summary>
        /// Get a single string newline separated list of errors
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                var finalList = GetFinalList();

                if (!finalList.Any())
                {
                    return "";
                }

                return string.Join("\n", finalList);
            }
        }

        protected void AddError(string errorMessage)
        {
            _messages.Add(errorMessage);
        }
    }
}