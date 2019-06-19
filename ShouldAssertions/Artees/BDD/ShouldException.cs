using System;

namespace Artees.BDD
{
    /// <inheritdoc />
    /// <summary>Thrown when a "should" assertion failed.</summary>
    public class ShouldException : Exception
    {
        internal ShouldException(string message) : base(message)
        {
        }
    }
}