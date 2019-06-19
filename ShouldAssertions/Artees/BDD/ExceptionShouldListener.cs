using System.Diagnostics;

namespace Artees.BDD
{
    /// <inheritdoc />
    /// <exception cref="ShouldException">Thrown on a failure.</exception>
    public class ExceptionShouldListener : ShouldListener
    {
        /// <inheritdoc />
        /// <exception cref="ShouldException">Thrown on a failure.</exception>
        public override void LogError(string message)
        {
            throw new ShouldException(message);
        }

        /// <inheritdoc />
        public override void LogPending(string message)
        {
            Trace.TraceWarning(message);
        }
    }
}