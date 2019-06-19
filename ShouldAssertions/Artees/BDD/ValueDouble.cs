using System;
using System.Diagnostics;

namespace Artees.BDD
{
    /// <inheritdoc />
    public class ValueDouble : Value<double>
    {
        internal ValueDouble(double actual, Func<string> getName) : base(actual, getName)
        {
        }

        /// <summary>
        ///     Verifies that two values are equal considering a delta.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="delta">
        ///     The maximum acceptable difference between the expected and the actual
        /// </param>
        [Conditional(ShouldAssertions.Define)]
        public void BeEqual(double expected, double delta = double.Epsilon)
        {
            if (Math.Abs(expected - Actual) <= delta == IsPositive) return;
            LogShouldBeEqualError(expected);
        }

        /// <summary>
        ///     Verifies that the value is an <code>NaN</code> value.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void BeNaN()
        {
            if (double.IsNaN(Actual) == IsPositive) return;
            LogShouldBeNaNError();
        }
    }
}