using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Artees.Diagnostics.BDD
{
    /// <inheritdoc />
    public class ValueEnumerable<T> : Value<IEnumerable<T>>
    {
        internal ValueEnumerable(IEnumerable<T> actual, Func<string> getName) : base(actual,
            getName)
        {
        }

        /// <inheritdoc cref="Value{T}.Contains{TValue}"/>
        [Conditional(ShouldAssertions.Define)]
        public void Contains(T item)
        {
            if ((Actual != null && Actual.Contains(item)) == IsPositive) return;
            var itemString = item.ToStringExplicitly();
            var mes = string.Format("{0} {1} contains {2} but was {3}", Name, Should, itemString,
                EnumerableString);
            LogError(mes);
        }

        private string EnumerableString
        {
            get
            {
                if (Actual == null) return "null";
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(Actual);
                stringBuilder.Append(" {");
                foreach (var i in Actual)
                {
                    stringBuilder.Append(i);
                    stringBuilder.Append(", ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
    }
}