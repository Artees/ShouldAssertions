using Artees.Diagnostics.BDD;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Tests.Play
{
	internal class UnityShouldListenerTest : MonoBehaviour
	{
		private void Start()
		{
			Debug.Log("Begin");
			using (var listener = new UnityShouldListener())
			{
				ShouldAssertions.Listeners.Add(listener);
				false.Should().BeTrue();
			}
			Debug.Log("End");
		}
	}
}