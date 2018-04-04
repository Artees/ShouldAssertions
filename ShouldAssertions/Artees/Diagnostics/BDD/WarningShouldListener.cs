using System.Diagnostics;

namespace Artees.Diagnostics.BDD
{
    /// <inheritdoc />
    /// <summary>
    /// A <see cref="ShouldListener" /> that writes a warning message to the <see cref="Trace"/>
    /// listeners.
    /// </summary>
    public class WarningShouldListener : ShouldListener
    {
        /// <inheritdoc />
        public override void LogError(string message)
        {
            Trace.TraceWarning(message);
        }

        /// <inheritdoc />
        public override void LogPending(string message)
        {
            Trace.TraceInformation(message);
        }
    }
}