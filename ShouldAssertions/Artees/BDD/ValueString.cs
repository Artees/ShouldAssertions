using System;
using System.Diagnostics;

namespace Artees.BDD
{
    /// <inheritdoc />
    public class ValueString : Value<string>
    {
        internal ValueString(string actual, Func<string> getName) : base(actual, getName)
        {
        }

        /// <summary>
        ///     Asserts that the string contains a substring.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void Contains(string substring)
        {
            if (Actual == null || substring == null)
            {
                if (!IsPositive) return;
            }
            else
            {
                if (Actual.Contains(substring) == IsPositive) return;
            }

            const string f = "{0} {1} contains {2} but was {3}";
            var message = string.Format(f, Name, Should, substring ?? "null", Actual ?? "null");
            LogError(message);
        }
    }
}