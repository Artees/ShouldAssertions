using System.Diagnostics;

namespace Artees.BDD
{
    /// <inheritdoc />
    /// <summary>
    ///     A <see cref="ShouldListener" /> that directs the <see cref="ShouldAssertions" /> output to
    ///     <see cref="Trace" />.
    /// </summary>
    public class TraceShouldListener : ShouldListener
    {
        /// <inheritdoc />
        public override void LogError(string message)
        {
            Trace.TraceError(message);
        }

        /// <inheritdoc />
        public override void LogPending(string message)
        {
            Trace.TraceWarning(message);
        }
    }
}