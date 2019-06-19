using System;
using System.Diagnostics;

namespace Artees.BDD
{
    /// <inheritdoc />
    public class ValueFloat : Value<float>
    {
        internal ValueFloat(float actual, Func<string> getName) : base(actual, getName)
        {
        }

        /// <inheritdoc cref="ValueDouble.BeEqual" />
        [Conditional(ShouldAssertions.Define)]
        public void BeEqual(float expected, float delta = float.Epsilon)
        {
            if (Math.Abs(expected - Actual) <= delta == IsPositive) return;
            LogShouldBeEqualError(expected);
        }

        /// <inheritdoc cref="ValueDouble.BeNaN" />
        [Conditional(ShouldAssertions.Define)]
        public void BeNaN()
        {
            if (float.IsNaN(Actual) == IsPositive) return;
            LogShouldBeNaNError();
        }
    }
}