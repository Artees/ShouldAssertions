using System.Collections.Generic;
using System.Diagnostics;

namespace Artees.Diagnostics.BDD
{
    /// <summary>
    /// Methods and properties that help you implement invariants for your code. 
    /// </summary>
    public static class ShouldAssertions
    {
        internal const string Define = "SHOULD_ASSERTIONS";
        
        /// <summary>
        /// The list of listeners that is monitoring the <see cref="ShouldAssertions"/> output.
        /// </summary>
        public static readonly List<ShouldListener> Listeners = new List<ShouldListener>();
        
        private static readonly List<ShouldListener> ListenersCopy = new List<ShouldListener>();

        static ShouldAssertions()
        {
            Clear();
        }

        /// <summary>
        /// Resets the state of <see cref="ShouldAssertions"/>. Use with caution.
        /// </summary>
        [Conditional(Define)]
        public static void Clear()
        {
            Listeners.Clear();
            Listeners.Add(new EmptyWarningShouldListener());
            ListenersCopy.Clear();
        }

        private static IEnumerable<ShouldListener> CopyListeners()
        {
            ListenersCopy.Clear();
            ListenersCopy.AddRange(Listeners);
            return ListenersCopy;
        }

        /// <summary>
        /// Logs a failed test.
        /// </summary>
        [Conditional(Define)]
        public static void Fail(string message = "")
        {
            foreach (var listener in CopyListeners())
            {
                listener.LogError(message);
            }
        }

        /// <summary>
        /// Logs a pending (not implemented) test.
        /// </summary>
        [Conditional(Define)]
        public static void LogPendingTest(string message = "")
        {
            foreach (var listener in CopyListeners())
            {
                listener.LogPending(message);
            }
        }
    }
}