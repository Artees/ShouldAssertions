using UnityEngine;

namespace Artees.Diagnostics.BDD
{
	public class UnityShouldListener : ShouldListener 
	{
		public override void LogError(string message)
		{
			Debug.LogAssertion(message);
		}

		public override void LogPending(string message)
		{
			Debug.LogWarning(message);
		}
	}
}