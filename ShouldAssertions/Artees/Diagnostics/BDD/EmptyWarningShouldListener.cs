using System.Diagnostics;

namespace Artees.Diagnostics.BDD
{
    internal class EmptyWarningShouldListener : ShouldListener
    {
        public override void LogError(string message)
        {
            LogWarning();
        }

        public override void LogPending(string message)
        {
            LogWarning();
        }

        private void LogWarning()
        {
            ShouldAssertions.Listeners.Remove(this);
            if (ShouldAssertions.Listeners.Count > 0) return;
            const string m = "Add a listener to " +
                             "Artees.Diagnostics.ShouldAssertions.ShouldAssertions.Listeners " +
                             "to see the ShouldAssertions output.";
            Trace.TraceWarning(m);
        }
    }
}