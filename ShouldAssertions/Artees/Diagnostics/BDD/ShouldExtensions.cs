using System;
using System.Collections.Generic;

namespace Artees.Diagnostics.BDD
{
    /// <summary>
    /// Shortcuts for <see cref="ShouldAssertions"/>.
    /// </summary>
    public static class ShouldExtensions
    {
        /// <summary>
        /// Sets the variable name for human-readable output of the "should" assertion.
        /// </summary>
        public static ValueThrow Aka(this Action value, string name)
        {
            return new ValueThrow(value, name.ToString);
        }
        
        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueThrow Aka(this Action value, Func<string> name)
        {
            return new ValueThrow(value, name);
        }
        
        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueFloat Aka(this float value, string name)
        {
            return new ValueFloat(value, name.ToString);
        }

        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueFloat Aka(this float value, Func<string> name)
        {
            return new ValueFloat(value, name);
        }
        
        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueDouble Aka(this double value, string name)
        {
            return new ValueDouble(value, name.ToString);
        }

        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueDouble Aka(this double value, Func<string> name)
        {
            return new ValueDouble(value, name);
        }
        
        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueEnumerable<T> Aka<T>(this IEnumerable<T> value, string name)
        {
            return new ValueEnumerable<T>(value, name.ToString);
        }

        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueEnumerable<T> Aka<T>(this IEnumerable<T> value, Func<string> name)
        {
            return new ValueEnumerable<T>(value, name);
        }
        
        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueString Aka(this string value, string name)
        {
            return new ValueString(value, name.ToString);
        }

        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static ValueString Aka(this string value, Func<string> name)
        {
            return new ValueString(value, name);
        }
        
        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static Value<T> Aka<T>(this T value, string name)
        {
            return new Value<T>(value, name.ToString);
        }

        /// <inheritdoc cref="Aka(System.Action,string)"/>
        public static Value<T> Aka<T>(this T value, Func<string> name)
        {
            return new Value<T>(value, name);
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueThrow Should(this ValueThrow value)
        {
            return value;
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueThrow Should(this Action value)
        {
            return new ValueThrow(value, GetDefaultActionName);
        }

        private static string GetDefaultActionName()
        {
            return "Action";
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueFloat Should(this ValueFloat value)
        {
            return value;
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueFloat Should(this float value)
        {
            return new ValueFloat(value, GetDefaultValueName);
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueDouble Should(this ValueDouble value)
        {
            return value;
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueDouble Should(this double value)
        {
            return new ValueDouble(value, GetDefaultValueName);
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueEnumerable<T> Should<T>(this ValueEnumerable<T> value)
        {
            return value;
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueEnumerable<T> Should<T>(this IEnumerable<T> value)
        {
            return new ValueEnumerable<T>(value, GetDefaultValueName);
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueString Should(this ValueString value)
        {
            return value;
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static ValueString Should(this string value)
        {
            return new ValueString(value, GetDefaultValueName);
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static Value<T> Should<T>(this Value<T> value)
        {
            return value;
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static Value<T> Should<T>(this T value)
        {
            return new Value<T>(value, GetDefaultValueName);
        }

        private static string GetDefaultValueName()
        {
            return "Value";
        }

        /// <summary>
        /// Returns available assertions for this value.
        /// </summary>
        /// <param name="value">
        /// The value for which the available assertions are to be returned
        /// </param>
        /// <param name="pendingTest">
        /// The message describing the pending test. Use only for pending (not implemented) tests.
        /// </param>
        public static void Should<T>(this Value<T> value, string pendingTest)
        {
            value.LogPendingTest(pendingTest);
        }

        /// <inheritdoc cref="Should{T}(Value{T},string)"/>
        public static void Should<T>(this T value, string pendingTest)
        {
            var v = new Value<T>(value, GetDefaultValueName);
            v.LogPendingTest(pendingTest);
        }

        /// <summary>
        /// Returns the opposite assertions.
        /// </summary>
        public static T Not<T>(this T value) where T : Value
        {
            value.IsPositive = false;
            return value;
        }

        /// <inheritdoc cref="Not{T}(T)"/>
        public static void Not<T>(this T value, string pendingTest) where T : Value
        {
            value.IsPositive = false;
            value.LogPendingTest(pendingTest);
        }

        internal static T OrShouldNot<T>(this T value, bool isPositive) where T : Value
        {
            value.IsPositive = isPositive;
            return value;
        }
        
        internal static string ToStringExplicitly<T>(this T value)
        {
            return value != null ? value.ToString() : "null";
        }
    }
}