using System;
using System.Diagnostics;

namespace Artees.BDD
{
    /// <inheritdoc />
    public class ValueThrow : Value<Action>
    {
        private const string NullExceptionName = "nothing";

        internal ValueThrow(Action action, Func<string> getName) : base(action, getName)
        {
        }

        /// <summary>
        ///     Verifies that a delegate throws a particular exception when called.
        /// </summary>
        /// <typeparam name="T">Type of the expected exception</typeparam>
        [Conditional(ShouldAssertions.Define)]
        public void Throw<T>() where T : Exception
        {
            var exception = NullExceptionName;
            var isCatch = false;
            try
            {
                Actual();
            }
            catch (T e)
            {
                isCatch = true;
                exception = e.ToString();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            if (isCatch == IsPositive) return;

            const string format = "{0} {1} throw {2} but threw {3}";
            var message = string.Format(format, Name, Should, typeof(T), exception);
            LogError(message);
        }
    }
}