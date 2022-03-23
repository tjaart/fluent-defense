using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FluentDefense
{
    public abstract class DefenderBase
    {
        protected readonly string ParameterName;

        private readonly List<string> _messages = new List<string>();

        protected DefenderBase(string parameterName)
        {
            ParameterName = parameterName;
        }

        /// <summary>
        /// Throw an exception if any of the validations fail
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        [StackTraceHidden]
        [DebuggerHidden]
        public void Throw()
        {
            if (!_messages.Any())
            {
                return;
            }
            JustThrow();
        }

        [DoesNotReturn]
        [StackTraceHidden]
        [DebuggerHidden]
        protected void JustThrow() 
            => throw new ArgumentException(ErrorMessage, ParameterName);

        private List<string> GetFinalList()
        {
            var finalList = new List<string>();
            if (!_messages.Any())
            {
                return finalList;
            }

            finalList.Add($"{ParameterName} is invalid.");
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

    public abstract class DefenderBase<TDefender, TValue> : DefenderBase 
    where TDefender : DefenderBase<TDefender, TValue>
    {
        protected readonly TValue Value;

        protected DefenderBase(string parameterName, TValue value) : base(parameterName)
        {
            Value = value;
        }
        
        public TValue ValueOrThrow()
        {
            if (IsValid)
            {
                return Value;
            }

            JustThrow();
            return default;
        }
        
        public TDefender Custom(Func<TValue, bool> test, string messageTemplate)
        {
            Debug.Assert(test != null, nameof(test) + " != null");
            if (!test.Invoke(Value))
            {
                AddError(string.Format(messageTemplate, Value));
            }

            return (TDefender)this;
        }
    }
}