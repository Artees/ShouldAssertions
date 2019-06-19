using System;

namespace Artees.BDD
{
    /// <inheritdoc />
    /// <summary>
    ///     A listener that monitors <see cref="T:Artees.Diagnostics.BDD.ShouldAssertions" /> output.
    /// </summary>
    public abstract class ShouldListener : IDisposable
    {
        /// <inheritdoc />
        public void Dispose()
        {
            ShouldAssertions.Listeners.Remove(this);
        }

        /// <inheritdoc cref="ShouldAssertions.Fail" />
        public abstract void LogError(string message);

        /// <inheritdoc cref="ShouldAssertions.LogPendingTest" />
        public abstract void LogPending(string message);
    }
}