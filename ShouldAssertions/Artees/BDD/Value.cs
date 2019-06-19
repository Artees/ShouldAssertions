using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Artees.BDD
{
    /// <summary>
    ///     Contains the methods that implement the <see cref="ShouldAssertions" />.
    /// </summary>
    public abstract class Value
    {
        private readonly Func<string> _getName;

        /// <summary>
        ///     Initializes an instance of the <see cref="Value" /> class used in
        ///     <see cref="ShouldAssertions" />.
        /// </summary>
        protected Value(Func<string> getName)
        {
            _getName = getName;
            IsPositive = true;
        }

        internal bool IsPositive { get; set; }

        /// <summary>
        ///     The value name for human-readable output.
        /// </summary>
        protected string Name
        {
            get { return _getName().ToStringExplicitly(); }
        }

        /// <summary>
        ///     Returns "should" for a regular assertion and "should not" for an inverted one.
        /// </summary>
        protected string Should
        {
            get { return IsPositive ? "should" : "should not"; }
        }

        /// <summary>
        ///     Returns a string that represents the actual (not the expected) value.
        /// </summary>
        protected abstract string ActualString { get; }

        internal void LogPendingTest(string pendingTest)
        {
            var message = string.Format("{0} {1} {2} but was {3}", Name, Should, pendingTest,
                ActualString);
            LogPending(message);
        }

        private static void LogPending(string message)
        {
            ShouldAssertions.LogPendingTest(message);
        }

        /// <returns>actual.Aka(Name).Should()</returns>
        protected Value<T> ActualShould<T>(T actual)
        {
            return actual.Aka(_getName).Should();
        }

        /// <inheritdoc cref="ActualShould{T}(T)" />
        protected ValueEnumerable<T> ActualShould<T>(IEnumerable<T> actual)
        {
            return actual.Aka(_getName).Should();
        }
    }

    /// <inheritdoc />
    public class Value<T> : Value
    {
        /// <summary>
        ///     The actual (not the expected) value.
        /// </summary>
        protected readonly T Actual;

        internal Value(T actual, Func<string> getName) : base(getName)
        {
            Actual = actual;
        }

        /// <inheritdoc />
        protected override string ActualString
        {
            get { return Actual.ToStringExplicitly(); }
        }

        private Value<T> ActualShould
        {
            get { return ActualShould(Actual); }
        }

        /// <summary>
        ///     Verifies that two values are equal.
        /// </summary>
        /// <param name="expected">The value that is expected</param>
        [Conditional(ShouldAssertions.Define)]
        public void BeEqual<TValue>(TValue expected)
        {
            var equals = expected == null ? Actual == null : expected.Equals(Actual);
            if (equals == IsPositive) return;
            LogShouldBeEqualError(expected);
        }

        /// <summary>
        ///     Logs a message when an "equal" assertion failed.
        /// </summary>
        protected void LogShouldBeEqualError<TValue>(TValue expected)
        {
            var expectedString = expected.ToStringExplicitly();
            var message = string.Format("{0} {1} be {2} but was {3}", Name, Should,
                expectedString, ActualString);
            LogError(message);
        }

        /// <summary>
        ///     Logs a message when a "NaN" assertion failed.
        /// </summary>
        protected void LogShouldBeNaNError()
        {
            var message = string.Format("{0} {1} be an NaN value but was {2}", Name, Should,
                ActualString);
            LogError(message);
        }

        /// <summary>
        ///     Asserts that a condition is true.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void BeTrue()
        {
            ActualShould.OrShouldNot(IsPositive).BeEqual(true);
        }

        /// <summary>
        ///     Asserts that a condition is false.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void BeFalse()
        {
            ActualShould.OrShouldNot(IsPositive).BeEqual(false);
        }

        /// <summary>
        ///     Verifies that the object is equal to <code>null</code>.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void BeNull()
        {
            ActualShould.OrShouldNot(IsPositive).BeEqual<object>(null);
        }

        /// <summary>
        ///     Verifies that the value is greater.
        /// </summary>
        /// <param name="expected">The value that expected to be less</param>
        [Conditional(ShouldAssertions.Define)]
        public void BeGreaterThan<TValue>(TValue expected) where TValue : IComparable<T>
        {
            if ((expected != null && expected.CompareTo(Actual) < 0) == IsPositive) return;
            var expectedString = expected.ToStringExplicitly();
            var mes = string.Format("{0} {1} be greater than {2} but was {3}", Name, Should,
                expectedString, ActualString);
            LogError(mes);
        }

        /// <summary>
        ///     Verifies that the value is less.
        /// </summary>
        /// <param name="expected">The value that expected to be greater</param>
        [Conditional(ShouldAssertions.Define)]
        public void BeLessThan<TValue>(TValue expected) where TValue : IComparable<T>
        {
            if ((expected != null && expected.CompareTo(Actual) > 0) == IsPositive) return;
            var expectedString = expected.ToStringExplicitly();
            var mes = string.Format("{0} {1} be less than {2} but was {3}", Name, Should,
                expectedString, ActualString);
            LogError(mes);
        }

        /// <summary>
        ///     Verifies that the value is greater or equal.
        /// </summary>
        /// <param name="expected">The value that expected to be less</param>
        [Conditional(ShouldAssertions.Define)]
        public void BeGreaterThanOrEqual<TValue>(TValue expected) where TValue : IComparable<T>
        {
            ActualShould.OrShouldNot(!IsPositive).BeLessThan(expected);
        }

        /// <summary>
        ///     Verifies that the value is less or equal.
        /// </summary>
        /// <param name="expected">The value that expected to be greater</param>
        [Conditional(ShouldAssertions.Define)]
        public void BeLessThanOrEqual<TValue>(TValue expected) where TValue : IComparable<T>
        {
            ActualShould.OrShouldNot(!IsPositive).BeGreaterThan(expected);
        }

        /// <summary>
        ///     Asserts that the collection contains an item.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void Contains<TValue>(TValue item)
        {
            var enumerable = Actual as IEnumerable<TValue>;
            if (enumerable == null)
                ActualShould.BeInstanceOf<IEnumerable<TValue>>();
            else
                ActualShould(enumerable).Contains(item);
        }

        /// <summary>
        ///     Asserts that the object is an instance of a given type.
        /// </summary>
        /// <typeparam name="TValue">The expected Type</typeparam>
        [Conditional(ShouldAssertions.Define)]
        public void BeInstanceOf<TValue>()
        {
            if (Actual is TValue == IsPositive) return;
            var m = string.Format("{0} {1} be an instance of {2} but was {3}", Name, Should,
                typeof(TValue), ActualString);
            LogError(m);
        }

        /// <summary>
        ///     Asserts that two objects refer to the same object.
        /// </summary>
        [Conditional(ShouldAssertions.Define)]
        public void BeSame<TValue>(TValue expected)
        {
            if (ReferenceEquals(expected, Actual) == IsPositive) return;
            var expectedString = expected.ToStringExplicitly();
            var message = string.Format("{0} {1} refer to {2} but was {3}", Name, Should,
                expectedString, ActualString);
            LogError(message);
        }

        /// <inheritdoc cref="ShouldListener.LogError" />
        protected static void LogError(string message)
        {
            ShouldAssertions.Fail(message);
        }
    }
}