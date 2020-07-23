using Artees.BDD;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Tests.Play
{
    internal class UnityShouldListenerTest : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("Begin");
            false.Should().BeTrue();
            Debug.Log("End");
        }
    }
}